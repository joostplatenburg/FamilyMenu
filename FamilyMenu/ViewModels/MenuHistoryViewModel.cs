using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FamilyMenu.Services;

namespace FamilyMenu
{
	public class MenuHistoryViewModel
	{
		public MenuHistoryViewModel ()
		{
			AllMenuEntries = new ObservableCollection<MenuOmschrijving> ();

			GetOmschrijvingen ();
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion INotifyPropertyChanged implementation

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

