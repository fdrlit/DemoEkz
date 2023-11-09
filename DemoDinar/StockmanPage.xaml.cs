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
    /// Логика взаимодействия для StockmanPage.xaml
    /// </summary>
    public partial class StockmanPage : Window
    {
        public StockmanPage()
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

        private void Fabrics_Click(object sender, RoutedEventArgs e)
        {
            FabricsList fabricsList = new FabricsList();
            fabricsList.Show();
            this.Close();

        }

        private void Furniture_Click(object sender, RoutedEventArgs e)
        {
            FurnitureList furnitureList = new FurnitureList();
            furnitureList.Show();
            this.Close();
        }
    }
}
