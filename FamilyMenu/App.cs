using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using FamilyMenu.Services;
using FamilyMenu.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FamilyMenu
{
    public class App : Application
	{
		public static MasterDetailPage MDPage;

		public static string DeviceName;
        public static ScreenMetrics Metrics;

		public static string urlDatabase = "http://www.platenburg.eu/php/FamilyMenu/FamilyMenu.sqlite";
		public static string databaseName = "";
		public static string updateChefStm = "http://www.platenburg.eu/php/FamilyMenu/updateChef.php?id={0}&name={1}&dn='{2}'";
		public static string deleteChefStm = "http://www.platenburg.eu/php/FamilyMenu/deleteChef.php?id={0}";

        public static ObservableCollection<Chef> Chefs = new ObservableCollection<Chef>();

		public App ()
		{
            
			Debug.WriteLine ("Start App(), " + Device.RuntimePlatform.ToString());

            //var device = Resolver.Resolve<IDevice>();

            //Metrics = DeviceDisplay.ScreenMetrics;

 			MainPage = new NavigationPage(new MainListView ())
            {
                BarBackgroundColor = Color.LightGray,
                BarTextColor = ColorResources.AccentColor,
                Title = "Family Menu",
                BackgroundColor = ColorResources.PageBackgroundColor
            };
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

		private async void GetChefs ()
		{
			Debug.WriteLine ("App.GetChefs()");

            var client = new FamilyMenuServices();

            Chefs = await client.GetChefsAsync();

            Debug.WriteLine ("Aantal Chefs: " + Chefs.Count);

		}
	}
}

