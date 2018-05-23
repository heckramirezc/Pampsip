using System;
using System.Collections.Generic;
using System.Linq;
using Pampsip.Interfaces;
using Pampsip.Models.SQLite;
using SQLite;
using Xamarin.Forms;

namespace Pampsip.Data
{
    public class PampsipDatabase
    {
        static object locker = new object();
        SQLiteConnection database;
		public PampsipDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
           
            database.CreateTable<usuarios>();
        }

        public usuarios GetUser(string email)
        {
            lock (locker)
            {
                return database.Table<usuarios>().FirstOrDefault(x => x.email == email);
            }
        }

        public usuarios GetEmailUser(int id)
        {
            lock (locker)
            {
                return database.Table<usuarios>().FirstOrDefault(x => x.id == id);
            }
        }

        public int InsertUsuario(usuarios usuario)
        {
            lock (locker)
            {                
                    return database.Insert(usuario);
            }
        }

        public int UpdateUsuario(usuarios usuario)
        {
            lock (locker)
            {
                database.Update(usuario);
                return usuario.id;
            }
        }        
    }
}
