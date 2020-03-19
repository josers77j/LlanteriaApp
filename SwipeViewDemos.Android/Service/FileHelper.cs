using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SwipeViewDemos.Droid.Services;
using SwipeViewDemos.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace SwipeViewDemos.Droid.Services
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }

        /*
         para ios:
          public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string docFolder = 
                Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder =
                Path.Combine(docFolder, "..", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, fileName);
        }
    }
         
         */

        /*Para UWP:
           public string GetLocalFilePath(string fileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path,
                fileName);
        }
         */
    }
}