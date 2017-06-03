using System;
using System.IO;
using Xamarin.Forms;
using EcoCasa.DB;
using EcoCasa.Droid;

[assembly: Dependency(typeof(FileHelper))]
namespace EcoCasa.Droid
{
    public class FileHelper : IFileHelper
    {
        
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

    }
}