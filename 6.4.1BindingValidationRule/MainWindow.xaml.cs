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

namespace _6._4._1BindingValidationRule
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Binding binding=new Binding("Value") { Source=this.slider};
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            RangeValidationRule rule = new RangeValidationRule();
            rule.ValidatesOnTargetUpdated = true;

            binding.ValidationRules.Add(rule);
            
            binding.NotifyOnValidationError=true;
            this.textBox.SetBinding(TextBox.TextProperty, binding);
            this.textBox.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(this.ValidationError));
            
        }

        void ValidationError(object sender, RoutedEventArgs e)
        {
            if (Validation.GetErrors(this.textBox).Count>0)
            {
                this.textBox.ToolTip = Validation.GetErrors(this.textBox)[0].ErrorContent.ToString();
            }
        }

    }

    //binding的数据校验类
    public class RangeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double d;
            if (double.TryParse(value.ToString(),out d)) 
            {
                if (d < 101 && d > -1)
                {
                    return new ValidationResult(true, null);
                }
                   
                
                    
            }
            return new ValidationResult(false, "Validation Failed 验证失败");
        }

       
    }
}
