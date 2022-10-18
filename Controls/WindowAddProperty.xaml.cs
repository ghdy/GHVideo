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
using System.Windows.Shapes;

namespace GHVideoApp.Controls
{
    /// <summary>
    /// WindowAddProperty.xaml 的交互逻辑
    /// </summary>
    public partial class WindowAddProperty : Window
    {
        IEnumerable<string> properties;
        public WindowAddProperty(IEnumerable<string> existsPeopertyList)
        {
            this.properties = existsPeopertyList;

            InitializeComponent();
        }
        public string NewProperty
        {
            get { return (string)GetValue(NewPropertyProperty); }
            set { SetValue(NewPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NewProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NewPropertyProperty =
            DependencyProperty.Register("NewProperty", typeof(string), typeof(WindowAddProperty), new PropertyMetadata(""));

        private void propertyValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = propertyValue.Text;
            this.existsPropertyList.Items.Clear();

            foreach (var item in properties)
            {
                if (item.Contains(text))
                    this.existsPropertyList.Items.Add(item);
            }
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            //this.DialogResult = false;
            propertyValue.Focus();
        }
    }
}
