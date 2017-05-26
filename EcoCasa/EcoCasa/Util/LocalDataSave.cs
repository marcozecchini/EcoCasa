using System;
using System.Threading.Tasks;
using PCLStorage;

namespace EcoCasa.Util
{
    public class LocalDataSave
    {

        public const string CODE_STRING = "CODE : ";

        public async void createFile()
        {
            String filename = "UserData.txt";
            IFolder folder = FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
        }

        public async Task<bool> existFile(string fileName)
        {
            IFolder folder = FileSystem.Current.LocalStorage;
            ExistenceCheckResult fileExist = await folder.CheckExistsAsync(fileName);
            if (fileExist == ExistenceCheckResult.FileExists) return true;
            return false;
        }


    }
}
