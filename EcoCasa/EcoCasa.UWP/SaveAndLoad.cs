using System;
using System.Threading.Tasks;
using Windows.Storage;
using EcoCasa.Util.XML;
using EcoCasa.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace EcoCasa.UWP
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public async Task SaveTextAsync(string filename, string text)
        {
            StorageFolder storage = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storage.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, text);
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync(filename);
            string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            return text;
        }

        public bool FileExists(string filename)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                localFolder.GetFileAsync(filename).AsTask().Wait();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}