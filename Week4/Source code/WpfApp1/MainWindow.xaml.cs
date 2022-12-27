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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
    public class Listphone
    {
        public List<phoneInfo> li { get; set; }
        
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
        public Listphone()
        {
            li = new List<phoneInfo>();
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Listphone list= new Listphone();
        public void first(object sender, RoutedEventArgs e)
        {
            list.readListPhone();
            lvItems.ItemsSource = list.li;
        }

        private void lvItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
   
}
