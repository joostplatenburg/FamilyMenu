using Xamarin.Forms;
using FamilyMenu.Android;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(Editor), typeof(AndroidEditorRenderer))]
namespace FamilyMenu.Android
{
	public class AndroidEditorRenderer : EditorRenderer
	{
		private static global::Android.Graphics.Color _textColor;

		static AndroidEditorRenderer()
		{
			_textColor = global::Android.Graphics.Color.Purple;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);
			Control.SetTextColor(_textColor);
		}
	}
}