using System;
using SQLite.Net;

namespace FamilyMenu
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

