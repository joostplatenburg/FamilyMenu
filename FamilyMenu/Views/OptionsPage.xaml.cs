using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FamilyMenu.Views
{	
	public partial class OptionsPage : ContentPage
	{	
		public OptionsPage ()
		{
			InitializeComponent ();
		}

		void SettingsClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new SettingsView());
		}

		void ManageChefsClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ChefListView());
		}

		void AboutFamilyMenyClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new AboutFamilyMenuView());
		}
	}
}

