using System;
using System.IO;
using DAT190_Bachelor_Project.Data;
using DAT190_Bachelor_Project.iOS.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_IOS))]
namespace DAT190_Bachelor_Project.iOS.Data
{
    public class SQLite_IOS : ISQLite
    {
        public string GetLocalFilePath(string fileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, fileName);
        }
    }
}
