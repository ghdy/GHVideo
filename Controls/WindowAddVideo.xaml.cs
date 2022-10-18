using GHVideo.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GHVideoApp.Controls
{
    /// <summary>
    /// WindowAddVideo.xaml 的交互逻辑
    /// </summary>
    public partial class WindowAddVideo : Window
    {
        public ObservableCollection<Video> VideoCollection { get; private set; }
        public App MyApp { get; private set; } = (App)Application.Current;

        public WindowAddVideo()
        {
            this.VideoCollection = new ObservableCollection<Video>();
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpFolder_Click(object sender, RoutedEventArgs e)
        {
            this.VideoCollection.Clear();

            foreach (var folder in this.MyApp.VideoSourceList)
            {
                if(Directory.Exists(folder.Location) == false) continue;

                var directory = Directory.GetParent(folder.Location);
                VideoSourceProcessor processor = new VideoSourceProcessor(folder.ID, directory.FullName);

                processor.GetFanAndFilepath().ForEach(f =>
                {
                    var video = new Video()
                    {
                        FanID = f.Key.Split('-')[0],
                        ID = f.Key,
                        Filepath = f.Value,
                        SourceID = folder.ID,
                        Title = VideoSourceProcessor.GetTitle(f.Value)
                    };
                    this.VideoCollection.Add(video);
                });
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
