using System.IO;
using Windows.Storage;
using EcoCasa.DB;
using EcoCasa.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace EcoCasa.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}