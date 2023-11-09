﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DemoDinar
{
    public partial class Registration : Window
    {
        private bool loginTextCleared = false;
        private bool passwordTextCleared = false;
        private bool nameTextCleared = false;
        private bool emailTextCleared = false;
        private bool confirmPasswordTextCleared = false;

        public Registration()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Color rgbRegistrationGridColor = Color.FromRgb(255, 255, 255);
            Brush RegistrationGridBrush = new SolidColorBrush(rgbRegistrationGridColor);
            RegistrationGrid.Background = RegistrationGridBrush;

            Color rgbRegistrationButtonColor = Color.FromRgb(181, 213, 202);
            Brush RegistrationButtonBrush = new SolidColorBrush(rgbRegistrationButtonColor);
            RegistrationButton.Background = RegistrationButtonBrush;
        }

        private void GoToAuthorization_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string name = NameTextBox.Text;
            string password = PasswordTextBox.Text;
            string confirmPassword = ConfirmPasswordTextBox.Text;

            if (!IsValidEmail(login))
            {
                MessageBox.Show("Введите правильный адрес электронной почты!");
                return;
            }

            if (!IsValidName(name))
            {
                MessageBox.Show("Имя должно содержать только кириллицу (от 2 до 15 символов)!");
                return;
            }

            if (!IsValidPassword(password, confirmPassword))
            {
                MessageBox.Show("Проверьте, что пароль содержит от 6 символов и совпадает с подтверждением пароля!");
                return;
            }

            using (SFabricaEntities db = new SFabricaEntities())
            {
                if (db.AppUser.Any(user => user.Login == login))
                {
                    MessageBox.Show("Пользователь с такой почтой уже зарегистрирован!");
                    return;
                }

                AppUser newUser = new AppUser
                {
                    Login = login,
                    Name = name,
                    Password = password,
                    Role = 1,
                };

                db.AppUser.Add(newUser);
                db.SaveChanges();
                UserManager.Instance.UserID = newUser.Id;
                MessageBox.Show("Регистрация успешна!");
                loginTextCleared = false;
                passwordTextCleared = false;
                nameTextCleared = false;
                emailTextCleared = false;
                confirmPasswordTextCleared = false;
            }
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }

        private bool IsValidName(string name)
        {
            string pattern = @"^[А-Яа-я]{2,15}$";
            return System.Text.RegularExpressions.Regex.IsMatch(name, pattern);
        }

        private bool IsValidPassword(string password, string confirmPassword)
        {
            if (password.Length < 6 || password.Length > 15)
            {
                return false;
            }

            if (password != confirmPassword)
            {
                return false;
            }

            return true;
        }

        private void LoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text == "Почта" && !loginTextCleared)
            {
                LoginTextBox.Text = "";
                loginTextCleared = true;
            }
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "Имя" && !nameTextCleared)
            {
                NameTextBox.Text = "";
                nameTextCleared = true;
            }
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTextBox.Text == "Пароль" && !passwordTextCleared)
            {
                PasswordTextBox.Text = "";
                passwordTextCleared = true;
            }
        }

        private void ConfirmPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ConfirmPasswordTextBox.Text == "Повторите пароль" && !confirmPasswordTextCleared)
            {
                ConfirmPasswordTextBox.Text = "";
                confirmPasswordTextCleared = true;
            }
        }
    }

    public class UserManager
    {
        private static UserManager instance;

        public int UserID { get; set; }

        private UserManager() { }

        public static UserManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserManager();
                }
                return instance;
            }
        }

        public void ClearUserID()
        {
            UserID = -1;
        }
    }
}
