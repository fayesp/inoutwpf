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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace command
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeCommand();
        }

        private RoutedCommand clearCmd=new RoutedCommand("Clear",typeof(MainWindow));
        private RoutedCommand New = new RoutedCommand("Add",typeof(Button));
        private void InitializeCommand()
        {
            //把命令赋值给命令源（发送者）并指定快捷键
            this.button.Command = this.clearCmd;
            this.clearCmd.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));

            this.button.CommandTarget = this.textbox;

            CommandBinding binding = new CommandBinding();
            binding.Command = this.clearCmd;
            binding.CanExecute += new CanExecuteRoutedEventHandler(binding_CanExecute);
            binding.Executed += new ExecutedRoutedEventHandler(binding_Executed);

            this.stackPanel.CommandBindings.Add(binding);

        }
        //当探测命令是否可以执行时，此方法被调用
        void binding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textbox.Text))
            { 
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute=true;
            }
            e.Handled = true;
        }

        //当命令送达目标后，此方法被调用
        void binding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.textbox.Clear();
            //避免继续向上传递而降低性能
            e.Handled = true;
        }
        void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(button.Name))
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
            e.Handled = true;
        }

        //当命令送达目标后，此方法被调用
        void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string name=e.Parameter.ToString()+e.Source.ToString();
            if (e.Parameter.ToString() == "Teacher")
            {
                this.textbox.Text = $"{name}teacher";
            }
            if (e.Parameter.ToString() == "Student")
            {
                this.textbox.Text = string.Format($"{name}" + "student");
            }
            //避免继续向上传递而降低性能
            e.Handled = true;
        }
    }
}
