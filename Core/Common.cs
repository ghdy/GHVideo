using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GHVideo.Core
{
    public static class Common
    {
        #region XElement Property AttchMethod

        public static XElement GetGHPropertyRootElement(this XElement element, string propertyName)
        {
            var rootPropertiesElementName = "Root" + propertyName;
            var rootElement = element.Element(rootPropertiesElementName);
            if (rootElement == null)
            {
                rootElement = new XElement(rootPropertiesElementName);
                element.Add(rootElement);
            }

            return rootElement;
        }

        public static void AddGHProperty(this XElement element, string propertyName, string propertyValue)
        {
            var rootElement = element.GetGHPropertyRootElement(propertyName);

            foreach (var item in rootElement.Elements(propertyName))
            {
                if (item.Value.Equals(propertyValue) == true)
                    item.Remove();
            }

            rootElement.Add(new XElement(propertyName, propertyValue));
        }

        public static void DelGHProperty(this XElement element, string propertyName, string propertyValue)
        {
            var rootElement = element.GetGHPropertyRootElement(propertyName);

            foreach (var item in rootElement.Elements(propertyName))
            {
                if (item.Value.Equals(propertyValue) == true)
                {
                    item.Remove();
                }
            }
        }

        public static IEnumerable<string> GetGHProperty(this XElement element, string propertyName)
        {
            var rootElement = element.GetGHPropertyRootElement(propertyName);

            foreach (var item in rootElement.Elements(propertyName))
            {
                yield return item.Value;
            }
        }

        #endregion

        #region XElement KeyValue AttchMethod

        public static XElement GetGHKeyValueRootElement(this XElement element, string keyTypeName)
        {
            var rootKeyValueElementName = "KT" + keyTypeName;
            var rootElement = element.Element(rootKeyValueElementName);
            if (rootElement == null)
            {
                rootElement = new XElement(rootKeyValueElementName);
                element.Add(rootElement);
            }

            return rootElement;
        }

        public static void AddGHKeyValue(this XElement element, string typeName, string key, string value)
        {
            var rootElement = element.GetGHPropertyRootElement(typeName);

            var valueElement = rootElement.Element(key);

            if (valueElement != null)
                valueElement.Remove();

            rootElement.Add(new XElement(key, value));
        }

        public static void DelGHKeyValue(this XElement element, string typeName, string key)
        {
            var rootElement = element.GetGHPropertyRootElement(typeName);

            var valueElement = rootElement.Element(key);

            valueElement?.Remove();
        }

        public static IEnumerable<KeyValuePair<string, string>> GetGHKeyValues(this XElement element, string typeName)
        {
            var rootElement = element.GetGHPropertyRootElement(typeName);

            foreach (var item in rootElement.Elements())
            {
                yield return new KeyValuePair<string, string>(item.Name.ToString(), item.Value);
            }
        }
        #endregion
        public static void KeyValueCollectionChanged(XElement? element, NotifyCollectionChangedEventArgs e)
        {
            if (element == null)
                return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (var kv in e.NewItems.Cast<KeyValuePair<string, Guid>>())
                        {
                            element.Add(new XElement(kv.Key, kv.Value));
                        }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var kv in e.OldItems.Cast<KeyValuePair<string, Guid>>())
                        {
                            var tmp = element.Element(kv.Key);
                            if (tmp != null) tmp.Remove();
                        }
                    break;
            }
        }

        public static void StringCollectionChanged(XElement? element, NotifyCollectionChangedEventArgs e)
        {
            if (element == null)
                return;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                        foreach (var kv in e.NewItems.Cast<string>())
                        {
                            element.Add(new XElement(kv));
                        }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null)
                        foreach (var kv in e.OldItems.Cast<string>())
                        {
                            var tmp = element.Element(kv);
                            if (tmp != null) tmp.Remove();
                        }
                    break;
            }
        }
    }
}

