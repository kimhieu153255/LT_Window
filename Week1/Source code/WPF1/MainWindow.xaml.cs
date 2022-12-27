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

namespace WPF1
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
        List<string> _quotes;
        List<string> _pathImg;

        private void setData()
        {
            _quotes = new List<string>
            {
                "Remember it’s just a bad day, not a bad life.",
                "The path to success is to take massive, determined action. – Tony Robbins",
                "Keep going. Everything you need will come to you at the perfect time.",
                "Difficult roads always lead to beautiful destinations. – Zig Ziglar",
                "Everything you’ve ever wanted is on the other side of fear. — George Addair",
                "Success is what comes after you stop making excuses. – Luis Galarza",
                "You will never always be motivated. You have to learn to be disciplined.",
                "Life is 10% what happens to you and 90% how you react to it.",
                "Failure is not the opposite of success. Its part of success.",
                "Work hard in silence. Let success make the noise.",
            };
            _pathImg = new List<string>
            {
                "/images/quotes0.jpg",
                "/images/quotes1.jpg",
                "/images/quotes2.jpg",
                "/images/quotes3.jpg",
                "/images/quotes4.jpg",
                "/images/quotes5.jpg",
                "/images/quotes6.jpg",
                "/images/quotes7.jpg",
                "/images/quotes8.jpg",
                "/images/quotes9.jpg",
            };
        }

        private void next()
        {
            Random ran = new Random();
            int i = ran.Next(0, _quotes.Count());
            texQuote.Text = _quotes[i];
            var uri= new Uri(_pathImg[i], UriKind.Relative);
            var sour = new BitmapImage(uri);
            imgQuote.Source =  sour;

        }

        private void firstPage_Load(object sender, RoutedEventArgs e)
        {
            setData();
            next();
        }

        private void clickButton(object sender, RoutedEventArgs e)
        {
            next();
        }


    }
}
