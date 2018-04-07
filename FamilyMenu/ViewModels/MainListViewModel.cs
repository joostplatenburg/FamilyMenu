using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using FamilyMenu.Views;
using System.Diagnostics;
using FamilyMenu.Services;
using System.Globalization;

namespace FamilyMenu
{
	public class MainListViewModel : INotifyPropertyChanged
	{
		public MainListViewModel(DateTime datum)
		{
			Debug.WriteLine ("MainListViewModel: " + datum.ToString ());

			CurrentSaterday = GetLastSaterday (datum);

			FillCurrentWeekViewModel (CurrentSaterday);
			//CurrentWeek =  new ObservableCollection<MenuEntry>( App.Database.GetWeek (CurrentSaterday) );

			MessagingCenter.Subscribe<INetworkFunctions> 
				(this, "UpdateListView", (sender) => { ExecuteCurrentWeekCommand();	});

			MessagingCenter.Subscribe<DetailsViewModel> 
				(this, "UpdateListView", (sender) => { ExecuteCurrentWeekCommand();	});
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

		private async void FillCurrentWeekViewModel (DateTime date)
		{
			Debug.WriteLine ("FillCurrentWeekViewModel: " + date.ToString ());

			var thisWeek = await App.Database.GetWeek (date);

			if (thisWeek.Count < 7)
            {
				// Add new Week
				AddNewWeek (date.AddDays (thisWeek.Count), 7 - thisWeek.Count);
			} 

			CurrentWeek.Clear ();

			foreach (var me in thisWeek) {
				CurrentWeek.Add (new MenuEntryViewModel (me));
			}
		}

		private ObservableCollection<MenuEntryViewModel> currentWeek = new ObservableCollection<MenuEntryViewModel>();
		public ObservableCollection<MenuEntryViewModel> CurrentWeek 
		{
			get { return currentWeek; } 
			set { 
				currentWeek = value;

				OnPropertyChanged ("CurrentWeek");
			}
		}

		private MenuEntry _selectedItem;
		public MenuEntry SelectedItem
		{
			get { return _selectedItem; }
			set {
				if (_selectedItem != value) {
					_selectedItem = value;

					OnPropertyChanged ("SelectedItem");
				}
			}
		}

		private DateTime currentSaterday;
		public DateTime CurrentSaterday
		{
			get { return currentSaterday; }
			set {
				if (currentSaterday == value)
					return;

				currentSaterday = value;

				OnPropertyChanged("CurrentSaterday");
                OnPropertyChanged("DisplayCurrentSaterday");
                OnPropertyChanged("UseCardFrom");
			}
		}

        public string DisplayCurrentSaterday
        {
            get { return currentSaterday.ToString("dd MMMMM yyyy"); }
        }

        public string UseCardFrom
        {
            get {
                int weekNo = GetIso8601WeekOfYear(CurrentSaterday);

                Debug.WriteLine("Weeknummer: " + weekNo + ", modulo 3: " + (weekNo % 3));

                switch(weekNo % 3)
                {
                    case 0:
                        return "CASPER";
                    case 1:
                        return "ISABELLE";
                    case 2:
                        return "MATTHIJS";
                    default:
                        return "Error during modulo calculation: " + weekNo;
                }
            }
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

		internal async void AddNewWeek(DateTime saterday, int numberOfDays) {
			Debug.WriteLine ("AddNewWeek: " + saterday.ToString () + " ("+numberOfDays+")");

			// First get highest id and add 1
			var tmp = App.Database.GetItems();

			var sv1 = new GetHighestIDWebService ();
			var es = await sv1.GetHighestIDAsync ();

			int newId = int.Parse(es) + 1;

			for (int i = 0; i < numberOfDays; i++) {

				DateTime dt = saterday.AddDays (i);
	
				MenuEntry me = new MenuEntry {
					ID = newId + i,
					Datum = dt.ToString("yyyy-MM-dd"),
					Chef = "Choose a chef",
					Omschrijving = "", AantalDieet = "", Dieet = ""
				};

				try {
                    var sv2 = new InsertMenuEntryWebService();
                    var rcB = await sv2.InsertMenuAsync(me);

				} catch (Exception ex) {
					Debug.WriteLine ("Error: " + ex.Message);
				}
			}

			FillCurrentWeekViewModel (saterday);
		}

		#region Commands

		private Command nextWeekCommand;
		public Command NextWeekCommand {
			get {
				return nextWeekCommand ?? (nextWeekCommand = new Command(ExecuteNextWeekCommand));
			}
		}

		internal void ExecuteNextWeekCommand()
		{
			CurrentSaterday = CurrentSaterday.AddDays (7);

			FillCurrentWeekViewModel (CurrentSaterday);

			if (CurrentWeek.Count < 7) {
				// Add new Week
				AddNewWeek (CurrentSaterday.AddDays(CurrentWeek.Count), 7 - CurrentWeek.Count);

				FillCurrentWeekViewModel (CurrentSaterday);
			}
//
//			OnPropertyChanged ("CurrentWeek");
		}

		private Command previousWeekCommand;
		public Command PreviousWeekCommand {
			get {
				return previousWeekCommand ?? (previousWeekCommand = new Command(ExecutePreviousWeekCommand));
			}
		}

		internal  void ExecutePreviousWeekCommand()
		{
			CurrentSaterday = CurrentSaterday.AddDays (-7);

			FillCurrentWeekViewModel (CurrentSaterday);
//
//			OnPropertyChanged ("CurrentWeek");
		}

		private Command currentWeekCommand;
		public Command CurrentWeekCommand {
			get {
				return currentWeekCommand ?? (currentWeekCommand = new Command(ExecuteCurrentWeekCommand));
			}
		}

		private  void ExecuteCurrentWeekCommand()
		{
			FillCurrentWeekViewModel (CurrentSaterday);
		}

		private Command thisWeekCommand;
		public Command ThisWeekCommand {
			get {
				return thisWeekCommand ?? (thisWeekCommand = new Command(ExecuteThisWeekCommand));
			}
		}

		private void ExecuteThisWeekCommand()
		{
			CurrentSaterday = GetLastSaterday (DateTime.Now.Date);

			FillCurrentWeekViewModel (CurrentSaterday);
//
//			OnPropertyChanged ("CurrentWeek");
		}

		#endregion

		private DateTime GetLastSaterday (DateTime date)
		{
			switch (date.DayOfWeek) {
			case DayOfWeek.Friday:
				return date.AddDays (-6);
			case DayOfWeek.Thursday:
				return date.AddDays (-5);
			case DayOfWeek.Wednesday:
				return date.AddDays (-4);
			case DayOfWeek.Tuesday:
				return date.AddDays (-3);
			case DayOfWeek.Monday:
				return date.AddDays (-2);
			case DayOfWeek.Sunday:
				return date.AddDays (-1);
			case DayOfWeek.Saturday:
				return date;
			};

			return CurrentSaterday;
		}

		#region VisualElement properties
		/// <summary>
		/// Thes values are depended on device orientation
		/// 
		/// </summary>
		public int RowHeight { get { return DeviceInfo.RowHeight; } }

		#endregion VisualElement properties
	}
}

