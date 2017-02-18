using Xamarin.Forms;
using FamilyMenu.Android;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(Picker), typeof(AndroidPickerRenderer))]
namespace FamilyMenu.Android
{
	public class AndroidPickerRenderer : PickerRenderer
	{
		private static global::Android.Graphics.Color _textColor;

		static AndroidPickerRenderer()
		{
			_textColor = global::Android.Graphics.Color.Purple;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);
			Control.SetTextColor(_textColor);
		}
	}
}