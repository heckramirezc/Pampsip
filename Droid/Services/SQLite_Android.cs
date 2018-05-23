using System.IO;
using Xamarin.Forms;
using Pampsip.Droid;
using Pampsip.Interfaces;
using Pampsip.Droid.Services;

[assembly: Dependency(typeof(SQLite_Android))]

namespace Pampsip.Droid.Services
{
	public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "PampsipSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}