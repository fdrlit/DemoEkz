using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Windows.Markup;

namespace DemoDinar
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    /// 
    
    public class DisplayItem
    {
        public string Articul { get; set; }
        public string Naimenovanie { get; set; }
        public Visibility Visibility { get; set; } = Visibility.Visible;
        public override string ToString()
        {
            return $"{Naimenovanie} - {Articul}";
        }
    }
    public partial class Order : Window
    {
        public decimal priceOrder = 0;
        private List<OrderedItem> orderedItems = new List<OrderedItem>();

        public Order()
        {
            InitializeComponent();
            using (SFabricaEntities db = new SFabricaEntities())
            {
                var izdeliya = db.Izdeliya
            .Where(iz => db.TkaniIzdeliya.Any(ti => ti.IdIzdeliya == iz.Articul) && db.FurnituraIzdeliya.Any(fi => fi.IdIzdeliya == iz.Articul))
            .Select(iz => new DisplayItem
            {
                Articul = iz.Articul,
                Naimenovanie = iz.Naimenovanie
            })
            .ToList();
                ProductComboBox.ItemsSource = izdeliya;
            }
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            UserManager.Instance.ClearUserID();
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SFabricaEntities sFabricaEntities = new SFabricaEntities();
            var userRole = sFabricaEntities.AppUser.Where(u => u.Id == UserManager.Instance.UserID).Select(u => u.Role).FirstOrDefault();
            if (userRole == 2)
            {
                ManagerPage managerPage = new ManagerPage();
                managerPage.Show();
                this.Close();
            }
            else if (userRole == 1)
            {
                CustomerPage clientPage = new CustomerPage();
                clientPage.Show();
                this.Close();
            }
        }



        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductComboBox.SelectedItem is DisplayItem selectedItem)
            {
                var izdeliya = (List<DisplayItem>)ProductComboBox.ItemsSource;
                izdeliya.Remove(selectedItem);
                ProductComboBox.ItemsSource = null;
                ProductComboBox.ItemsSource = izdeliya;
                ProductComboBox.SelectedItem = null;

                using (var db = new SFabricaEntities())
                {
                    var izdelieInfo = db.Izdeliya
                        .Where(i => i.Articul == selectedItem.Articul)
                        .Select(i => new { i.Articul, i.Naimenovanie, i.Shirina, i.Dlina })
                        .FirstOrDefault();

                    if (izdelieInfo != null)
                    {
                        TextBlock articulTextBlock = new TextBlock { Text = $"Артикул: {izdelieInfo.Articul}", Margin = new Thickness(5) };
                        TextBlock nameTextBlock = new TextBlock { Text = $"Наименование: {izdelieInfo.Naimenovanie}", Margin = new Thickness(5) };
                        TextBlock shirinaTextBlock = new TextBlock { Text = $"Ширина: {izdelieInfo.Shirina} см", Margin = new Thickness(5) };
                        TextBlock dlinaTextBlock = new TextBlock { Text = $"Длина: {izdelieInfo.Dlina} см", Margin = new Thickness(5) };
                        var poiskTkani = db.TkaniIzdeliya
                                             .Where(i => i.IdIzdeliya == izdelieInfo.Articul)
                                             .FirstOrDefault();
                        var Tkani = db.Tkani
                                         .Where(i => i.Articul == poiskTkani.IdTkani).FirstOrDefault();

                        var poiskFurnituri = db.FurnituraIzdeliya
                                             .Where(i => i.IdIzdeliya == izdelieInfo.Articul)
                                             .FirstOrDefault();
                        var Furnitura = db.Furnitura
                                         .Where(i => i.Articul == poiskFurnituri.IdFurnitura).FirstOrDefault();

                        var priceTkani = Tkani.Price;
                        var priceFurnituri = Furnitura.Cena;

                        var priceIzdeliya = priceTkani + priceFurnituri;

                        priceOrder += priceIzdeliya;
                        OrderPrice.Text = $"{priceOrder}";

                        // Добавляем выбранный элемент в список заказанных элементов
                        orderedItems.Add(new OrderedItem
                        {
                            IdIzdeliya = izdelieInfo.Articul,
                            Kolichestvo = 1
                        });

                        // Отображаем информацию о выбранном элементе в интерфейсе
                        var line = new Border
                        {
                            Height = 15,
                            Margin = new Thickness(0, 10, 0, 10),
                            Background = new SolidColorBrush(Color.FromRgb(255, 252, 214))
                        };

                        OrderStackPanel.Children.Add(articulTextBlock);
                        OrderStackPanel.Children.Add(nameTextBlock);
                        OrderStackPanel.Children.Add(shirinaTextBlock);
                        OrderStackPanel.Children.Add(dlinaTextBlock);
                        OrderStackPanel.Children.Add(line);

                    }
                }
            }
        }



        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            int idUser = 0;
            int idManager = 0;
            int idOrder = 0;
            string status = "Новый";
            using (SFabricaEntities sfabric = new SFabricaEntities())
            {
                var userRole = sfabric.AppUser.Where(u => u.Id == UserManager.Instance.UserID).Select(u => u.Role).FirstOrDefault();
                if (userRole == 1)
                {
                    idUser = UserManager.Instance.UserID;
                    Zakaz newOrder1 = new Zakaz
                    {
                        STATUS = status,
                        Stoimost = priceOrder,
                        IdZakazchika = idUser,
                        DATA = DateTime.Today,
                    };
                    sfabric.Zakaz.Add(newOrder1);
                    sfabric.SaveChanges();
                    idOrder = newOrder1.Nomer;

                }
                else if (userRole == 2)
                {
                    idManager = UserManager.Instance.UserID;
                    Zakaz newOrder = new Zakaz
                    {
                        STATUS = status,
                        Stoimost = priceOrder,
                        IdManagera = idManager,
                        DATA = DateTime.Today,
                    };
                   
                    sfabric.Zakaz.Add(newOrder);
                    sfabric.SaveChanges();
                    idOrder = newOrder.Nomer;
                   
                }

               
            }
            using (SFabricaEntities sfabric = new SFabricaEntities())
            {
                foreach (var item in orderedItems)
                {
                    ZakazniyeIzdeliya newOrderedItem = new ZakazniyeIzdeliya
                    {
                        IdZakaza = idOrder,
                        IdIzdeliya = item.IdIzdeliya,
                        Kolichestvo = item.Kolichestvo.ToString()
                    };
                    sfabric.ZakazniyeIzdeliya.Add(newOrderedItem);
                }
                sfabric.SaveChanges();

                orderedItems.Clear();
            }
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public class OrderedItem
        {
            public string IdIzdeliya { get; set; }
            public int Kolichestvo { get; set; }
        }
    }
}
