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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private ListQuotes li = new ListQuotes();
        public void first(object sender, RoutedEventArgs e)
        {
            li.setData();
            Quote t = li.next();
            texQuote.Text = t.TextQuote;
            var uri = new Uri(t.PathImg, UriKind.Relative);
            imgQuote.Source = new BitmapImage(uri);

        }
        public void next(object sender,RoutedEventArgs e)
        {
            Quote t = li.next();
            texQuote.Text = t.TextQuote;
            var uri = new Uri(t.PathImg, UriKind.Relative);
            imgQuote.Source = new BitmapImage(uri);
        }
        public class Quote
        {
            public string TextQuote { get; set; }
            public string PathImg { get; set; }
            public Quote()
            {
                TextQuote = "";
                PathImg = "";
            }
            public Quote(string text, string path)
            {
                TextQuote = text;
                PathImg = path;
            }
        }
        public class ListQuotes 
        {
            public List<Quote> Quotes { get; set; }
            public ListQuotes()
            {
                this.Quotes = new List<Quote>();
            }
            public void setData()
            {
                string quote = "../../../quotes.txt";
                string path = "../../../pathQuotes.txt";
                string[] q = File.ReadAllLines(quote);
                string[] p = File.ReadAllLines(path);
                for (int i = 0; i < q.Length; i++)
                {
                    Quote temp = new Quote(q[i], p[i]);
                    this.Quotes.Add(temp);
                }
            }
            public Quote next()
            {
                Random random = new Random();
                int ind =random.Next(0,this.Quotes.Count);
                return this.Quotes[ind];
            }
        }
    }
}
