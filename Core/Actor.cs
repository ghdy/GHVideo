using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace GHVideo.Core
{
    public class Actor : INotifyPropertyChanged
    {
        const string ActorElementName = "Actor";
        const string TagElementName = "Tag";
        const string AttrNameRace = "Race";
        const string AttrID = "ID";
        const string AttrGHName = "Name"; 
        XElement _self;

        public event PropertyChangedEventHandler? PropertyChanged;

        public XElement Element { get { return _self; } }

        public Actor() 
        {
            _self = new(ActorElementName);
        }
        public Actor(string name, string race)
        {

            _self = new(ActorElementName);
            //ID = id;
            Race = race;
            GHName = name;
        }

        public Actor(XElement element)
        {
            _self = element;
        }

        public void RemoveGH()
        {
            this._self.Remove();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Child:ActorName
        public void AddActorName(string name)
        {
            _self.AddGHProperty(ActorElementName, name);
        }

        public IEnumerable<string> ActorNames
        {
            get
            {
                return _self.GetGHProperty(ActorElementName);
            }
        }

        public void DelActorName(string name)
        {
            _self.DelGHProperty(ActorElementName, name);
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

        #region Attribute:Race
        public string Race
        {
            get
            {
                var attr = _self.Attribute(AttrNameRace);
                if (attr == null)
                    return "»’±æ";
                else
                    return attr.Value;
            }
            set
            {
                _self.SetAttributeValue(AttrNameRace, value);
                this.NotifyPropertyChanged(Race);
            }
        }
        #endregion

        #region Attribute:GHName
        public string GHName
        {
            get
            {
                var attr = _self.Attribute(AttrGHName);
                if (attr == null)
                    return string.Empty;
                else
                    return attr.Value;
            }
            set
            {
                _self.SetAttributeValue(AttrGHName, value);
                this.NotifyPropertyChanged("GHName");
            }
        }
        #endregion
    }
}
