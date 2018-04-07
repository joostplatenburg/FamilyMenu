using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FamilyMenu
{
	public class DeviceInfo
	{
		protected static DeviceInfo _instance;
		double width;
		double height;
		string orientation;

		static DeviceInfo()
		{
			Debug.WriteLine ("Start DeviceInfo()");
			_instance = new DeviceInfo();
		}

		protected DeviceInfo()
		{
		}

		public static bool IsOrientationPortrait() 
		{
			Debug.WriteLine ("Start DeviceInfo.IsOrientationPortrait()");

			return _instance.height > _instance.width;
		}

		public static void SetSize(double width, double height, string orientation)
		{
			Debug.WriteLine ("Start DeviceInfo.SetSize(" + width + "," + height + ")");

			_instance.width = width;
			_instance.height = height;
			_instance.orientation = orientation;

			SetDeviceType ();
		}

		public static int Width { get { return (int)_instance.width; } }
		public static int Height { get { return (int)_instance.height; } }
		public static string Orientation { get { return (string)_instance.orientation; } }

		public static void SetDeviceType()
		{
			Debug.WriteLine ("Start DeviceInfo.SetDeviceType(" + Width + "," + Height + ")");

			Debug.WriteLine ("RowHeight: " + ((Height-40)/7));

			switch (Height) {
			case 288:									// Landscape iPhone5
				_instance.deviceType = "iPhone5";
				_instance.deviceOrientation = "Landscape";
				break;
			case 295:									// Landscape GalaxayS3
			case 360:									// Landscape GalaxayS3
				_instance.deviceType = "GalaxayS3";
				_instance.deviceOrientation = "Landscape";
				break;
			case 480:
			case 416:
				_instance.deviceType = "iPhone4";
				_instance.deviceOrientation = "Portrait";
				break;
			case 567:									// Portrait Galaxy S3
			case 640:									// Portrait Galaxy S3
				_instance.deviceType = "GalaxyS3";
				_instance.deviceOrientation = "Portrait";
				break;
			case 519:									// Landscape TabletGalaxy7
			case 600:									// Landscape TabletGalaxy7
				_instance.deviceType = "TabletGalaxy7";
				_instance.deviceOrientation = "Landscape";
				break;
			case 504:
			case 568:
				_instance.deviceType = "iPhone5";
				_instance.deviceOrientation = "Portrait";
				break;
			case 596:
				_instance.deviceType = "Nexus 4";
				_instance.deviceOrientation = "Landscape";
				break;
			case 642:
				_instance.deviceType = "Nexus 5";
				_instance.deviceOrientation = "Landscape";
				break;
			case 671:
			case 752:									// Landscape Galaxy Tab2
				_instance.deviceType = "GalaxyTablet10";
				_instance.deviceOrientation = "Landscape";
				break;
			case 704:									// Landscape iPad2
				_instance.deviceType = "iPad2";
				_instance.deviceOrientation = "Landscape";
				break;
			case 880:									// Portrait TabletGalaxy7
			case 961: 
				_instance.deviceType = "TabletGalaxy7";
				_instance.deviceOrientation = "Portrait";
				break;
			case 1151:
			case 1232:									// Portrait Galaxy Tab2
				_instance.deviceType = "GalaxyTablet10";
				_instance.deviceOrientation = "Portrait";
				break;
			case 960:
			case 1024:
				_instance.deviceType = "iPad2";
				_instance.deviceOrientation = "Portrait";
				break;
			default:
				_instance.deviceType = "Other";
				_instance.deviceOrientation = "Portrait";
				break;
			}

			//_instance.deviceOrientation = Orientation;
			_instance.rowHeight = ((Height-86)/7);
			_instance.fontSizeDay = ((Height-50)/17);
			_instance.fontSizeDate = ((Height-50)/50);
			_instance.fontSizeChef = ((Height-50)/42);
			_instance.fontSizeOmschrijving = ((Height-50)/28);
		}

		string deviceType;
		public static string DeviceType { get { return _instance.deviceType; } }
		string deviceOrientation;
		public static string DeviceOrientation { get { return _instance.deviceOrientation; } }

		double fontSizeDay;
		public static double FontSizeDay { get { return _instance.fontSizeDay; } }
		double fontSizeDate;
		public static double FontSizeDate { get { return _instance.fontSizeDate; } }
		double fontSizeChef;
		public static double FontSizeChef { get { return _instance.fontSizeChef; } }
		double fontSizeOmschrijving;
		public static double FontSizeOmschrijving { get { return _instance.fontSizeOmschrijving; } }

		int rowHeight;
		public static int RowHeight { get { return _instance.rowHeight; } }
	}
}

