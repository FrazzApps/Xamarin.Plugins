using FrazzApps.Xamarin.DataStorer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.DataStorer.iOS;
using System.Threading.Tasks;
using System.IO;

[assembly: Dependency(typeof(DataStorer))]
namespace FrazzApps.Xamarin.DataStorer.iOS
{
    /// <summary>
    /// DataStorer Implementation
    /// </summary>
    public class DataStorer : IDataStorer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }


        public async Task<bool> SaveText(string filename, string text)
        {
            await Task.Delay(1);
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
            return true;
        }
        public async Task<string> LoadText(string filename)
        {
            await Task.Delay(1);
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            if (System.IO.File.Exists(filePath))
                return System.IO.File.ReadAllText(filePath);
            else
                return "";
        }
    }
}
