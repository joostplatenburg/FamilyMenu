using System;
using Xamarin.Forms;
using FamilyMenu.Views;
using System.Diagnostics;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace FamilyMenu
{
	public class App : Application
	{
		public static MasterDetailPage MDPage;
		public static MenuEntryDatabase database;

		public static string DeviceName;

		public static string urlDatabase = "http://www.platenburg.eu/php/FamilyMenu/FamilyMenu.sqlite";
		public static string databaseName = "";
		public static string updateChefStm = "http://www.platenburg.eu/php/FamilyMenu/updateChef.php?id={0}&name={1}&dn='{2}'";
		public static string deleteChefStm = "http://www.platenburg.eu/php/FamilyMenu/deleteChef.php?id={0}";

		public App ()
		{
			Debug.WriteLine ("Start App(), "+Device.OS.ToString());

            var device = Resolver.Resolve<IDevice>();

 			MainPage = new NavigationPage(new MainListView ());
		}

		protected override void OnStart ()
		{
			Debug.WriteLine ("Start App.OnStart()");

			GetChefs ();
		}

		protected override void OnSleep ()
		{
			Debug.WriteLine ("Start App.OnSleep()");
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			Debug.WriteLine ("Start App.OnResume()");
			// Handle when your app resumes
		}

		public static MenuEntryDatabase Database {
			get {
				if (database == null) {
					database = new MenuEntryDatabase ();
				}
				return database;
			}
		}

		private async void GetChefs ()
		{
			Debug.WriteLine ("App.GetChefs()");

			var chefs = await Database.GetChefs ();

			Debug.WriteLine ("Aantal Chefs: " + chefs.Count);

		}
	}
}

