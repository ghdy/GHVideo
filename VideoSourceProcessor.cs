using GHVideo.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GHVideoApp
{
    public class VideoSourceProcessor
    {
        public string ID { get; private set; }
        public string Folder { get; private set; }
        public List<KeyValuePair<string, string>> ListFanAndFilepath { get; private set; } = new();
        public VideoSourceProcessor(string id,string directory)
        {
            //var directories =  Directory.GetDirectories(directory);
            this.ID = id;
            this.Folder = directory;
        }

        public List<KeyValuePair<string, string>> GetFanAndFilepath()
        {
            return Process(this.Folder);
        }

        public static KeyValuePair<string, string> GetFanAndID(string fileName)
        {
            Regex regex = new Regex(@"^(?<fan>[A-Z]{2,5})\-?(?<id>\d{3})\D", RegexOptions.Singleline);
            Match match = regex.Match(fileName.ToUpper());
            if (match.Success)
            {
                var key = match.Groups["fan"].ToString();
                var value = match.Groups["id"].ToString();
                return new KeyValuePair<string, string>(key, value);
            }
            else
                return new KeyValuePair<string, string>("", "");
        }

        public static string GetTitle(string fileName)
        {
            var array = fileName.Split('$', StringSplitOptions.RemoveEmptyEntries);
            if (array != null && array.Length > 1)
                return array[1].Trim();
            else
                return String.Empty;
        }

        private void SearchFileInPath(string path, string folderName, string fileName, ref List<string> projectPaths)
        {
            var dirs = Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly).ToList();  //获取当前路径下所有文件与文件夹
            var desFolders = dirs.FindAll(x => x.Contains(folderName)); //在当前目录中查找目标文件夹
            if (desFolders == null || desFolders.Count <= 0)
            {
                //当前目录未找到目标则递归
                foreach (var dir in dirs)
                {
                    SearchFileInPath(dir, folderName, fileName, ref projectPaths);
                }
            }
            else
            {
                //找到则添加至结果集
                projectPaths.Add(path + "\\" + folderName + "\\" + fileName);
            }
        }

        public static List<KeyValuePair<string, string>> Process(string directory)
        {
            if (Directory.Exists(directory) == false)
                return null;

            var result = new List<KeyValuePair<string, string>>();
            var files = Directory.GetFiles(directory, "*.mp4", SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                var kv = GetFanID(item);
                if (string.IsNullOrEmpty(kv.Key) == false)
                    result.Add(kv);
            }
            return result;
        }

        private static KeyValuePair<string, string> GetFanID(string filepath)
        {
            string fileName = Path.GetFileName(filepath);
            KeyValuePair<string, string> kv = VideoSourceProcessor.GetFanAndID(fileName);
            if (string.IsNullOrEmpty(kv.Key) == false)
                return new KeyValuePair<string, string>(String.Format("{0}-{1}", kv.Key, kv.Value), filepath);
            else
                return new KeyValuePair<string, string>();
        }
    }
}
