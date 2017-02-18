using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FamilyMenu
{
	public partial class ChefDetailView : ContentPage
	{
		public ChefDetailView ()
		{
			InitializeComponent ();

			BindingContext = new ChefDetailViewModel (Navigation);
		}

		public ChefDetailView (object selectedItem)
		{
			InitializeComponent ();

			Chef currentChef = selectedItem as Chef;

			BindingContext = new ChefDetailViewModel (currentChef, Navigation);
		}
	}
}

