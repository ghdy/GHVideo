using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GHVideoApp
{
    public class SourceSection : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            List<GHDirectory> myConfigObject = new List<GHDirectory>();

            foreach (XmlNode childNode in section.ChildNodes)
            {

                var result = new GHDirectory();
                foreach (XmlAttribute attrib in childNode.Attributes)
                {
                    if(attrib.Name == "ID")
                        result.ID = attrib.Value;
                    else if(attrib.Name == "Location")
                        result.Location = attrib.Value;
                }
                myConfigObject.Add(result);
            }
            return myConfigObject;
        }
    }

    public class GHDirectory
    {
        public string ID { get; set; }
        public string Location { get; set; }
    }
}
