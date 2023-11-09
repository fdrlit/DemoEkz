using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace DemoDinar
{
    /// <summary>
    /// Interaction logic for CustomerOrders.xaml
    /// </summary>
    public partial class CustomerOrders : Window
    {
        private SFabricaEntities db;
        public CustomerOrders()
        {
            InitializeComponent();
            LoadOrdersFromDatabase();
            db = new SFabricaEntities();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void LoadOrdersFromDatabase()
        {
            using (SFabricaEntities dbContext = new SFabricaEntities())
            {
                var orders = dbContext.Zakaz.Where(x => x.IdZakazchika == UserManager.Instance.UserID)
                    .Include(z => z.ZakazniyeIzdeliya)
                    .Select(z => new
                    {
                        Nomer = z.Nomer,
                        DATA = z.DATA,
                        STATUS = z.STATUS,
                        Stoimost = z.Stoimost
                    })
                    .ToList();

                OrderList.ItemsSource = orders;
            }

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
            CustomerPage customerPage = new CustomerPage();
            customerPage.Show();
            this.Close();
        }

        private void OrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderList.SelectedItem != null)
            {

                var selectedOrder = (dynamic)OrderList.SelectedItem;

                int orderNumber = selectedOrder.Nomer;
                string orderStatus = selectedOrder.STATUS;

                var order = db.Zakaz.Where(x => x.Nomer == orderNumber).First();

                if (order.STATUS == "Новый")
                {
                    DropButton.IsEnabled = true;
                    DropButton.Click += (s, args) =>
                    {
                        order.STATUS = "Отклонен";
                        DropButton.IsEnabled = false;
                        db.SaveChanges();
                        LoadOrdersFromDatabase();
                    };
                    db.SaveChanges();
                    LoadOrdersFromDatabase();

                }
            }
        }


        private void DropButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

