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

namespace WpfApp1 {
    public partial class ClientWindow : Window {
        hotelDbEntities db = new hotelDbEntities();
        public ClientWindow() {
            InitializeComponent();
            datePickerFrom.DisplayDateStart = DateTime.Now;
            datePickerTo.DisplayDateStart = DateTime.Now;
            this.Title += App.CurrentUser.login;
        }

        private void roomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            orderRoomButton.IsEnabled = ((ListBox)sender).SelectedIndex != -1;
        }

        private void orderRoomButton_Click(object sender, RoutedEventArgs e) {
            if (datePickerTo.SelectedDate <=  DateTime.Now) { }
        }

        private void datePickerFrom_CalendarClosed(object sender, RoutedEventArgs e) {
            datePickerTo.DisplayDateStart = ((DatePicker)sender).SelectedDate + new TimeSpan(1, 0, 0, 0); // + 1 день
            if (datePickerTo.SelectedDate <= datePickerFrom.SelectedDate) {
                datePickerTo.SelectedDate = datePickerFrom.SelectedDate + new TimeSpan(1, 0, 0, 0); // + 1 день
            }
        }

        private void datePickerTo_CalendarClosed(object sender, RoutedEventArgs e) {
            if (datePickerFrom.SelectedDate != null && ((DatePicker)sender).SelectedDate <= datePickerFrom.SelectedDate) {
                ((DatePicker)sender).SelectedDate = (DateTime)(datePickerFrom.SelectedDate) + new TimeSpan(1, 0, 0, 0); // + 1 день
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            label1_Копировать1.Content = Convert.ToInt32(slider.Value);
        }

        private void findRoomButton_Click(object sender, RoutedEventArgs e) {
            roomsListView.Items.Clear();
            var rooms = db.rooms.ToList();
            var reservations = db.reservations.Where(
                reserv => reserv.date >= datePickerFrom.SelectedDate || 
                          reserv.date <= datePickerTo.SelectedDate
            );

            foreach ( var reservation in reservations) {
                rooms.Remove(rooms.Find(room => room.id == reservation.id));
            }
            foreach (var room in rooms) {
                roomsListView.Items.Add(
                    new ListViewItem().Content = "Комната " + room.id
                );
            }
        }
    }
}
