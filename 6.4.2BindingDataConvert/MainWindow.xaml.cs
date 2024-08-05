using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _6._4._2BindingDataConvert
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            List<Plane> planes = new List<Plane>()
            {
                new Plane() {Category=Category.Bomber,Name="B-1",State=State.Unknown},
                new Plane() {Category=Category.Bomber,Name="B-2",State=State.Unknown},
                new Plane() {Category=Category.Fighter,Name="F-22",State=State.Unknown},
                new Plane() {Category=Category.Fighter,Name="Su-47",State=State.Unknown},
                //new Plane() {Category=Category.Bomber,Name="B-52",State=State.Unknown},
                //new Plane() {Category=Category.Fighter,Name="J-10",State=State.Unknown},
            };
            this.ListBoxPlane.ItemsSource = planes;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Plane item in ListBoxPlane.Items)
            {
                sb.AppendLine(string.Format($"Category={item.Category},Name={item.Name},State={item.State}"));
            }
            File.WriteAllText(@"D:\desktop\深入浅出wpf\PlaneList.txt", sb.ToString());
        }
    }

    #region Converter
    public class CategoryToSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Category c = (Category)value;
            switch (c) 
            { 
                case Category.Bomber :
                    //图片资源导入时需要设置生成为resource才可以使用，被url找到
                   return @"\Resources\微信图片_20230210195311.jpg";
                    //return "pack://application:,,,/6.4.2BindingDataConvert;component/Resources/微信图片_20230210195359.jpg";
                case Category.Fighter:
                    return @"\Resources\微信图片_20230210195359.jpg";
                //return "pack://application:,,,/6.4.2BindingDataConvert;component/Resources/微信图片_20230210195311.jpg";
                default:
                    return null;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class StateToNullableBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            State state = (State)value;
            switch (state)
            {
                case State.Avaliable:
                    return true; 
                case State.Locked:
                    return false;
                case State.Unknown:
                default:
                    return null;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? nb = (bool?)value;
            switch (nb)
            {
                case true:
                    return State.Avaliable;
                case false:
                    return State.Locked;
                case null:
                default:
                    return State.Unknown;
            }
            
        }
    }
    #endregion
    #region Plane类型
    //种类
    public enum Category
    { 
        Bomber,
        Fighter
    }

    //状态
    public enum State
    { 
        Avaliable,
        Locked,
        Unknown
    }

    //飞机
    public class Plane
    {
        public string Name { get; set; }
        public State State { get; set; }
        public Category Category { get; set; }
    }
    #endregion
}
