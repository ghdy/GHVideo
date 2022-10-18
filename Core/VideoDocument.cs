using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GHVideo.Core
{
    public class VideoDocument
    {
        string _filepath = String.Empty;
        public string FilePath { get { return _filepath; } }
        readonly XDocument _document;

        public bool IsChanged { get; private set; } = false;

        public VideoDocument(string filepath)
        {
            if (File.Exists(filepath) == true)
            {
                _document = XDocument.Load(filepath);
            }
            else
            {
                _document = new();
                _document.Add(new XElement("Items"));
            }
            _filepath = filepath;
            Init();
        }
        //public VideoDocument(XDocument doc, string filepath)
        //{
        //    _document = doc;
        //    _filepath = filepath;
        //    Init();
        //}

        public void Init()
        {
            this.VideoCollection.CollectionChanged -= VideoCollection_CollectionChanged;

            foreach (var item in this.Videos)
            {
                this.VideoCollection.Add(item);
            }
            this.VideoCollection.CollectionChanged += VideoCollection_CollectionChanged;

            _document.Changed -= _document_Changed;
            _document.Changed += _document_Changed;
        }

        private void _document_Changed(object? sender, XObjectChangeEventArgs e)
        {
            this.IsChanged = true;
        }

        public Video? GetVideo(string fanID)
        {
            var result = this.Videos.Where((video) =>
            {
                if (video.ID == fanID)
                    return true;
                else
                    return false;
            });

            if (result == null || result.Count() < 1)
                return null;
            else
                return result.First();
        }

        private void VideoCollection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (var item in e.NewItems.Cast<Video>())
                        {
                            if (_document != null && _document.Root != null)
                                _document.Root.Add(item.Element);
                        }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var item in e.OldItems.Cast<Video>())
                        {
                            item.RemoveGH();
                        }
                    break;

            }
        }

        public IEnumerable<Video> Videos
        {
            get
            {
                if (_document != null && _document.Root != null)
                    foreach (var item in _document.Root.Elements())
                    {
                        yield return new Video(item);
                    }
            }
        }

        readonly ObservableCollection<Video> _videoCollection = new();

        public ObservableCollection<Video> VideoCollection
        {
            get { return _videoCollection; }
        }

        public void SaveGH()
        {
            try
            {
                _document.Save(_filepath, SaveOptions.None);
                this.IsChanged = false;
            }
            catch (Exception ex)
            { 
            
            }
        }

        public void Sort()
        {
            _document.Root.ReplaceNodes(_document.Root.Elements().OrderBy(el => el.Attribute("ID").Value));
        }
    }
}
