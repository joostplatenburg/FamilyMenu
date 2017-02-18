using System;
using System.Collections.ObjectModel;

namespace FamilyMenu
{
	public class MenuHistoryViewModel
	{
		public MenuHistoryViewModel ()
		{
			AllMenuEntries = new ObservableCollection<MenuOmschrijving> ();

			GetOmschrijvingen ();
		}

		public ObservableCollection<MenuOmschrijving> AllMenuEntries { get; set; }

		private async void GetOmschrijvingen() {
			var tmp = await App.Database.GetMenuItems ();

			foreach(var me in tmp) {
				AllMenuEntries.Add (me);
			}
		}
	}
}

