using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для GoodsInformation.xaml
    /// </summary>
    public partial class GoodsInformation : Window
    {
        public SFabricaEntities _db = new SFabricaEntities();
        private Izdeliya _izdeliya;
        public GoodsInformation(SFabricaEntities db, object o)
        {
            InitializeComponent();
            _izdeliya = (o as Button).DataContext as Izdeliya;
            _db = db;
            name.Text = _izdeliya.Naimenovanie;
            Shirina.Text = _izdeliya.Shirina.ToString();
            Dlina.Text = _izdeliya.Dlina.ToString();
            Articul.Text = _izdeliya.Articul.ToString();
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
            GoodsList goodsList = new GoodsList();
            goodsList.Show();
            this.Close();
        }
    }
}
