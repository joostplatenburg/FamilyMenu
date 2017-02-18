using System;
using Xamarin.Forms;

namespace FamilyMenu
{
	static class ColorResources
	{
		public static readonly Color PageBackgroundColor =
			Device.OnPlatform(Color.Default, Color.White, Color.White);
		public static readonly Color AccentColor =
			Device.OnPlatform(Color.Purple, Color.Purple, Color.Purple);
		public static readonly Color LabelTextColor = 
			Device.OnPlatform(Color.Purple, Color.Purple, Color.Purple);
		public static readonly Color ButtonTextColor = Color.Purple;
		public static readonly Color ButtonBackgroundColor = Color.Silver;
		public static readonly Color EntryTextColor =
			Device.OnPlatform(Color.Black, Color.Default, Color.Accent);

		public static readonly Color ListViewTextColor = Color.Black;
		public static readonly Color ButtonSettingColor = Color.FromRgb(0.9, 0.9, 0.9);
//		public static Color TextColor = Device.OnPlatform(Color.Default, Color.Default, Color.Accent);
//		public static Color AccentColor = Color.Purple;


	}
}

