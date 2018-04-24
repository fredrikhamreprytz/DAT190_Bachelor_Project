using System;
using System.IO;
using DAT190_Bachelor_Project.Data;
using DAT190_Bachelor_Project.Droid.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace DAT190_Bachelor_Project.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
        public string GetLocalFilePath(string fileName)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
    }
}
