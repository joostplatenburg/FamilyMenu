using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;

namespace FamilyMenu
{
	public partial class MenuHistoryView : ContentPage
	{
		public MenuHistoryView (DetailsViewModel currentDetailsViewModel)
		{
			InitializeComponent ();

			BindingContext = new MenuHistoryViewModel ();

			list.ItemTapped += (sender, e) => {
				Debug.WriteLine ("Start - MainListView.ItemTapped");

				MenuOmschrijving currentItem = e.Item as MenuOmschrijving;

				currentDetailsViewModel.Omschrijving = currentItem.Omschrijving;

				Navigation.PopAsync();

				((ListView)sender).SelectedItem = null; // de-select the row

				Debug.WriteLine ("End - MainListView.ItemTapped");
			};
		}
	}
}

