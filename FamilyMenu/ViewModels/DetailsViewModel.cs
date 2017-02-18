using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using FamilyMenu.Services;

namespace FamilyMenu
{
	public class DetailsViewModel : INotifyPropertyChanged
	{
		private MenuEntryViewModel currentMenuEntryViewModel;

		private INavigation navigation;

		public DetailsViewModel(MenuEntryViewModel _mevm, INavigation navigation)
		{
			this.navigation = navigation;

			currentMenuEntryViewModel = _mevm;

			FillSelectListChefs();

			Datum = currentMenuEntryViewModel.Datum;
			Chef = currentMenuEntryViewModel.Chef;
			Omschrijving = currentMenuEntryViewModel.Omschrijving;
		}

        private void FillSelectListChefs()
        {
			_Chefs.Clear();
            _Chefs.Add("Choose a chef");

            var chefs = App.Database.GetLocalChefs();

			Debug.WriteLine ("Aantal Chefs: " + chefs.Count);
            foreach (var chef in chefs)
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

		public DateTime Datum 
		{
			get { return currentMenuEntryViewModel.Datum; } 
			set { 
				if (currentMenuEntryViewModel.Datum == value)
					return;

				currentMenuEntryViewModel.Datum = value;

				OnPropertyChanged ("Datum");
			}
		}

		public string DatumString {
			get { return Datum.ToString ("D"); }
		}

		public string Chef { 
			get { return currentMenuEntryViewModel.Chef; }
			set {
				if (currentMenuEntryViewModel.Chef == value)
					return;

				currentMenuEntryViewModel.Chef = value;

				OnPropertyChanged ("Chef"); 
			}
		}

		public string Omschrijving { 
			get { return currentMenuEntryViewModel.Omschrijving; }
			set {
				if (currentMenuEntryViewModel.Omschrijving == value)
					return;

				currentMenuEntryViewModel.Omschrijving = value;

				OnPropertyChanged ("Omschrijving"); 
			}
		}

		public string Dieet { 
			get { return currentMenuEntryViewModel.Dieet; }
			set {
				if (currentMenuEntryViewModel.Dieet == value)
					return;

				currentMenuEntryViewModel.Dieet = value;

				OnPropertyChanged ("Dieet"); 
			}
		}

		private ObservableCollection<string> _Chefs = new ObservableCollection<string> ();
		public ObservableCollection<string> Chefs {
			get { return _Chefs; }
		}

		#region Commands

		private Command saveCommand;
		public Command SaveCommand {
			get {
				return saveCommand ?? (saveCommand = new Command(ExecuteSaveCommand));
			}
		}

		private async void ExecuteSaveCommand()
		{
			if (currentMenuEntryViewModel != null)
				App.Database.SaveItem (currentMenuEntryViewModel.CurrentMenuEntry);

			try
			{
                var sv = new UpdateMenuEntryWebService();
                var rc = await sv.UpdateMenuAsync(currentMenuEntryViewModel.CurrentMenuEntry);

				MessagingCenter.Send<DetailsViewModel> (this, "UpdateListView");

				Debug.WriteLine("ExecuteSaveCommand: " + rc);
			}
			catch(Exception) {

			}

			// No go back to main page with week menu
			await navigation.PopToRootAsync ();
		}

		private Command pickCommand;
		public Command PickCommand {
			get {
				return pickCommand ?? (pickCommand = new Command(ExecutePickCommand));
			}
		}

		private void ExecutePickCommand()
		{
			navigation.PushAsync (new MenuHistoryView (this));
		}

		private void ExecutePickDieetCommand()
		{
			navigation.PushAsync (new MenuDieetHistoryView (this));
		}

		private Command pickDieetCommand;
		public Command PickDieetCommand {
			get {
				return pickDieetCommand ?? (pickDieetCommand = new Command(ExecutePickDieetCommand));
			}
		}

		#endregion

		#region VisualElement properties
		/// <summary>
		/// Thes values are depended on device orientation
		/// </summary>
		public double Width { get { return DeviceInfo.Width - 85; } }

		#endregion VisualElement properties

	}
}