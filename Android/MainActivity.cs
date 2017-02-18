using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

//using Xamarin.Forms.Platform.Android;
using Android.Provider;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace FamilyMenu.Android
{
	[Activity (Label = "Family Menu", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			// Add following lines to get extended device information 
			// 	(replace AndroidDevice with AppleDevice in iOS and WindowsPhoneDevice in WindowsPhone
			var resolverContainer = new SimpleContainer ();
			resolverContainer.Register<IDevice> (t => AndroidDevice.CurrentDevice);
			Resolver.SetResolver (resolverContainer.GetResolver ());

			App.DeviceName = Settings.Secure.GetString(Application.Context.ContentResolver, Settings.Secure.AndroidId);

			LoadApplication(new App());
		}
	}
}

