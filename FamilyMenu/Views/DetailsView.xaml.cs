using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FamilyMenu.Views
{	
	public partial class DetailsView : ContentPage
	{	
		private MenuEntryViewModel currentMenuEntryViewModel; 

		public DetailsView (object selectedItem)
		{
			InitializeComponent ();

			currentMenuEntryViewModel = selectedItem as MenuEntryViewModel;

			BindingContext = new DetailsViewModel (currentMenuEntryViewModel, Navigation);
		}
	}
}

