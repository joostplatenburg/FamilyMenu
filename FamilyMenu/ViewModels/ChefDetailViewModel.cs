using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Diagnostics;

namespace FamilyMenu
{
	public class ChefDetailViewModel : INotifyPropertyChanged
	{
		private Chef CurrentChef = new Chef();

		private INavigation navigation;

		public ChefDetailViewModel (Chef _currentChef, INavigation navigation)
		{
			this.navigation = navigation;

			CurrentChef = _currentChef;

		}

		public ChefDetailViewModel (INavigation navigation)
		{
			this.navigation = navigation;

			CurrentChef = new Chef();

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

		public string Name { 
			get { return CurrentChef.Name; }
			set {
				if (CurrentChef.Name == value)
					return;

				CurrentChef.Name = value;

				OnPropertyChanged ("Chef"); 
			}
		}

		private Color _labelTextColor = ColorResources.AccentColor;
		public Color LabelTextColor {
			get { return _labelTextColor; }
		}

		private Color _entryTextColor = ColorResources.EntryTextColor; 
		public Color EntryTextColor {
			get { return _entryTextColor; }
		}

		//private double _width = DeviceInfo.Width - 85;
		//public double Width {
		//	get { return _width; }
		//}

		private Color _buttonTextColor = ColorResources.AccentColor; 
		public Color ButtonTextColor {
			get { return _buttonTextColor; }
		}

		#region Commands

		private Command saveCommand;
		public Command SaveCommand {
			get {
				return saveCommand ?? (saveCommand = new Command(ExecuteSaveCommand));
			}
		}

		private void ExecuteSaveCommand()
		{
			if (CurrentChef != null) {
				//App.Database.SaveChef(CurrentChef);
			
				MessagingCenter.Send<ChefDetailViewModel> (this, "ChefAdded");
			}

			try
			{
				//string urlPhp = string.Format(App.updateChefStm, CurrentChef.ID, CurrentChef.Name, 
				//						App.DeviceName);

				//DependencyService.Get<INetworkFunctions> ().callAsyncPHPScript (urlPhp);
			}
			catch(Exception ex) {
				Debug.WriteLine ("Error: " + ex.Message);
			}

			// No go back to main page with week menu
			navigation.PopAsync ();
		}

		private Command deleteCommand;
		public Command DeleteCommand {
			get {
				return deleteCommand ?? (deleteCommand = new Command(ExecuteDeleteCommand));
			}
		}

		private void ExecuteDeleteCommand()
		{
			//			MessagingCenter.Send(this, "Deleted " + Name);
			//App.Database.DeleteChef (CurrentChef.ID);

			try {
				//string urlPhp = string.Format(App.deleteChefStm, CurrentChef.ID);

				//DependencyService.Get<INetworkFunctions> ().callAsyncPHPScript (urlPhp);

			} catch (Exception) {

				// DisplayAlert ("Family Menu Action", "Delete " + chef.Name	, "OK");
			}
		}

		#endregion
	}
}

