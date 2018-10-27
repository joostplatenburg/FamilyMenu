using System;
using FamilyMenu.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using System.Diagnostics;
using FamilyMenu.Services;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FamilyMenu
{
    public partial class App : Application
    {
        public static string DeviceName;
        public static ScreenMetrics Metrics;

        public static ObservableCollection<Chef> Chefs = new ObservableCollection<Chef>();

        public App()
        {
            Debug.WriteLine("JAP001 - App");

            InitializeComponent();

            Metrics = DeviceDisplay.ScreenMetrics;

            Debug.WriteLine(string.Format("JAP001 - App ScreenMetrics.Heigh={0}, ScreenMetrics.Width={1}", Metrics.Height, Metrics.Width));
            Debug.WriteLine(string.Format("JAP001 - App ScreenMetrics.Heigh={0}, ScreenMetrics.Width={1}", Metrics.Height / Metrics.Density, Metrics.Width / Metrics.Density));
            GetChefs();

            MainPage = new NavigationPage(new MainListView())
            {
                BarBackgroundColor = Color.LightGray,
                BarTextColor = ColorResources.AccentColor,
                Title = "Group Menu"
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Debug.WriteLine("JAP001 - App.OnStart");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Debug.WriteLine("JAP001 - App.OnSleep");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Debug.WriteLine("JAP001 - App.OnResume");
        }

        private async void GetChefs()
        {
            Debug.WriteLine("App.GetChefs()");

            var client = new FamilyMenuServices();
            Chefs = await client.GetChefsAsync();

            Debug.WriteLine("Aantal Chefs: " + Chefs.Count);
        }
    }
}
