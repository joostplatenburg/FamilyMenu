using System;
using Xamarin.Forms;

namespace FamilyMenu
{
	static class ColorResources
	{
        public static readonly Color PageBackgroundColor = GetPageBackgroundColor();
        public static readonly Color AccentColor = GetAccentColor();
        public static readonly Color LabelTextColor = GetLabelTextColor();

        public static readonly Color ButtonTextColor = Color.Purple;
		public static readonly Color ButtonBackgroundColor = Color.Silver;
        public static readonly Color EntryTextColor = GetEntryTextColor();

		public static readonly Color ListViewTextColor = Color.Black;
		public static readonly Color ButtonSettingColor = Color.FromRgb(0.9, 0.9, 0.9);
//		public static Color TextColor = Device.OnPlatform(Color.Default, Color.Default, Color.Accent);
//		public static Color AccentColor = Color.Purple;

        private static Color GetPageBackgroundColor()
        {
            switch(Device.RuntimePlatform)
            {
                case Device.Android:
                    return Color.White;
                case Device.iOS:
                    return Color.White;
//                case Device.Windows:
//                    return Color.Default;
                case Device.WinPhone:
                    return Color.Default;
                default:
                    return Color.Default;
            }
		}

		private static Color GetAccentColor()
		{
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					return Color.Purple;
				case Device.iOS:
					return Color.Purple;
//				case Device.Windows:
//					return Color.Purple;
				case Device.WinPhone:
					return Color.Purple;
				default:
					return Color.Default;
			}
		}

		private static Color GetLabelTextColor()
		{
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					return Color.Purple;
				case Device.iOS:
					return Color.Purple;
//				case Device.Windows:
//					return Color.Purple;
				case Device.WinPhone:
					return Color.Purple;
				default:
					return Color.Default;
			}
		}

        private static Color GetEntryTextColor()
		{
			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					return Color.Black;
				case Device.iOS:
                    return Color.Default;
//				case Device.Windows:
				case Device.WinPhone:
					return Color.Accent;
				default:
					return Color.Default;
			}
		}
	}
}

