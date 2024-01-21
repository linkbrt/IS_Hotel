using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace WpfApp1 {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// Server=localhost;Database=master;Trusted_Connection=True;
    /// </summary>
    public partial class MainWindow : Window {
        readonly static string connectionString = "";

        public SqlConnection sqlConnection = new SqlConnection(connectionString);
        hoteldbEntities db = new hoteldbEntities();

        public MainWindow() {
            InitializeComponent();
            LoginPanel.Visibility = Visibility.Visible;
            RegistrationPanel.Visibility = Visibility.Collapsed;
        }

        // Вход
        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            byte[] data = Encoding.ASCII.GetBytes(PasswordTextBox.Text);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string password_hash = Encoding.ASCII.GetString(data);


            App.CurrentUser = db.users.Where(
                user_item => user_item.login == LoginTextBox.Text &&
                             user_item.password == password_hash
            ).FirstOrDefault();
            if (App.CurrentUser != null)
            {
                MessageBox.Show($"Добро пожаловать, {App.CurrentUser.role}!");
                
                
                switch (App.CurrentUser.role)
                {
                    case 0:
                        new ClientWindow().Show();
                        break;
                    case 1:
                        new ManagerWindow().Show();
                        break;
                    case 2:
                        new AdminWindow().Show();
                        break;
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!");
            }
        }

        // Регистрация
        private void RegisterButton_Click(object sender, RoutedEventArgs e) {
            if (passwordBoxNew1.Password != passwordBoxNew2.Password) {
                MessageBox.Show("Указанные пароли не совпадают!");
                return;
            }

            byte[] data = Encoding.ASCII.GetBytes(passwordBoxNew1.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string password_hash = Encoding.ASCII.GetString(data);
            var current_user = db.users.Where(
                user_item => user_item.login == newLoginBox.Text
            ).FirstOrDefault();
            if (current_user != null) {
                MessageBox.Show($"Пользователь с заданным логином уже существует!");
                return;
            }

            user newUser = new user {
                login = newLoginBox.Text,
                password = password_hash,
                role = 0,
            };
            db.users.Add(newUser);
            db.SaveChanges();

            MessageBox.Show("Пользователь успешно создан!");
            ShowLoginPanel();
            
        }



        private void ToRegisterButton_Click(object sender, RoutedEventArgs e) {
            ShowRegisterPanel();
        }

        private void ToLoginPageButton_Click(object sender, RoutedEventArgs e) {
            ShowLoginPanel();
        }

        private void ShowLoginPanel() {
            LoginPanel.Visibility = Visibility.Visible;
            RegistrationPanel.Visibility = Visibility.Collapsed;
            this.Title = "Вход";
        }

        private void ShowRegisterPanel() {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegistrationPanel.Visibility = Visibility.Visible;
            this.Title = "Регистрация";
        }
    }
}
