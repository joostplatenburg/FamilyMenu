using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace FamilyMenu.Droid
{
    [Activity(Label = "Family Menu", Theme = "@style/MyTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }
    }
}
