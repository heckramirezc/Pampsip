using System;
using SQLite;

namespace Pampsip.Interfaces
{
	public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
