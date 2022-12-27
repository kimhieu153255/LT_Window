using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for editWindow.xaml
    /// </summary>
    public partial class editWindow : Window
    {
        public delegate void CreditsValueChangeHandler(phoneInfo phone);
        public event CreditsValueChangeHandler phoneChanged;
        public phoneInfo phone { get; set; }
        public editWindow(phoneInfo data)
        {
            InitializeComponent();
            phone = data;
        }
        private void editWin_load(object sender, RoutedEventArgs e)
        {
            dataPhone.DataContext = phone;

            string exeFolder = AppDomain.CurrentDomain.BaseDirectory;
            var path = $"{exeFolder}..\\..\\..\\images";
            var directoryInfo = new DirectoryInfo(path);
            var files = from file in directoryInfo.GetFiles("*.*")
                        select new
                        {
                            Avatar = $"images/{file.Name}"
                        };

            foreach (var file in files)
            {
                Debug.WriteLine(file.Avatar);
            }

            // Hien thi anh len listview cho nguoi ta chon
            lvImg.ItemsSource = files;
        }

        private void cancleBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void displayView(object sender, RoutedEventArgs e)
        {
            lvImg.Visibility = Visibility.Visible;
        }


        private void imgUpdate(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = lvImg.SelectedItem;
            string newAvatar = item.Avatar;
            phone.img = newAvatar;
            phoneChanged?.Invoke(phone);
        }
    }
}
