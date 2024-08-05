using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _6._5MultiBinding
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetMultiBinding();
        }

        private void SetMultiBinding()
        {
            Binding b1 = new Binding("Text") { Source = textbox1 };  
            Binding b2 = new Binding("Text") { Source = textbox2 };  
            Binding b3 = new Binding("Text") { Source = textbox3 };  
            Binding b4 = new Binding("Text") { Source = textbox4 };  
            MultiBinding multiBinding = new MultiBinding() { Mode=BindingMode.OneWay};

            multiBinding.Bindings.Add(b1);
            multiBinding.Bindings.Add(b2);
            multiBinding.Bindings.Add(b3);
            multiBinding.Bindings.Add(b4);

            multiBinding.Converter=new LogonMultiBindingConverter();

            this.button.SetBinding(Button.IsEnabledProperty, multiBinding);
        }
    }

    public class LogonMultiBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!values.Cast<string>().Any(text=>string.IsNullOrEmpty(text)) 
                && values[0].ToString() == values[1].ToString()
                && values[2].ToString() == values[3].ToString())
            {
                return true;
            }   
            else { return false; }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
