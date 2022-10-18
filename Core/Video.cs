using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GHVideo.Core
{
    public enum ShowWindowCommands : int
    {
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,    //用最近的大小和位置显示，激活
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_MAX = 10
    }

    public class Video : INotifyPropertyChanged
    {
        const string VideoTypeElementName = "VideoType";
        const string TagElementName = "Tag";
        const string ActorElementName = "Actor";
        const string VideoElementName = "Video";

        const string AttrID = "ID";
        const string AttrFanID = "FanID";
        const string AttrFilepath = "Filepath";
        const string AttrSourceID = "SourceID";
        const string AttrTitle = "Title";
        const string AttrChinese = "Chinese";
        const string AttrAllActors = "AllActors";
        const string AttrAllTags = "AllTags";
        const string AttrIsFavourite = "IsFavourite";
        const string AttrSubtitle = "Subtitle";

        public ObservableCollection<string> ActorCollection { get; private set; } = new();
        public ObservableCollection<string> TagCollection { get; private set; } = new();

        XElement _self;
        public XElement Element { get { return _self; } }

        public event PropertyChangedEventHandler? PropertyChanged;


        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
           IntPtr hwnd,
           string lpszOp,
           string lpszFile,
           string lpszParams,
           string lpszDir,
           ShowWindowCommands FsShowCmd
           );

        public Video()
        {
            _self = new XElement(VideoElementName);
            Init();
        }

        public Video(XElement element)
        {
            _self = element;
            Init();
        }

        public void RemoveGH()
        {
            this._self.Remove();
        }

        public void OpenOnSystemSoft()
        {
            if (File.Exists(this.Filepath) == true)
                ShellExecute(IntPtr.Zero, "open", this.Filepath, "", "", ShowWindowCommands.SW_SHOWNORMAL);
        }

        #region Init Methods and Events
        private void Init()
        {
            this.ActorCollection.CollectionChanged -= ActorCollection_CollectionChanged;

            if (this.Actors.Count() != this.ActorCollection.Count)
            {
                this.ActorCollection.Clear();
                foreach (var item in this.Actors)
                {
                    this.ActorCollection.Add(item);
                }
            }

            this.ActorCollection.CollectionChanged += ActorCollection_CollectionChanged;

            this.TagCollection.CollectionChanged -= TagCollection_CollectionChanged;

            if (this.Tags.Count() != this.TagCollection.Count)
            {
                this.TagCollection.Clear();
                foreach (var item in this.Tags)
                {
                    this.TagCollection.Add(item);
                }
            }

            this.TagCollection.CollectionChanged += TagCollection_CollectionChanged;

        }

        private void ActorCollection_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (var item in e.NewItems)
                        {
                            if (item != null)
                                this.AddActor(item.ToString());
                        }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var item in e.OldItems)
                        {
                            if (item != null)
                                this.DelActor(item.ToString());
                        }
                    break;
            }
            this.NotifyPropertyChanged(AttrAllActors);
        }

        private void TagCollection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (var item in e.NewItems)
                        {
                            if (item != null)
                                this.AddTag(item.ToString());
                        }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var item in e.OldItems)
                        {
                            if (item != null)
                                this.DelTag(item.ToString());
                        }
                    break;
            }
            this.NotifyPropertyChanged(AttrAllTags);
        }


        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Child:Actor
        public void AddActor(string? name)
        {
            if (name != null && string.IsNullOrEmpty(name) == false)
                _self.AddGHProperty(ActorElementName, name);

            NotifyPropertyChanged(AttrAllActors);
        }

        public IEnumerable<string> Actors
        {
            get
            {
                return _self.GetGHProperty(ActorElementName);
            }
        }

        public void DelActor(string? actor)
        {
            if (actor != null && string.IsNullOrEmpty(actor) == false)
                _self.DelGHProperty(ActorElementName, actor);
            NotifyPropertyChanged(AttrAllActors);
        }
        #endregion

        #region Child:VideoType
        public void AddVideoType(string type)
        {
            _self.AddGHProperty(VideoTypeElementName, type);
        }

        public IEnumerable<string> VideoTypes
        {
            get
            {
                return _self.GetGHProperty(VideoTypeElementName);
            }
        }

        public void DelVideoType(string type)
        {
            _self.DelGHProperty(VideoTypeElementName, type);
        }
        #endregion

        #region Child:Tag
        public void AddTag(string name)
        {
            _self.AddGHProperty(TagElementName, name);
        }

        public IEnumerable<string> Tags
        {
            get
            {
                return _self.GetGHProperty(TagElementName);
            }
        }

        public void DelTag(string tag)
        {
            _self.DelGHProperty(TagElementName, tag);
        }
        #endregion

        #region Attribute:ID
        public string ID
        {
            get
            {
                var attr = _self.Attribute(AttrID);
                if (attr == null)
                    return String.Empty;
                else
                    return attr.Value;
            }
            set
            {
                _self.SetAttributeValue(AttrID, value);
                NotifyPropertyChanged("ID");
            }
        }
        #endregion

        #region Attribute:FanID
        public string FanID
        {
            get
            {
                var attr = _self.Attribute(AttrFanID);
                if (attr == null)
                    return String.Empty;
                else
                    return attr.Value;
            }
            set
            {
                _self.SetAttributeValue(AttrFanID, value);
                NotifyPropertyChanged(AttrFanID);
            }
        }
        #endregion

        #region Attribute:Filepath
        public string Filepath
        {
            get
            {
                var attr = _self.Attribute(AttrFilepath);
                if (attr == null)
                    return "";
                else
                    return attr.Value;
            }
            set
            {
                _self.SetAttributeValue(AttrFilepath, value);
                NotifyPropertyChanged(AttrFilepath);
            }
        }
        #endregion

        #region Attribute:Folder
        public string SourceID
        {
            get
            {
                var attr = _self.Attribute(AttrSourceID);
                if (attr == null)
                    return "";
                else
                    return attr.Value;
            }
            set
            {
                _self.SetAttributeValue(AttrSourceID, value);
                NotifyPropertyChanged(AttrSourceID);
            }
        }
        #endregion

        #region Attribute:Title
        public string Title
        {
            get
            {
                var chinese = this.Chinese;
                if (string.IsNullOrEmpty(chinese.Trim()) == true)
                    return Path.GetFileNameWithoutExtension(this.Filepath);
                else
                    return chinese;
            }
            set
            {
                this.Chinese = value;
            }
        }
        #endregion

        #region Attribute:Chinese
        public string Chinese
        {
            get
            {
                var attr = _self.Attribute(AttrChinese);
                if (attr == null)
                    return "";
                else
                    return attr.Value;
            }
            set
            {
                _self.SetAttributeValue(AttrChinese, value);
                NotifyPropertyChanged(AttrChinese);
                NotifyPropertyChanged(AttrTitle);
            }
        }
        #endregion

        #region Attribute:IsFavourite
        public bool IsFavourite
        {
            get
            {
                var attr = _self.Attribute(AttrIsFavourite);
                if (attr == null)
                    return false;
                else
                    return bool.Parse(attr.Value);
            }
            set
            {
                _self.SetAttributeValue(AttrIsFavourite, value);
                NotifyPropertyChanged(AttrIsFavourite);
            }
        }
        #endregion

        #region Attribute:have Subtitle
        public bool Subtitle
        {
            get
            {
                var result = false;
                var attr = _self.Attribute(AttrSubtitle);
                if (attr == null)
                {
                    var tmp = this.Filepath + " " + this.Title;
                    Regex regex = new Regex(@"[-,_, ]CH");

                    if (tmp.Contains("中文") == true || regex.IsMatch(tmp) == true)
                    {
                        result = true;
                        this.Subtitle = true;
                    }
                    return result;
                }
                else
                    return bool.Parse(attr.Value);
            }
            set
            {
                _self.SetAttributeValue(AttrSubtitle, value);
                NotifyPropertyChanged(AttrSubtitle);
            }
        }
        #endregion

        #region Attribute:AllActors
        public string AllActors
        {
            get
            {
                string result = "";
                foreach (var item in this.Actors)
                {
                    result += item + ";";
                }
                return result;
            }
        }
        #endregion


        #region Attribute:AllTags
        public string AllTags
        {
            get
            {
                string result = "";
                foreach (var item in this.Tags)
                {
                    result += item + ";";
                }
                return result;
            }
        }
        #endregion
    }
}
