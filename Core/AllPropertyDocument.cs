using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GHVideo.Core
{
    public class AllPropertyDocument
    {
        const string VideoTypeElementName = "VideoTypes";
        const string TagElementName = "Tags";
        const string ActorElementName = "Actors";
        const string FanElementName = "Fans";

        public ObservableCollection<string> TagCollection { get; private set; } = new();
        public ObservableCollection<string> VideoTypeCollection { get; private set; } = new();
        public ObservableCollection<string> FanCollection { get; private set; } = new();

        private XDocument _document = new XDocument();

        public string Filepath { get; private set; }

        public bool IsChanged { get; private set; } = false;
        public AllPropertyDocument(string filepath)
        {
            this._document = new();

            this._document.Add(new XElement("Properties"));

            this.Filepath = filepath;

            init();
        }
        public AllPropertyDocument(XDocument doc,string filepath)
        {
            this._document = doc;
            this.Filepath=filepath;
            init();
        }

        #region Init Collection
        private void init()
        {
            if (this.Tags != null)
                foreach (var item in this.Tags)
                {
                    this.TagCollection.Add(item);
                }

            if (this.VideoTypes != null)
                foreach (var item in this.VideoTypes)
                {
                    this.VideoTypeCollection.Add(item);
                }

            if (this.Fans != null)
                foreach (var item in this.Fans)
                {
                    this.FanCollection.Add(item);
                }

            this.TagCollection.CollectionChanged += TagCollection_CollectionChanged;

            this.VideoTypeCollection.CollectionChanged += VideoTypeCollection_CollectionChanged;

            this.FanCollection.CollectionChanged += FanCollection_CollectionChanged;

            _document.Changed += _document_Changed;
        }

        private void _document_Changed(object? sender, XObjectChangeEventArgs e)
        {
            this.IsChanged = true;
        }

        private void FanCollection_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (_document.Root != null)
                Common.StringCollectionChanged(this._document.Root.Element(FanElementName), e);
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (var item in e.NewItems)
                        {
                            if (item != null)
                                this.AddFan(item.ToString());
                        }
                    break;
                    case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var item in e.OldItems)
                        {
                            this.DelTag(item.ToString());
                        }
                    break;
            }
        }

        private void VideoTypeCollection_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (_document.Root != null)
                Common.KeyValueCollectionChanged(_document.Root.Element(VideoTypeElementName), e);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (var item in e.NewItems)
                        {
                            if (item != null)
                                this.AddVideoType(item.ToString());
                        }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var item in e.OldItems)
                        {
                            this.DelVideoType(item.ToString());
                        }
                    break;
            }
        }

        private void TagCollection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_document.Root != null)
                Common.KeyValueCollectionChanged(_document.Root.Element(TagElementName), e);


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
                            this.DelTag(item.ToString());
                        }
                    break;
            }
        }
        #endregion

        public void SaveGH()
        {
            this._document.Save(Filepath, SaveOptions.None);
            this.IsChanged = false;
        }

        #region Child:Actors
        //public void AddActor(Guid id, string name)
        //{
        //    _document.Root.AddGHKeyValue(ActorElementName, name, id.ToString());
        //}

        //public IEnumerable<KeyValuePair<string, string>> Actors
        //{
        //    get
        //    {
        //        return _document.Root.GetGHKeyValues(ActorElementName);
        //    }
        //}

        //public void DelActor(string name)
        //{
        //    _document.Root.DelGHKeyValue(ActorElementName, name.ToString());
        //}
        #endregion

        #region Child:VideoTypes
        public void AddVideoType(string type)
        {
            if (_document.Root != null)
                _document.Root.AddGHProperty(VideoTypeElementName, type);
        }

        public IEnumerable<string>? VideoTypes
        {
            get
            {
                if (_document.Root != null)
                    return _document.Root.GetGHProperty(VideoTypeElementName);
                else
                    return null;
            }
        }

        public void DelVideoType(string type)
        {
            if (_document.Root != null)
                _document.Root.DelGHProperty(VideoTypeElementName, type);
        }
        #endregion

        #region Child:Tags
        public void AddTag(string name)
        {
            if (_document.Root != null)
                _document.Root.AddGHProperty(TagElementName, name);
        }

        public IEnumerable<string>? Tags
        {
            get
            {
                if (_document.Root != null)
                    return _document.Root.GetGHProperty(TagElementName);
                else return null;
            }
        }

        public void DelTag(string tag)
        {
            if (_document.Root != null)
                _document.Root.DelGHProperty(TagElementName, tag);
        }
        #endregion

        #region Child:Fans
        public void AddFan(string fanName)
        {
            if (_document.Root != null)
                _document.Root.AddGHProperty(FanElementName, fanName);
        }

        public IEnumerable<string>? Fans
        {
            get
            {
                if (_document.Root != null)
                    return _document.Root.GetGHProperty(FanElementName);
                else
                    return null;
            }
        }

        public void DelFan(string fan)
        {
            if (_document.Root != null)
                _document.Root.DelGHProperty(FanElementName, fan);
        }
        #endregion


    }
}
