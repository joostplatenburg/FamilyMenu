using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;

namespace FamilyMenu
{
	public partial class MenuDieetHistoryView : ContentPage
	{
		public MenuDieetHistoryView (DetailsViewModel currentDetailsViewModel)
		{
			InitializeComponent ();

			BindingContext = new MenuDieetHistoryViewModel ();

			list.ItemTapped += (sender, e) => {
				Debug.WriteLine ("Start - MenuDieetHistoryView.ItemTapped");

				DieetOmschrijving currentItem = e.Item as DieetOmschrijving;

				currentDetailsViewModel.Dieet = currentItem.dieetOmschrijving;

				Navigation.PopAsync();

				((ListView)sender).SelectedItem = null; // de-select the row

				Debug.WriteLine ("End - MenuDieetHistoryView.ItemTapped");
			};
		}
	}
}

