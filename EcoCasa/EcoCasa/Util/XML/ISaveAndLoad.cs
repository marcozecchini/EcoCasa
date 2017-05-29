using System.Threading.Tasks;

namespace EcoCasa.Util.XML
{
    public interface ISaveAndLoad
    {
        Task SaveTextAsync(string filename, string text);
        Task<string> LoadTextAsync(string filename);
        bool FileExists(string filename);
    }
}