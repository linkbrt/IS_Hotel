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
    /// </summary>
    public partial class MainWindow : Window {
        readonly static string connectionString = 
            "server=DESKTOP-3TTJHJR;Trusted_Connection=Yes;DataBase=hotel_db;";

        public SqlConnection sqlConnection = new SqlConnection(connectionString);
        hotelDbEntities db = new hotelDbEntities();
        
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


            /*var current_user = db.users.Where(
                user_item => user_item.login == LoginTextBox.Text && 
                             user_item.password == password_hash
            ).FirstOrDefault();
            if (current_user != null ) {
                MessageBox.Show($"Добро пожаловать, {current_user.login}!");
            } else {
                MessageBox.Show("Такого пользователя не существует!");
            }*/
            //new AdminWindow(db).Show();
            //new ManagerWindow(db).Show();
            new ClientWindow().Show();
            this.Close();
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
