using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using FamilyMenu.Views;
using System.Diagnostics;
using FamilyMenu.Services;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FamilyMenu
{
    public class MainListViewModel : INotifyPropertyChanged
    {
        #region Constructors
        public MainListViewModel()
        {
            Debug.WriteLine("JAP001 - MainListViewModel()");

        }

        public MainListViewModel(DateTime datum, double footerHeight)
        {
            Debug.WriteLine(string.Format("\nJAP001 - MainListViewModel({0}, {1})", datum.ToString(), footerHeight));

            HeightInPixels = App.Metrics.Height / App.Metrics.Density;
            if (footerHeight == -1) {
                footerHeight = 54 * App.Metrics.Density;
            }
            FooterHeight = footerHeight;

            //Debug.WriteLine(string.Format("JAP001 - ScreenHeight in pixels: {0}/{1}={2}", App.Metrics.Height, App.Metrics.Density, HeightInPixels));
            //Debug.WriteLine(string.Format("JAP001 - FooterHeight: {0}", footerHeigth));

            CurrentSaterday = GetLastSaterday(datum);

            FillCurrentWeekViewModel(CurrentSaterday);

            if (null != CurrentWeek) { 
                Debug.WriteLine("JAP001 - > Aantal menu entries in huidige week: " + CurrentWeek.Count);
            }
        }

        double HeightInPixels = 0;
        double FooterHeight = 0;
        #endregion Constructors

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.OnPropertyChanged({0})", name));

            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Properties
        private double _rowheight;
        public double RowHeight
        {
            get {
                return DeviceInfo.Platform == DeviceInfo.Platforms.UWP ? (HeightInPixels - FooterHeight) / 11 : (HeightInPixels - FooterHeight) / 7;
            }
        }

        private List<MenuEntryViewModel> _currentWeek;
        public List<MenuEntryViewModel> CurrentWeek
        {
            get { return _currentWeek; }
            set
            {
                if (_currentWeek != value)
                {
                    _currentWeek = value;

                    OnPropertyChanged("CurrentWeek");
                }
            }
        }

        private MenuEntryViewModel _selectedday;
        public MenuEntryViewModel SelectedDay
        {
            get { return _selectedday; }
            set
            {
                if (_selectedday != value)
                {
                    _selectedday = value;

                    OnPropertyChanged("SelectedDay");
                }
            }
        }

        private DateTime _currentSaterday;
        public DateTime CurrentSaterday
        {
            get { return _currentSaterday; }
            set
            {
                if (_currentSaterday != value)
                {
                    _currentSaterday = value;

                    OnPropertyChanged("CurrentSaterday");
                    OnPropertyChanged("DisplayCurrentSaterday");
                    OnPropertyChanged("UseCardFrom");
                }
            }
        }

        public string DisplayCurrentSaterday
        {
            get { return CurrentSaterday.ToString("dd MMMMM yyyy"); }
        }

        public string UseCardFrom
        {
            get
            {
                int weekNo = GetIso8601WeekOfYear(CurrentSaterday);

                Debug.WriteLine("JAP001 - MainListViewModel.UseCardFrom(): Weeknummer: " + weekNo + ", modulo 3: " + (weekNo % 3));

                switch (weekNo % 2)
                {
                    case 0:
                        return "CASPER";
                    case 1:
                        return "ISABELLE";
                    //case 2:
                    //    return "MATTHIJS";
                    default:
                        return "Error during modulo calculation: " + weekNo;
                }
            }
        }
        #endregion Properties

        #region Methods
        private async void FillCurrentWeekViewModel(DateTime date)
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.FillCurrentWeekViewModel({0})", date.ToString()));

            var startdatum = CurrentSaterday.Year + "-"
                            + string.Format("{0:D2}", CurrentSaterday.Month) + "-"
                            + string.Format("{0:D2}", CurrentSaterday.Day);

            var client = new FamilyMenuServices();
            Week week = await client.GetWeekAsync(startdatum);

            if(null == week) {
                AddNewWeek(CurrentSaterday, 7);

                week = await client.GetWeekAsync(startdatum);
            }

            var thisWeek = week.week;

            // Add new Week
            if (thisWeek.Count < 7)
            {
                AddNewWeek(date.AddDays(thisWeek.Count), 7 - thisWeek.Count);
            }

            CurrentWeek = new List<MenuEntryViewModel>();

            foreach (var me in thisWeek)
            {
                var menuDatum = DateTime.Parse(me.Datum);

                CurrentWeek.Add(new MenuEntryViewModel(me));
            }
        }

        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.GetIso8601WeekOfYear({0})", time.ToString()));

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

        internal async void AddNewWeek(DateTime saterday, int numberOfDays)
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.AddNewWeek({0}, {1})", saterday.ToString(), numberOfDays));

            // First get highest id and add 1
            //var tmp = App.Database.GetItems();

            var client = new FamilyMenuServices();
            //var es = await client.GetHighestIDAsync();

            //int newId = int.Parse(es) + 1;

            for (int i = 0; i < numberOfDays; i++)
            {

                DateTime dt = saterday.AddDays(i);

                MenuEntry me = new MenuEntry
                {
                    //ID = newId + i,
                    Datum = dt.Year + "-" + dt.Month.ToString("d2") + "-" + dt.Day.ToString("d2"),
                    Chef = "Choose a chef",
                    Omschrijving = ""
                };

                if (dt.DayOfWeek.ToString() == "Wednesday") {
                    me.Chef = "Jacqueline";
                    me.Omschrijving = "Broodjes";
                } else if (dt.DayOfWeek.ToString() == "Friday") {
                    me.Chef = "Joost";
                    me.Omschrijving = "Frites";
                }

                try
                {
                    var rcB = await client.InsertMenuAsync(me);

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                }
            }
        }
        #endregion

        #region Commands
        private Command nextWeekCommand;
        public Command NextWeekCommand
        {
            get
            {
                return nextWeekCommand ?? (nextWeekCommand = new Command(ExecuteNextWeekCommand));
            }
        }

        internal void ExecuteNextWeekCommand()
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.ExecuteNextWeekCommand()"));

            CurrentSaterday = CurrentSaterday.AddDays(7);

            FillCurrentWeekViewModel(CurrentSaterday);

            OnPropertyChanged ("CurrentWeek");
        }

        private Command previousWeekCommand;
        public Command PreviousWeekCommand
        {
            get
            {
                return previousWeekCommand ?? (previousWeekCommand = new Command(ExecutePreviousWeekCommand));
            }
        }

        internal void ExecutePreviousWeekCommand()
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.ExecutePreviousWeekCommand()"));

            CurrentSaterday = CurrentSaterday.AddDays(-7);

            FillCurrentWeekViewModel(CurrentSaterday);
            //
            //			OnPropertyChanged ("CurrentWeek");
        }

        private Command currentWeekCommand;
        public Command CurrentWeekCommand
        {
            get
            {
                return currentWeekCommand ?? (currentWeekCommand = new Command(ExecuteCurrentWeekCommand));
            }
        }

        private void ExecuteCurrentWeekCommand()
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.ExecuteCurrentWeekCommand()"));

            FillCurrentWeekViewModel(CurrentSaterday);
        }

        private Command thisWeekCommand;
        public Command ThisWeekCommand
        {
            get
            {
                return thisWeekCommand ?? (thisWeekCommand = new Command(ExecuteThisWeekCommand));
            }
        }

        private void ExecuteThisWeekCommand()
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.ExecuteThisWeekCommand()"));

            CurrentSaterday = GetLastSaterday(DateTime.Now.Date);

            FillCurrentWeekViewModel(CurrentSaterday);
            //
            //			OnPropertyChanged ("CurrentWeek");
        }
        #endregion

        private DateTime GetLastSaterday(DateTime date)
        {
            Debug.WriteLine(string.Format("JAP001 - MainListViewModel.GetLastSaterday({0})", date.ToString()));

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return date.AddDays(-6);
                case DayOfWeek.Thursday:
                    return date.AddDays(-5);
                case DayOfWeek.Wednesday:
                    return date.AddDays(-4);
                case DayOfWeek.Tuesday:
                    return date.AddDays(-3);
                case DayOfWeek.Monday:
                    return date.AddDays(-2);
                case DayOfWeek.Sunday:
                    return date.AddDays(-1);
                case DayOfWeek.Saturday:
                    return date;
            };

            return CurrentSaterday;
        }
    }
}

