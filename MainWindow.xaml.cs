using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Xml.Linq;
using GHVideo.Core;
using GHVideoApp.Controls;
using static GHVideoApp.GHVideoCommands;

namespace GHVideoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public App MyApp { get; private set; } = (App)Application.Current;

        private Actor? _currentActor = null;
        public Actor? CurrentActor
        {
            get { return this._currentActor; }
            set
            {
                this._currentActor = value;

                if (this.VideoCollectionViewSource != null)
                    this.VideoCollectionViewSource.View.Refresh();
            }
        }

        private string? _currentTag = string.Empty;
        public string? CurrentTag {
            get { return this._currentTag; }
            set
            {
                this._currentTag = value;

                if (this.VideoCollectionViewSource != null)
                    this.VideoCollectionViewSource.View.Refresh();
            }
        }


        public CollectionViewSource? VideoCollectionViewSource { get; private set; }

        public MainWindow()
        {

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;

            this.VideoCollectionViewSource = (CollectionViewSource)(FindResource("videoCVS"));
            this.VideoCollectionViewSource.Source = MyApp.VideoDocument.VideoCollection;

            this.VideoCollectionViewSource.Filter += VideoCollectionViewSource_Filter;

            this.CommandBindings.Add(new CommandBinding(GHVideoCommands.ActorAdd,
                new ExecutedRoutedEventHandler(GHVideoCommands.AddActorCommand_Executed)));
            this.CommandBindings.Add(new CommandBinding(GHVideoCommands.ActorClear,
                new ExecutedRoutedEventHandler(GHVideoCommands.ClearActorCommand_Executed),
                new CanExecuteRoutedEventHandler(GHVideoCommands.ClearActorCommand_CanExecute)));

            this.CommandBindings.Add(new CommandBinding(GHVideoCommands.TagClear,
                new ExecutedRoutedEventHandler(GHVideoCommands.ClearPropertyCommand_Executed),
                new CanExecuteRoutedEventHandler(GHVideoCommands.ClearPropertyCommand_CanExecute)));
        }

        private void VideoCollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
            Video? video = e.Item as Video;
            if (video != null)
            {
                if (this.CurrentActor != null)
                {
                    if (video.Actors.Contains(this.CurrentActor.GHName) == false)
                        e.Accepted = false;
                }

                if (string.IsNullOrEmpty(CurrentTag) == false)
                {
                    if (video.Tags.Contains(this.CurrentTag) == false)
                        e.Accepted = false;
                }

                if (this.CmdShowAllVideo.IsChecked == false)
                {
                    var sourcePath = MyApp.GetSourceDirectory(video.SourceID);
                    e.Accepted = Directory.Exists(sourcePath);
                }
            }
        }


        private void comboBoxActorSelecter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #region MenuEvents
        private void CmdFindAllVideo_Click(object sender, RoutedEventArgs e)
        {
            var array = MyApp.VideoSourceList;

            if (array != null)
            {
                foreach (var item in array)
                {
                    if (Directory.Exists(item.Location) == false)
                        continue;
                    var id = item.ID;
                    VideoSourceProcessor processor = new VideoSourceProcessor(id, item.Location);
                    var kvList = processor.GetFanAndFilepath();

                    foreach (var kv in kvList)
                    {
                        var oldVideo = MyApp.VideoDocument.GetVideo(kv.Key);
                        if (oldVideo != null)
                        {
                            if (File.Exists(oldVideo.Filepath) == false)
                                oldVideo.Filepath = kv.Value;

                            if (string.IsNullOrEmpty(oldVideo.SourceID) == true)
                                oldVideo.SourceID = item.ID;
                            continue;
                        }
                        else
                            MyApp.VideoDocument.VideoCollection.Add(new Video()
                            {
                                FanID = kv.Key.Split('-').First(),
                                ID = kv.Key,
                                Filepath = kv.Value,
                                SourceID = item.ID
                            });
                    }
                }
            }
            MyApp.VideoDocument.SaveGH();
        }

        private void CmdSortFan_Click(object sender, RoutedEventArgs e)
        {
            MyApp.VideoDocument.Sort();
            MyApp.VideoDocument.SaveGH();
            var path = MyApp.VideoDocument.FilePath;

            MyApp.VideoDocument = new(path);

            videoDataGrid.GetBindingExpression(DataGrid.ItemsSourceProperty).UpdateTarget();
        }

        private void CmdClearnFileName_Click(object sender, RoutedEventArgs e)
        {
            if (ConfigurationManager.AppSettings != null)
            {
                var temp = ConfigurationManager.AppSettings["DeleteArray"];
                if (temp != null)
                {
                    var array = temp.Split(';', StringSplitOptions.RemoveEmptyEntries);

                    var folderArray = ConfigurationManager.GetSection("Source") as List<GHDirectory>;

                    if (folderArray != null)
                    {
                        foreach (var item in folderArray)
                        {
                            if (Directory.Exists(item.Location) == false)
                                continue;
                            var files = Directory.GetFiles(item.Location, "*.mp4", SearchOption.AllDirectories);
                            foreach (var file in files)
                            {
                                var name = Path.GetFileName(file);
                                string newName = name;
                                foreach (var txt in array)
                                {

                                    newName = newName.Replace(txt, "");
                                }

                                newName = newName.Trim().Trim('-').Trim();

                                if (newName != name)
                                {
                                    var newFilepath = Path.Combine(Path.GetDirectoryName(file), newName);
                                    if (File.Exists(newFilepath) == false)
                                        File.Move(file, newFilepath);
                                }
                            }
                        }
                    }
                }
            }

            MessageBox.Show("文件名清理完毕！");
        }

        private void CmdFindRepeat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmdLoadVideo_Click(object sender, RoutedEventArgs e)
        {
            WindowAddVideo window = new WindowAddVideo();
            if (window.ShowDialog() == true)
            {
                try
                {
                    foreach (var item in window.VideoCollection)
                    {
                        var sourceID = item.SourceID;
                        var sourceDirectory = this.MyApp.GetSourceDirectory(sourceID);
                        var sourceFilepath = Path.Combine(sourceDirectory, Path.GetFileName(item.Filepath));

                        var oldVideoFP = item.Filepath;
                        item.Filepath = sourceFilepath;
                        this.MyApp.VideoDocument.VideoCollection.Add(item);

                        File.Move(oldVideoFP, sourceFilepath, true);
                    }
                }
                catch
                {

                }
            }

            this.MyApp.VideoDocument.SaveGH();
        }

        private void CmdShowAllVideo_Checked(object sender, RoutedEventArgs e)
        {

            if (this.VideoCollectionViewSource != null)
                this.VideoCollectionViewSource.View.Refresh();

            //MenuItem? mi = sender as MenuItem;
            //if (mi != null)
            //{
            //    if (mi.IsChecked == true)
            //    {

            //    }
            //    else
            //    { 
                
            //    }
            //}
        }
        #endregion

        #region videoDataGridEvents
        private void videoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var video = videoDataGrid.SelectedValue;
            this.mediaPlayer.Close();
            this.mediaPlayer.Source = null;
            this.sliderPosition.Value = 0;
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender == null)
                return;
            DataGrid? dataGrid = sender as DataGrid;

            if (dataGrid != null)
            {
                Video? video = dataGrid.SelectedItem as Video;

                if (video != null)
                {
                    if (radioBurronMediaElement.IsChecked == true)
                    {
                        mediaPlayer.Source = new Uri(video.Filepath);
                        mediaPlayer.Play();
                        this.IsPlaying = true;
                    }
                    else
                    {
                        mediaPlayer.Stop();
                        mediaPlayer.Source = null;
                        //用windows默认程序运行
                        video.OpenOnSystemSoft();
                    }
                }
            }
        }

        private void videoDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            var keyString = e.Key.ToString().ToUpper();
            if (keyString.Length == 1)
            {
                foreach (var video in MyApp.VideoDocument.Videos)
                {
                    if (video.FanID.StartsWith(keyString) == true)
                    {
                        //this.videoDataGrid.SelectedValue = video;
                        //this.videoDataGrid.BringIntoView();
                        //this.videoDataGrid.SelectedIndex = 60;
                        //this.videoDataGrid.ScrollIntoView();
                        return;
                    }
                }
            }
        }

        #endregion

        #region mediaPlayerEvents
        private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            this.sliderPosition.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.mediaPlayer.Position = TimeSpan.FromSeconds(sliderPosition.Value);
        }

        public bool IsPlaying { get; private set; } = false;

        private void mediaPlayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsPlaying == true)
            {
                this.mediaPlayer.Stop();
                this.IsPlaying = false;
            }
            else
            {
                this.mediaPlayer.Play();
                this.IsPlaying = true;
            }
        }

        #endregion
    }

}
