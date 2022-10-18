using GHVideo.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Xml.Linq;

namespace GHVideoApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<GHDirectory>? DirectoryList { get; private set; } = ConfigurationManager.GetSection("Source") as List<GHDirectory>;
        public ActorDocument ActorDocument { get; private set; }
        public AllPropertyDocument AllProperties { get; private set; }
        public VideoDocument VideoDocument { get; set; }

        public List<GHDirectory> VideoSourceList
        {
            get
            {
                return (List<GHDirectory>)ConfigurationManager.GetSection("Source");
            }
        }

        private Timer _timer = new Timer(10000);

        ObservableCollection<string> _actorNames = new();
        public ObservableCollection<string> ActorNames
        {
            get
            {
                if (_actorNames.Count != this.ActorDocument.ActorCollection.Count)
                {
                    _actorNames.Clear();
                    foreach (var item in this.ActorDocument.ActorCollection)
                    {
                        _actorNames.Add(item.GHName);
                    }
                }

                return _actorNames;
            }
        }

        public List<string> Races { get; private set; } = new()
        { "日本", "中国", "欧美", "非洲", };
        private void Init()
        {
            string file;

            #region Actors
            file = FileUtilities.Instance.ActorFilepath;
            this.ActorDocument = new ActorDocument(file);
            if (File.Exists(file) == false)
            {
                var actorCSV = Path.Combine(Environment.CurrentDirectory, "CSV", "actors.csv");
                var actorArray = File.ReadAllLines(actorCSV, System.Text.Encoding.UTF8);
                foreach (var item in actorArray)
                {
                    if (String.IsNullOrEmpty(item.Trim()) == false)
                        this.ActorDocument.ActorCollection.Add(new Actor(item.Trim(), "日本"));

                }
                this.ActorDocument.SaveGH();
            }
            #endregion

            #region AllProperties
            file = FileUtilities.Instance.PropertyFilepath;
            if (File.Exists(file) == false)
            {
                this.AllProperties = new AllPropertyDocument(file);

                //番号初始化
                var fanCSV = Path.Combine(Environment.CurrentDirectory, "CSV", "fans.csv");
                var fanArray = File.ReadAllLines(fanCSV, System.Text.Encoding.UTF8);
                foreach (var fan in fanArray)
                {
                    if (this.AllProperties.FanCollection.Contains(fan) == false)
                        this.AllProperties.FanCollection.Add(fan.Trim());
                }

                //标签初始化
                var tagCSV = Path.Combine(Environment.CurrentDirectory, "CSV", "tags.csv");
                var tagArray = File.ReadAllLines(tagCSV, System.Text.Encoding.UTF8);
                foreach (var tag in tagArray)
                {
                    if (this.AllProperties.TagCollection.Contains(tag) == false)
                        this.AllProperties.TagCollection.Add(tag.Trim());
                }

                //类型初始化
                var typeCSV = Path.Combine(Environment.CurrentDirectory, "CSV", "types.csv");
                var typeArray = File.ReadAllLines(typeCSV, System.Text.Encoding.UTF8);
                foreach (var type in typeArray)
                {
                    if (this.AllProperties.VideoTypeCollection.Contains(type) == false)
                        this.AllProperties.VideoTypeCollection.Add(type.Trim());
                }
                this.AllProperties.SaveGH();

            }
            else
                this.AllProperties = new AllPropertyDocument(XDocument.Load(file), file);
            #endregion

            #region VideoDocument
            file = FileUtilities.Instance.VideoFilepath;
            this.VideoDocument = new VideoDocument(file);
            #endregion
        }

        public string GetSourceDirectory(string sourceID)
        {
            foreach (var item in this.VideoSourceList)
            {
                if (item.ID == sourceID)
                    return item.Location;
            }

            return string.Empty;
        }

        public void SaveAllDocument()
        {
            if (this.AllProperties.IsChanged)
                this.AllProperties.SaveGH();

            if (this.VideoDocument.IsChanged)
                this.VideoDocument.SaveGH();

            if (this.ActorDocument.IsChanged)
                this.ActorDocument.SaveGH();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Init();

            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            this.SaveAllDocument();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            this._timer.Dispose();
            this.SaveAllDocument();
        }
    }

}
