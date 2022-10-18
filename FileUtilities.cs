using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHVideoApp
{
    public class FileUtilities
    {
        static FileUtilities _instance = new FileUtilities();
        public static FileUtilities Instance { get { return _instance; } }

        public FileUtilities()
        {
            var tmp = Path.Combine(Environment.CurrentDirectory, "Xml");
            if(Directory.Exists(tmp) == false) Directory.CreateDirectory(tmp);
        }

        public string ActorFilepath { get { return Path.Combine(Environment.CurrentDirectory, "Xml", "Actor.xml"); } }
        public string VideoFilepath { get { return Path.Combine(Environment.CurrentDirectory, "Xml", "Video.xml"); } }
        //public string TypeFilepath { get { return Path.Combine(Environment.CurrentDirectory, "Xml", "Type.xml"); } }
        public string PropertyFilepath { get { return Path.Combine(Environment.CurrentDirectory, "Xml", "Property.xml"); } }
        public string ResourceFilepath { get { return Path.Combine(Environment.CurrentDirectory, "Xml", "Resource.txt"); } }
        
    }
}
