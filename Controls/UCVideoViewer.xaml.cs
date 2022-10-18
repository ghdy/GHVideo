using GHVideo.Core;
using System;
using System.Collections.Generic;
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

namespace GHVideoApp.Controls
{
    /// <summary>
    /// UCVideoViewer.xaml 的交互逻辑
    /// </summary>
    public partial class UCVideoViewer : UserControl
    {
        public App MyApp { get; private set; } = Application.Current as App;

        public Video Current
        {
            get { return (Video)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Current.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(Video), typeof(UCVideoViewer), new PropertyMetadata(null));

        public UCVideoViewer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;

            this.CommandBindings.Add(new CommandBinding(GHVideoCommands.ActorAdd,
                new ExecutedRoutedEventHandler(GHVideoCommands.AddActorCommand_Executed)));

            this.CommandBindings.Add(new CommandBinding(GHVideoCommands.TagAdd,
                new ExecutedRoutedEventHandler(GHVideoCommands.AddTagCommand_Executed)));

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            listActors.GetBindingExpression(ListView.ItemsSourceProperty).UpdateTarget();
        }


        private void btnDeleteVideo_Click(object sender, RoutedEventArgs e)
        {
            var video = this.Current;
            if (video != null)
            {
                if (File.Exists(video.Filepath) == true)
                    File.Delete(video.Filepath);
                else
                    MessageBox.Show("没有找到：" + video.Filepath);

                var result = MyApp.VideoDocument.VideoCollection.Remove(video);
            }
        }

        private void listActors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.Current == null) return;

            WindowStringsSelector selector = new WindowStringsSelector("选择演员",this.MyApp.ActorNames,this.Current.Actors);
            var result = selector.ShowDialog();
            if (result == true)
            { 
                this.Current.ActorCollection.Clear();
                foreach (var item in selector.SelectedItems)
                {
                    this.Current.ActorCollection.Add(item);
                }
            }
        }

        private void listTags_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.Current == null) return;

            WindowStringsSelector selector = new WindowStringsSelector("选择标签", this.MyApp.AllProperties.TagCollection, this.Current.Tags);
            var result = selector.ShowDialog();
            if (result == true)
            {
                this.Current.TagCollection.Clear();
                foreach (var item in selector.SelectedItems)
                {
                    this.Current.TagCollection.Add(item);
                }
            }
        }
    }
}
