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

namespace DemoDinar
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Window
    {
        public ManagerPage()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            UserManager.Instance.ClearUserID();
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }


        private void ToMakeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            order.Show();
            this.Close();
        }

        private void GoodsListButton_Click(object sender, RoutedEventArgs e)
        {
            GoodsList goodsList = new GoodsList();
            goodsList.Show();
            this.Close();
        }
    }
}
