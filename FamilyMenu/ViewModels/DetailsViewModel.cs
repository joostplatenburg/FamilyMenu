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

        public DetailsViewModel(MenuEntry me, MainListViewModel _mainListVM)
        {
            Debug.WriteLine(string.Format("JAP001 - Start DetailsViewModel({0})", me.Datum));

            CurrentEntry = me;
            mainListVM = _mainListVM;

            FillSelectListChefs();

            Datum = me.Datum;
            Chef = me.Chef;
            Omschrijving = me.Omschrijving;
        }

        private void FillSelectListChefs()
        {
			_Chefs.Clear();
            _Chefs.Add("Choose a chef");

            foreach (var chef in App.Chefs)
            {
				_Chefs.Add(chef.Name);
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

        public string Datum 
		{
            get { return CurrentEntry.Datum; } 
			set { 
                if (CurrentEntry.Datum == value)
					return;

                CurrentEntry.Datum = value;

				OnPropertyChanged ("Datum");
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

		public string Dieet { 
            get { return CurrentEntry.Dieet; }
			set {
                if (CurrentEntry.Dieet == value)
					return;

                CurrentEntry.Dieet = value;

				OnPropertyChanged ("Dieet"); 
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
                var currentDate = DateTime.Parse(CurrentEntry.Datum);

                switch (currentDate.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        mainListVM.Zaterdag = CurrentEntry;
                        break;
                    case DayOfWeek.Sunday:
                        mainListVM.Zondag = CurrentEntry;
                        break;
                    case DayOfWeek.Monday:
                        mainListVM.Maandag = CurrentEntry;
                        break;
                    case DayOfWeek.Tuesday:
                        mainListVM.Dinsdag = CurrentEntry;
                        break;
                    case DayOfWeek.Wednesday:
                        mainListVM.Woensdag = CurrentEntry;
                        break;
                    case DayOfWeek.Thursday:
                        mainListVM.Donderdag = CurrentEntry;
                        break;
                    case DayOfWeek.Friday:
                        mainListVM.Vrijdag = CurrentEntry;
                        break;
                }
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

		//private void ExecutePickDieetCommand()
		//{
		//	navigation.PushAsync (new MenuDieetHistoryView (this));
		//}

		//private Command pickDieetCommand;
		//public Command PickDieetCommand {
		//	get {
		//		return pickDieetCommand ?? (pickDieetCommand = new Command(ExecutePickDieetCommand));
		//	}
		//}

		#endregion

		#region VisualElement properties
		/// <summary>
		/// Thes values are depended on device orientation
		/// </summary>
		//public double Width { get { return DeviceInfo.Width - 85; } }

		#endregion VisualElement properties

	}
}