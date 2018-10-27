using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using FamilyMenu.Services;
using Xamarin.Forms;

namespace FamilyMenu
{
    public class DetailsViewModel : INotifyPropertyChanged
	{
        private MainListViewModel mainListVM;

        #region Constructors
        public DetailsViewModel()
        {
            Debug.WriteLine(string.Format("JAP001 - Start DetailsViewModel()"));

            FillSelectListChefs();
        }

        public DetailsViewModel(MenuEntry me, MainListViewModel _mainListVM)
        {
            Debug.WriteLine(string.Format("JAP001 - Start DetailsViewModel({0})", me.Datum));

            CurrentEntry = me;
            mainListVM = _mainListVM;

            FillSelectListChefs();

            Datum = DateTime.Parse(me.Datum);
            Chef = me.Chef;
            Omschrijving = me.Omschrijving;
        }

        public DetailsViewModel(MenuEntryViewModel mevm)
        {
            Debug.WriteLine(string.Format("JAP001 - Start DetailsViewModel({0})", mevm.Datum));

            CurrentEntry = mevm.CurrentMenuEntry;

            FillSelectListChefs();

            Datum = mevm.Datum;
            Chef = mevm.Chef;
            Omschrijving = mevm.Omschrijving;

            AllMenuEntries = new ObservableCollection<MenuOmschrijving>();

            GetOmschrijvingen();
        }
        #endregion Constructors

        private void FillSelectListChefs()
        {
			Chefs.Clear();
            Chefs.Add("Choose a chef");

            foreach (var chef in App.Chefs)
            {
				Chefs.Add(chef.Name);
            }
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
        private MenuEntry _currentEntry;
        public MenuEntry CurrentEntry
        {
          get { return _currentEntry; } 
          set { 
              if (_currentEntry == value)
                  return;

              _currentEntry = value;

              OnPropertyChanged ("CurrentEntry");
          }
        }

        public DateTime Datum 
		{
            get {
                DateTime dt = DateTime.Parse(CurrentEntry.Datum);

                return dt; 
            }
            set
            {
                if (CurrentEntry.Datum == value.ToString())
                    return;

                CurrentEntry.Datum = value.ToString();

                OnPropertyChanged("Datum");
            }
        }

        public string Chef { 
            get { return CurrentEntry.Chef; }
			set {
                if (CurrentEntry.Chef == value)
					return;

                CurrentEntry.Chef = value;

				OnPropertyChanged ("Chef"); 
			}
		}

		public string Omschrijving { 
            get { return CurrentEntry.Omschrijving; }
			set {
                if (CurrentEntry.Omschrijving == value)
					return;

                CurrentEntry.Omschrijving = value;

				OnPropertyChanged ("Omschrijving"); 
			}
		}

		private ObservableCollection<string> _Chefs = new ObservableCollection<string> ();
		public ObservableCollection<string> Chefs {
			get { return _Chefs; }
		}

		#region Commands
		public async Task<bool> ExecuteSaveCommand()
		{
			try
			{
                var client = new FamilyMenuServices();
                var rc = await client.UpdateMenuAsync(CurrentEntry);

                Debug.WriteLine("UpdateMenuAsync: " + rc);

                // Nu de mainlist UI update forceren
                //var currentDate = DateTime.Parse(CurrentEntry.Datum);
			}
			catch(Exception ex) {
                Debug.WriteLine("ExecuteSaveCommand: " + ex.Message);
                return false;
			}
            return true;
		}

		private Command pickCommand;
		public Command PickCommand {
			get {
				return pickCommand ?? (pickCommand = new Command(ExecutePickCommand));
			}
		}

		private void ExecutePickCommand()
		{
			
		}
		#endregion

		#region VisualElement properties
		/// <summary>
		/// Thes values are depended on device orientation
		/// </summary>
		//public double Width { get { return DeviceInfo.Width - 85; } }

		#endregion VisualElement properties

        public ObservableCollection<MenuOmschrijving> AllMenuEntries { get; set; }

        private async void GetOmschrijvingen() {

            var client = new FamilyMenuServices();
            var tmp = await client.GetMenuOmschrijvingenAsync();

            foreach(var me in tmp) {
                me.Omschrijving = me.Omschrijving.Trim();
                AllMenuEntries.Add (me);
            }
        }
    }
}