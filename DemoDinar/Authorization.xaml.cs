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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoDinar
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private bool loginTextCleared = false;
        private bool passwordTextCleared = false;
        public Authorization()
        {
            InitializeComponent();
            Color rgbAuthorizationGridColor = Color.FromRgb(255, 255, 255);
            Brush AuthorizationGridBrush = new SolidColorBrush(rgbAuthorizationGridColor);
            AuthorizationGrid.Background = AuthorizationGridBrush;
            Color rgbAuthorizationButtonColor = Color.FromRgb(181, 213, 202);
            Brush AuthorizationButtonBrush = new SolidColorBrush(rgbAuthorizationButtonColor);
            AuthorizationButton.Background = AuthorizationButtonBrush;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void GoToRegistration_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
            loginTextCleared = false;
            passwordTextCleared = false;
        }

        private void LoginTextBlock_GotFocus(object sender, RoutedEventArgs e)
        {
                if ( LoginTextBlock.Text == "Логин" && !loginTextCleared)
                {
                    LoginTextBlock.Text = "";
                    loginTextCleared = true;
                }
            }

        private void PasswordTextBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            if ( PasswordTextBlock.Text == "Пароль" && !passwordTextCleared)
            {
                PasswordTextBlock.Text = "";
                passwordTextCleared = true;
            }
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
           using (SFabricaEntities db  = new SFabricaEntities())
            {
                var matchingUser = db.AppUser.FirstOrDefault(u => u.Login == LoginTextBlock.Text && u.Password == PasswordTextBlock.Text);
                if (matchingUser != null)
                {
                    UserManager.Instance.UserID = matchingUser.Id;
                    if (matchingUser.Role == 1)
                    {
                        MessageBox.Show("Авторизация пользователем успешна!");
                        CustomerPage client = new CustomerPage();
                        client.Show();
                        this.Close();
                    }
                    else if (matchingUser.Role == 2)
                    {
                        MessageBox.Show("Авторизация менеджером успешна!");
                        ManagerPage manager = new ManagerPage();
                        manager.Show();
                        this.Close();
                    }
                    else if (matchingUser.Role == 3)
                    {
                        MessageBox.Show("Авторизация кладовщиком успешна!");
                        StockmanPage manager = new StockmanPage();
                        manager.Show();
                        this.Close();
                    }
                    else if (matchingUser.Role == 4)
                    {
                        MessageBox.Show("Авторизация дирекцией успешна!");
                        DirectorPage director = new DirectorPage();
                        director.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                }
            }
        }
    }
}
