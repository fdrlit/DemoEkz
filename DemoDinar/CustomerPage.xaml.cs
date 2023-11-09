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
    /// Логика взаимодействия для CustomerPage.xaml
    /// </summary>
    public partial class CustomerPage : Window
    {
        public CustomerPage()
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

        private void MyOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerOrders customerOrders = new CustomerOrders();
            customerOrders.Show();
            this.Close();
        }
    }
}
