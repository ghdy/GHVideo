using GHVideo.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Shell32;

namespace GHVideoApp
{
    public class ActorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            Actor actor = value as Actor;
            if (actor != null)
                result = actor.GHName;
            return result;
            //var actorIDs = (IEnumerable<string>)value;
            //if (Application.Current != null && actorIDs.Count() > 0)
            //{
            //    var actors = (Application.Current as App).ActorDocument.Actors;

            //    if (actors != null)
            //        foreach (var actor in actors)
            //        {
            //            foreach (var item in actorIDs)
            //            {
            //                if (actor.ID.ToString() == item)
            //                    result += actor.GHName + ";";
            //            }

            //        }
            //}
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Filepath2FilenameCovnerter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            Video video = value as Video;

            if (video != null)
            {
                if (video.Chinese.Length > 0)
                    result = video.Chinese;
                else if (video.Title.Length > 0)
                    result = video.Title;
                else
                    result = Path.GetFileNameWithoutExtension(video.Filepath);
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Filepath2ExistsConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            if (value != null)
            {
                result = File.Exists(value.ToString());
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
    public class Filepath2FileInfoConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "No FileInfo!";
            if (string.IsNullOrEmpty(value.ToString()) == false)
            {
                var fileinfo = new FileInfo(value.ToString());

                if (fileinfo.Exists == true)
                {
                    result = (fileinfo.Length / 1048576).ToString() + "MB";

                    //var sh = new ShellClass();
                    //Shell32.Folder folder = sh.NameSpace(fileinfo.FullName.Substring(0, fileinfo.FullName.LastIndexOf("\\")));
                    //Shell32.FolderItem folderItem = folder.ParseName(fileinfo.FullName.Substring(fileinfo.FullName.LastIndexOf("\\") + 1));

                    //StringBuilder sb = new();
                    //for (int i = 0; i < 30; i++)
                    //{
                    //    var str = folder.GetDetailsOf(folderItem, i);
                    //    sb.AppendLine(i.ToString() + ":" + str);
                    //}
                }
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
