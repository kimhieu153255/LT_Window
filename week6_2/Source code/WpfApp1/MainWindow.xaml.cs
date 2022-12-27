using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Fluent;

namespace WpfApp1
{
    public interface IArmy { 
        public String pathImg { get; set; }
        public String Name { get; set; }
        public int ATK { get; set; } 
        public int DEF { get; set; } 
    }
    public class Knight: IArmy,ICloneable
    {
        public String pathImg { get; set; } = "../../images/knight.jpg";
        public String Name { get; set; } = "Knight";
        public int ATK { get; set; } = 20;
        public int DEF { get; set; } = 12;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    public class Swordman: IArmy,ICloneable
    {
        public String pathImg { get; set; } = "../../images/swordman.jpg";
        public String Name { get; set; } = "Swordman";
        public int ATK { get; set; } = 15;
        public int DEF { get; set; } = 10;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    public class Pikeman: IArmy,ICloneable
    {
        public String pathImg { get; set; } = "../../images/pikeman.jpg";
        public String Name { get; set; } = "Pikeman";
        public int ATK { get; set; } = 8;
        public int DEF { get; set; } = 7;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    public class Factory
    {
        private static Factory? instance = null;
        private static Dictionary<String, IArmy> prototypes = new Dictionary<String, IArmy>();
        
        public static void Load(List<IArmy> armies)
        {
            foreach (IArmy army in armies)
                prototypes.Add(army.Name, army);
        }
        private Factory() { }
        public static Factory Instance()
        {
            if(instance == null)
                instance = new Factory();
            return instance;
        }
        public IArmy LapRap(String name)
        {
            IArmy army = (IArmy) (prototypes[name] as ICloneable)!.Clone();
            return army;
        }
    }
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public BindingList<IArmy> li = new BindingList<IArmy>();
        private void btnPikeman_Click(object sender, RoutedEventArgs e)
        {
            var factory = Factory.Instance();
            var pikeman = factory.LapRap("Pikeman");
            li.Add(pikeman);
        }

        private void btnSwordman_Click(object sender, RoutedEventArgs e)
        {
            var factory = Factory.Instance();
            var swordman = factory.LapRap("Swordman");
            li.Add(swordman);
        }

        private void btnKnight_Click(object sender, RoutedEventArgs e)
        {
            var factory = Factory.Instance();
            var knight = factory.LapRap("Knight");
            li.Add(knight);
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var knight = new Knight();
            var swordman = new Swordman();
            var pikeman = new Pikeman();
            List<IArmy> list = new List<IArmy> { knight, swordman, pikeman };
            Factory.Load(list);
            lv_Army.ItemsSource = li;
        }
    }
}
