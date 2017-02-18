using System;
using SQLite.Net;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using FamilyMenu.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FamilyMenu
{
	/// <summary>
	/// MenuEntry builds on SQLite.Net and represents a specific database, in our case, the MenuEntry DB.
	/// It contains methods for retrieval and persistance as well as db creation, all based on the 
	/// underlying ORM.
	/// </summary>
	public class MenuEntryDatabase
	{
		static object locker = new object ();

		SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="FamilyMenu.MenuEntryDatabase"/> MenuEntryDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public MenuEntryDatabase ()
		{
			try
			{
				database = DependencyService.Get<ISQLite> ().GetConnection ();

				// create the tables
				database.CreateTable<MenuEntry> ();
				database.CreateTable<Setting> ();
				database.CreateTable<Chef> ();
			}
			catch(Exception ex) {
				Debug.WriteLine ("ERROR: " + ex.Message);
			}
		}
		
		public List<MenuEntry> GetItems ()
		{
            lock (locker) {
				return (from i in database.Table<MenuEntry> () select i).ToList ();
            }
		}

		public async Task<List<MenuEntry>> GetWeek (DateTime currentSaterday)
		{
            Debug.WriteLine("Start GetWeek()");

            var startdatum = currentSaterday.Year + "-"
                + string.Format("{0:D2}", currentSaterday.Month) + "-"
                + string.Format("{0:D2}", currentSaterday.Day);

            var sv = new GetWeekWebService();
            var es = await sv.GetWeekAsync(startdatum);

            List<MenuEntry> lijstje = es.ToList<MenuEntry>();
            
			foreach (var me in lijstje) {
				var rc = SaveItem (me);
			}

			return lijstje;
		}

		public MenuEntry GetItem (int id)
		{
			lock (locker) {
				return database.Table<MenuEntry>().FirstOrDefault(x => x.ID == id);
			}
		}

		public MenuEntry GetItemByDatum (string datum)
		{
			lock (locker) {
				return database.Table<MenuEntry>().FirstOrDefault(x => x.Datum == datum);
			}
		}

		public int SaveItem (MenuEntry item)
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();

			lock (locker) {
                if (item.ID != 0) {
                    database.Update (item);
                    return item.ID;
                } else {
                    return database.Insert (item);
                }
            }
		}
		
		public int InsertItem (MenuEntry item)
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();

			lock (locker) {
				return database.Insert (item);
			}
		}

		public int DeleteItem(int id)
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();

			lock (locker) {
				return database.Delete<MenuEntry> (id);
            }
		}

		public async Task<List<MenuOmschrijving>> GetMenuItems ()
		{
			var sv = new GetMenuOmchrijvingenWebService ();
			var es = await sv.GetMenuOmschrijvingenAsync ();

			List<MenuOmschrijving> lijstje = es.ToList<MenuOmschrijving> ();

			return lijstje;
		}

		public async Task<List<DieetOmschrijving>> GetDieetmenuItems ()
		{
			var sv = new GetDieetmenuOmchrijvingenWebService ();
			var es = await sv.GetDieetmenuOmschrijvingenAsync ();

			List<DieetOmschrijving> lijstje = es.ToList<DieetOmschrijving> ();

			return lijstje;
		}

        #region Chefs
		public List<Chef> GetLocalChefs ()
		{
			Debug.WriteLine("Start GetLocalChefs()");

			lock (locker) {
				return (from i in database.Table<Chef> () select i).ToList ();
			}
		}

        public async Task<List<Chef>> GetChefs ()
		{
            Debug.WriteLine("Start GetChefs()");

            var sv = new GetChefsWebService();
            var es = await sv.GetChefsAsync();

            List<Chef> lijstje = es.ToList<Chef>();

			foreach (var chef in lijstje) {
				var rc = SaveChef (chef);
			}

            return lijstje;
		}

		public Chef GetChefByName (string chefsName)
		{
			lock (locker) {
				return database.Table<Chef>().FirstOrDefault(x => x.Name == chefsName);
			}
		}

		public int DeleteChef(int id)
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();

			lock (locker) {
				return database.Delete<Chef> (id);
			}
		}

		public int SaveChef (Chef _chef)
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();

			lock (locker) {
				var ch = GetChefByName (_chef.Name);

				if (ch == null) {
					return database.Insert (_chef);
				} else {
					database.Update (_chef);
					return _chef.ID;
				}
			}
		}

		#endregion Chefs
	}
}