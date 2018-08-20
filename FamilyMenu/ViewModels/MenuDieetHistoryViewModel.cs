using System;
using System.Collections.ObjectModel;

namespace FamilyMenu
{
	public class MenuDieetHistoryViewModel
	{
		public MenuDieetHistoryViewModel ()
		{
			AllDieetmenuEntries = new ObservableCollection<DieetOmschrijving> ();

			GetOmschrijvingen ();
		}

		public ObservableCollection<DieetOmschrijving> AllDieetmenuEntries { get; set; }

		private async void GetOmschrijvingen() {
			//var tmp = await App.Database.GetDieetmenuItems ();

			//foreach(var me in tmp) {
			//	if (me.dieetOmschrijving != null) {
			//		if (!string.IsNullOrEmpty (me.dieetOmschrijving.Trim ())) {
			//			AllDieetmenuEntries.Add (me);
			//		}
			//	}
			//}
		}
	}
}

