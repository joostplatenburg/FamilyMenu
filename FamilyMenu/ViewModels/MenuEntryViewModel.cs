using System;

using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FamilyMenu
{
	public class MenuEntryViewModel : INotifyPropertyChanged
	{
		private MenuEntry currentMenuEntry;
		public MenuEntry CurrentMenuEntry { get { return currentMenuEntry; } }

		public MenuEntryViewModel (MenuEntry _me)
		{
			currentMenuEntry = _me;
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string name)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged (this, new PropertyChangedEventArgs(name));
		}
		#endregion

		#region  
		public DateTime Datum {
			get { return DateTime.Parse(currentMenuEntry.Datum); }
			set { 
				if (currentMenuEntry.Datum == value.ToString("yyyy-MM-dd"))
					return;

				currentMenuEntry.Datum = value.ToString("yyyy-MM-dd");

				OnPropertyChanged ("Datum");
			}
		}

		public string Chef { get { return currentMenuEntry.Chef; }
			set { 
				if (currentMenuEntry.Chef == value)
					return;

				currentMenuEntry.Chef = value;

				OnPropertyChanged ("Chef");
			}
		}

		public string Omschrijving { get { return currentMenuEntry.Omschrijving; }
			set { 
				if (currentMenuEntry.Omschrijving == value)
					return;

				currentMenuEntry.Omschrijving = value;

				OnPropertyChanged ("Omschrijving");
			}
		}

        public string AfwasmachineBeurt
        {
            get
            {
                var returnString = string.Empty;

                switch (Datum.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        returnString = "Afwasmachinebeurt: Matthijs";
                        break;
                    case DayOfWeek.Thursday:
                        returnString = "Afwasmachinebeurt: Isabelle";
                        break;
                    case DayOfWeek.Wednesday:
                        returnString = "Afwasmachinebeurt: Isabelle";
                        break;
                    case DayOfWeek.Tuesday:
                        returnString = "Afwasmachinebeurt: Matthijs";
                        break;
                    case DayOfWeek.Monday:
                        returnString = "Afwasmachinebeurt: Casper";
                        break;
                    case DayOfWeek.Sunday:
                        returnString = "Afwasmachinebeurt: Sebastiaan";
                        break;
                    case DayOfWeek.Saturday:
                        returnString = "Afwasmachinebeurt: Casper";
                        break;
                };

                return returnString;
            }
        }

        public string KattenbakSignaal
        {
            get
            {
                var returnString = string.Empty;

                if ((Datum.DayOfYear % 3) == 0)
                {
                    returnString = returnString + "KATENBAK VERSCHONEN !";
                }

                return returnString;
            }
        }
        #endregion

        //public double DayColumnWidth { get { return DeviceInfo.FontSizeDay * 2.4; } }

        //// Specificly for day
        //public double DayFontSize { get { return DeviceInfo.FontSizeDay; } }
        //public double DateFontSize { get { return DeviceInfo.FontSizeDate; } }
        //public double ChefFontSize { get { return DeviceInfo.FontSizeChef; } }
        //public double OmschrijvingFontSize { get { return DeviceInfo.FontSizeOmschrijving; } }

        //public double DayHeight { get { return (DeviceInfo.FontSizeDay + 6); } }
        //public double ChefHeight { get { return (DeviceInfo.FontSizeChef + 6); } }
        //public double OmschrijvingHeight { get { return ((DeviceInfo.FontSizeOmschrijving * 2.5) + 6); } }
    }
}


