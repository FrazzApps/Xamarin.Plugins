using FrazzApps.Xamarin.DataStorer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.DataStorer.WinPhone;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;

[assembly: Dependency(typeof(DataStorer))]
namespace FrazzApps.Xamarin.DataStorer.WinPhone
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


        public async Task<string> LoadText(string filename)
        {
            String result = "";
            try
            {
                //var task = LoadTextAsync(filename);
                //task.Wait(); // HACK: to keep Interface return types simple (sorry!)
                //return task.Result;
                result = await LoadTextAsync(filename);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception loading " + filename + ": " + ex.Message);
                if (ex.InnerException != null)
                    System.Diagnostics.Debug.WriteLine("\t" + ex.InnerException.Message);
            }
            return result;
        }
        private async Task<string> LoadTextAsync(string filename)
        {
            String result = "";
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (local != null)
            {
                try
                {

                    result =  await TimeoutAfterAsync<string>(GetFileAsync(filename), TimeSpan.FromSeconds(5));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }


        private async Task<string> GetFileAsync(string filename)
        {
            String result = "";
            try
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await local.GetItemAsync(filename);
                //return file.Path;
                using (StreamReader streamReader = new StreamReader(file.Path))
                {
                    var text = streamReader.ReadToEnd();
                    result = text;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetFileAsync Exception:" + ex.Message);
            }
            return result;
        }

        private async Task<TResult> TimeoutAfterAsync<TResult>(Task<TResult> task, TimeSpan timeout)
        {
            // var result = await Task.WhenAny(task, Task.Delay(timeout));
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                // Task completed within timeout.
                return task.GetAwaiter().GetResult();
            }
            else
            {
                // Task timed out.
                throw new TimeoutException();
            }
        }


        public async Task<bool> SaveText(string filename, string text)
        {
            bool result = false;
            //     System.Diagnostics.Debug.WriteLine("Getting folder");
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                //     System.Diagnostics.Debug.WriteLine("Getting file - " + filename);
                var file = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                //    System.Diagnostics.Debug.WriteLine("Getting StreamWriter - " + filename);
                using (StreamWriter writer = new StreamWriter(await file.OpenStreamForWriteAsync()))
                {
                    //       System.Diagnostics.Debug.WriteLine("Writting file- " + filename);
                    writer.Write(text);
                    //      System.Diagnostics.Debug.WriteLine("file written");
                }
                //   System.Diagnostics.Debug.WriteLine(filename + "  DONE");
#if DEBUG
                string test = await GetFileAsync(filename);

#endif
                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SaveText Exception:" + ex.Message);
                result = false;
            }
            return result;
        }


    }
}
