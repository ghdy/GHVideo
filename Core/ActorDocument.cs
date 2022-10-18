using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GHVideo.Core
{
    public class ActorDocument
    {
        string _filepath = String.Empty;
        readonly XDocument _document;

        public bool IsChanged { get; private set; } = false;
        public ActorDocument(string filepath)
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
        //public ActorDocument(XDocument doc, string filepath)
        //{

        //    _document = doc;
        //    Init(filepath);
        //}

        private void Init()
        {
            foreach (var item in this.Actors)
            {
                this.ActorCollection.Add(item);
            }
            this.ActorCollection.CollectionChanged += ActorCollection_CollectionChanged;
            _document.Changed += _document_Changed;
        }

        private void _document_Changed(object? sender, XObjectChangeEventArgs e)
        {
            this.IsChanged = true;
        }

        private void ActorCollection_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (var item in e.NewItems.Cast<Actor>())
                        {
                            if (_document.Root != null)
                                _document.Root.Add(item.Element);
                        }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var item in e.OldItems.Cast<Actor>())
                        {
                            item.RemoveGH();
                        }
                    break;

            }
        }

        public IEnumerable<Actor> Actors
        {
            get
            {
                if (_document.Root != null)
                    foreach (var item in _document.Root.Elements())
                    {
                        yield return new Actor(item);
                    }
            }
        }

        ObservableCollection<Actor> _actorCollection = new();

        public ObservableCollection<Actor> ActorCollection
        {
            get { return _actorCollection; }
        }

        public void SaveGH()
        {
            _document.Save(_filepath, SaveOptions.None);
            this.IsChanged = false;
        }
    }
}
