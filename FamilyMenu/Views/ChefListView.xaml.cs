using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FamilyMenu
{
	public partial class ChefListView : ContentPage
	{
		private ChefListViewModel ViewModel;

		public ChefListView ()
		{
			InitializeComponent ();

			this.ViewModel = new ChefListViewModel (Navigation); 
			BindingContext = this.ViewModel;

			// Display the add icon
//			var toolbarItem = new ToolbarItem ("Add", "add.png", ()=>{
//				Navigation.PushAsync(new ChefDetailView());
//			}, 0, 0);
//			ToolbarItems.Add (toolbarItem);

			cheflist.ItemTapped += (sender, e) => {
				// do something with e.Item

				Navigation.PushAsync(new ChefDetailView(((ListView)sender).SelectedItem));

				((ListView)sender).SelectedItem = null; // de-select the row
			};

//			MessagingCenter.Subscribe<Chef> (this, "Refresh", (sender) => {
//				this.ViewModel.LoadChefsCommand.Execute(null);
//			});

		}
	}
}

