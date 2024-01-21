using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
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

namespace WpfApp1 {
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window {
        hoteldbEntities db = new hoteldbEntities();
        Dictionary<string, DbSet> data;
        public AdminWindow() {
            InitializeComponent();
            this.data = new Dictionary<string, DbSet>() {
                { "user", db.users },
                { "customer", db.customers },
                //{ "employee", db.employees },
                //{ "hotel", db.hotels },
                { "room", db.rooms },
                { "reservation", db.reservations },
                //{ "service", db.services },
                //{ "service_reservation", db.service_reservation },
            };

            modelBox.ItemsSource = data.Keys;
        }

        private void modelBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DbSet localDbSet = data[modelBox.SelectedValue.ToString()];
            localDbSet.Load();
            dataGrid.ItemsSource = localDbSet.Local;
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            db.SaveChanges();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e) {
            dataGrid.ItemsSource = null;
            DbSet localDbSet = data[modelBox.SelectedValue.ToString()];
            localDbSet.Load();
            dataGrid.ItemsSource = localDbSet.Local;
        }
    }
}
