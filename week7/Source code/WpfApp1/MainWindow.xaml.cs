using Fluent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
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
    
    public class Search: INotifyPropertyChanged
    {
        public int PhonePerPage { get; set; } = 3;
        public int TotalPages { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;
        public int TotalPhone { get; set; } = 1;
        public int Displayingphones { get; set; } = 0;

        public BindingList<phoneInfo> subPhones = new BindingList<phoneInfo>();
        public BindingList<phoneInfo> pagePhones = new BindingList<phoneInfo>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void UpdatePaging(BindingList<phoneInfo> allPhone)
        {
            subPhones = allPhone;
            TotalPhone = subPhones.Count;
            TotalPages = (TotalPhone / PhonePerPage) + (TotalPhone % PhonePerPage == 0 ? 0 : 1);

            pagePhones = new BindingList<phoneInfo>(subPhones.Skip((CurrentPage - 1) * PhonePerPage).Take(PhonePerPage).ToList());
            Displayingphones = pagePhones.Count();
        }
    }

    public partial class MainWindow : RibbonWindow, INotifyPropertyChanged
    {
        public Listphone list { get; set; } = new Listphone();
        public String Keyword { get; set; } = "";
        public Search search { get; set; } = new Search();
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void first(object sender, RoutedEventArgs e)
        {
            list.readListPhone();
            search.UpdatePaging(list.li);
            DataContext = this;
            pagingInfo.DataContext = search;
            lvItems.ItemsSource = search.pagePhones;
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

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var addScreen = new addWindow();
            if (addScreen.ShowDialog() == true)
            {
                list.li.Add(addScreen.phone);
            }
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            search.subPhones = new BindingList<phoneInfo>(list.li.Where(info => info.phoneName.ToLower().Contains(Keyword.ToLower())).ToList());
            search.UpdatePaging(search.subPhones);
            DataContext = this;
            lvItems.ItemsSource = search.pagePhones;
            
        }

        private void keywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            search.subPhones = new BindingList<phoneInfo>(list.li.Where(sv => sv.phoneName.ToLower().Contains(Keyword.ToLower())).ToList());
            lvItems.ItemsSource = search.subPhones;
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (search.CurrentPage < search.TotalPages)
            {
                search.CurrentPage++;
                search.UpdatePaging(search.subPhones);
                lvItems.ItemsSource = search.pagePhones;
            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            if (search.CurrentPage > 1)
            {
                search.CurrentPage--;
                search.UpdatePaging(search.subPhones);
                lvItems.ItemsSource = search.pagePhones;
            }
        }
    }
}
