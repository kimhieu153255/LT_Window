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
using System.IO;
using System.ComponentModel;

namespace WpfApp1
{
    public class phoneInfo
    {
        public String phoneName { get; set; }
        public String img { get; set; }
        public String manufacturer { get; set; }
        public int price { get; set; }
        public phoneInfo()
        {
            this.phoneName = "";
            this.img = "";
            this.manufacturer = "";
            this.price = 0;
        }
        public phoneInfo(String name, String img, String factur, int price)
        {
            this.phoneName = name;
            this.img = img;
            this.manufacturer = factur;
            this.price = price;
        }

    }
    public class Listphone: INotifyPropertyChanged
    {
        public BindingList<phoneInfo> li { get; set; }=new BindingList<phoneInfo>();
        public event PropertyChangedEventHandler? PropertyChanged;
        public void readListPhone()
        {
            String[] names = File.ReadAllLines("phoneNames.txt");
            String[] img = File.ReadAllLines("imgs.txt");
            String[] manufacturer = File.ReadAllLines("manufacturers.txt");
            String[] prices = File.ReadAllLines("prices.txt");
            for (int i = 0; i < names.Length; i++)
            {
                phoneInfo temp = new phoneInfo(names[i], img[i], manufacturer[i], Convert.ToInt32(prices[i]));
                li.Add(temp);
            }
        }
    }
    
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public Listphone list { get; set; } = new Listphone();

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void first(object sender, RoutedEventArgs e)
        {
            list.readListPhone();
            lvItems.ItemsSource = list.li;
        }

        private void deleteBtn_click(object sender, RoutedEventArgs e)
        {
            var item = (phoneInfo)lvItems.SelectedItem;
            if(item!=null)
                list.li.Remove(item);
        }

        private void editBtn_click(object sender, RoutedEventArgs e)
        {
            var phone = (phoneInfo)lvItems.SelectedItem;
            if (phone != null)
            {
                var index = (int)lvItems.SelectedIndex;
                var oldPhone = new phoneInfo(phone.phoneName, phone.img, phone.manufacturer, phone.price);
                var editScreen = new editWindow(phone);
                editScreen.phoneChanged += (phoneInfo phone)=>
        {
                    int i = lvItems.SelectedIndex;
                    list.li[i] = phone;
                };
                if (editScreen.ShowDialog() == false)
                {
                    list.li[index] = oldPhone;
                }
                else
                    list.li[index] = editScreen.phone;

            }
        }

        private void EditScreen_pathChanged(string newPath)
        {
            int i = lvItems.SelectedIndex;
            list.li[i].img = newPath;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addScreen = new addWindow();
            if (addScreen.ShowDialog() == true)
            {
                list.li.Add(addScreen.phone);
            }
        }
    }
}
