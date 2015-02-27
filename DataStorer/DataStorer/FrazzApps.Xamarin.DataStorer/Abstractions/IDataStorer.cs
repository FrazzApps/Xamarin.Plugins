using System;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.DataStorer.Abstractions
{
    /// <summary>
    /// DataStorer Interface
    /// </summary>
    public interface IDataStorer
    {
        Task<bool> SaveText(string filename, string text);
        Task<string> LoadText(string filename);
    }
}
