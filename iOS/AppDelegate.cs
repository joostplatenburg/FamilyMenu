using Foundation;
using UIKit;
using XLabs.Ioc;
//using Xamarin.Forms.Platform.iOS;
using XLabs.Platform.Device;

namespace FamilyMenu.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			// Add following lines to get extended device information 
			// 	(replace AndroidDevice with AndroidDevice in Android and WindowsPhoneDevice in WindowsPhone
			var resolverContainer = new SimpleContainer ();
			resolverContainer.Register<IDevice> (t => AppleDevice.CurrentDevice);

			Resolver.SetResolver (resolverContainer.GetResolver ());

			App.DeviceName = UIDevice.CurrentDevice.Name;

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}