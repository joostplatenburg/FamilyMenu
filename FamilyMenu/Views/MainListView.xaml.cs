using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;
//using Xamarin.Forms.Labs.Controls;

namespace FamilyMenu.Views
{	
	public partial class MainListView : ContentPage
	{	
		private MainListViewModel vm;
		private string CurrentOrientation = string.Empty;
		private string NewOrientation = string.Empty;

		public MainListView ()
		{
			Debug.WriteLine ("Start MainListView()");

			InitializeComponent ();
            
            NavigationPage.SetHasNavigationBar(this, true);

			vm = new MainListViewModel (DateTime.Now.Date);

			BindingContext = vm;

			list.ItemTapped += (sender, e) => {
				Debug.WriteLine ("Start - MainListView.ItemTapped");
				// do something with e.Item

				Navigation.PushAsync(new DetailsView(((ListView)sender).SelectedItem));

				((ListView)sender).SelectedItem = null; // de-select the row
				Debug.WriteLine ("End - MainListView.ItemTapped");
			};

			list.Refreshing += (sender, e) =>
			{
				Debug.WriteLine("Start - MainListView.Refreshing");


				list.EndRefresh();

				Debug.WriteLine("Einde - MainListView.Refreshing");
			};

			vm.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) => {

				Debug.WriteLine ("Start vm.PropertyChanged(" + e.PropertyName + ")");

				if(e.PropertyName.Equals("CurrentWeek"))
				{
					this.list.ItemsSource = vm.CurrentWeek;
					this.list.RowHeight = vm.RowHeight;
					this.ThisWeekButton.Text = vm.DisplayCurrentSaterday;
				
				} else if (e.PropertyName.Equals("RowHeight"))
				{
					this.list.RowHeight = vm.RowHeight;
					this.list.ItemsSource = vm.CurrentWeek;
				}
			};
		}

		protected override void OnAppearing ()
		{
			Debug.WriteLine ("Start MainListView.OnAppearing()");

			base.OnAppearing ();
        }

        protected override void OnBindingContextChanged ()
		{
			Debug.WriteLine ("Start MainListView.OnBindingContextChanged()");
			base.OnBindingContextChanged ();
		}

		protected override void OnSizeAllocated (double width, double height)
		{
			Debug.WriteLine ("Start MainListView.OnSizeAllocated(" + width + "," + height + ")");

			base.OnSizeAllocated (width, height);

			Debug.WriteLine ("CurrentOrientation: " + CurrentOrientation); 

			if ((width+2) > height) {					// Add 2 to compensate for the iPad MDPage width)
				NewOrientation = "Landscape";
				Debug.WriteLine ("NewOrientation: Landscape"); 
			} else {
				NewOrientation = "Portrait";
				Debug.WriteLine ("NewOrientation: Portrait"); 
			}

			if(!CurrentOrientation.Equals(NewOrientation)) {
				CurrentOrientation = NewOrientation;
			
				DeviceInfo.SetSize (width, height, CurrentOrientation);

				list.RowHeight = vm.RowHeight;

				if (CurrentOrientation.Equals ("Portrait")) {
					PreviousWeekButton.WidthRequest = (width /10);
					NextWeekButton.WidthRequest = (width /10);
					ThisWeekButton.WidthRequest = (width * 7/10);
				} else {
					PreviousWeekButton.WidthRequest = (width /10);
					NextWeekButton.WidthRequest = (width /10);
					ThisWeekButton.WidthRequest = (width * 7.5/10);
				}

                vm.CurrentWeekCommand.Execute (null);
			}
		}

		void OnClick(object sender, EventArgs e)
		{
			ToolbarItem tbi = (ToolbarItem) sender;

			//this.DisplayAlert("Show options page", tbi.Name,"OK");

			Navigation.PushAsync(new OptionsPage());
		}
	}
}

