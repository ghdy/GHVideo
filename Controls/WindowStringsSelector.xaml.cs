using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// WindowStringsSelector.xaml 的交互逻辑
    /// </summary>
    public partial class WindowStringsSelector : Window
    {
        //public App MyApp { get; private set; } = Application.Current as App;

        public ObservableCollection<string> SelectedItems { get; set; } = new();
        public ObservableCollection<string> AllItems { get; set; } = new();

        public string WindowTitle { get; set; }

        public WindowStringsSelector(string title, IEnumerable<string> allItems, IEnumerable<string> selectedItems)
        {
            this.WindowTitle = title;

            foreach (var item in allItems)
                this.AllItems.Add(item);
            foreach (var item in selectedItems)
                this.SelectedItems.Add(item);

            InitializeComponent();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            this.txtTitle.Content = this.WindowTitle;
        }

        private void listAllItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string? target = listAllItems.SelectedItem?.ToString();
            if (target != null && this.SelectedItems.Contains(target) == false)
            {
                this.SelectedItems.Add(target);
            }
        }

        private void listSelectedItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string? target = listSelectedItems.SelectedItem?.ToString();

            if (target != null)
            {
                this.SelectedItems.Remove(target);
            }
        }
    }
}
