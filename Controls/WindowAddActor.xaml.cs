using GHVideo.Core;
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
    /// WindowAddActor.xaml 的交互逻辑
    /// </summary>
    public partial class WindowAddActor : Window
    {
        public bool IsAddNew { get; private set; } = false;
        public Actor Current { get; set; }

        public ObservableCollection<string> TagCollection { get; set; } = new();
        public ObservableCollection<string> NameCollection { get; set; } = new();
        public List<string> Races { get; set; } = new();

        public string Race { get; set; } = "日本";

        public AllPropertyDocument Properties { get; private set; }
        public WindowAddActor()
        {
            IsAddNew = true;
            InitializeComponent();
            Current = new() { GHName = "" };
            Properties = ((App)Application.Current).AllProperties;
            Races = ((App)Application.Current).Races;
        }

        public WindowAddActor(Actor actor)
        {

            IsAddNew = false;
            InitializeComponent();
            Current = actor;

            foreach (var item in actor.Tags)
            {
                TagCollection.Add(item);
            }
            Properties = ((App)Application.Current).AllProperties;
            Races = ((App)Application.Current).Races;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.New,
                new ExecutedRoutedEventHandler(AddNameCommand_Executed),
                new CanExecuteRoutedEventHandler(AddNameCommand_CanExecute)));

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete,
                new ExecutedRoutedEventHandler(DelNameCommand_Executed),
                new CanExecuteRoutedEventHandler(DelNameCommand_CanExecute)));
        }

        #region CommandExecuted
        private void AddNameCommand_Executed(object? sender, ExecutedRoutedEventArgs e)
        {
            var name = e.Parameter.ToString();
            if (name != null)
            {
                this.NameCollection.Add(name);
                this.Current.AddActorName(name);
            }
        }

        private void AddNameCommand_CanExecute(object? sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (e.Parameter != null && String.IsNullOrEmpty(e.Parameter.ToString().Trim()) == false)
                e.CanExecute = true;
        }

        private void DelNameCommand_Executed(object? sender, ExecutedRoutedEventArgs e)
        {
            var name = e.Parameter.ToString();
            if (name != null)
            {
                this.NameCollection.Remove(name);
                this.Current.DelActorName(name);
            }
        }

        private void DelNameCommand_CanExecute(object? sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            if (e.Parameter != null)
                e.CanExecute = true;
        }
        #endregion

        #region 控件事件处理
        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e != null)
            {
                var tmp = e.OriginalSource.ToString();
                if (tmp != null)
                    this.Current.GHName = tmp;
            }
        }

        private void listTags_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.Current == null) return;

            WindowStringsSelector selector = new WindowStringsSelector("选择标签", this.Properties.TagCollection, this.TagCollection);
            var result = selector.ShowDialog();
            if (result == true)
            {
                this.TagCollection.Clear();
                foreach (var item in selector.SelectedItems)
                {
                    this.TagCollection.Add(item);
                }
            }
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()) == true)
            {
                MessageBox.Show("请输入姓名！");
                return;
            }

            this.Current.Race = this.Race;

            this.DialogResult = true;
        }

        private void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        #endregion
    }
}
