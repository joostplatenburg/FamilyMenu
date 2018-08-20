using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace FamilyMenu.Views
{
    public partial class DetailsView : ContentPage
    {
        private DetailsViewModel vm;

        public DetailsView(MenuEntry menuentry, MainListViewModel _mainListVM)
        {
            Debug.WriteLine(string.Format("JAP001 - Start DetailsView({0}", menuentry.Datum));

            InitializeComponent();

            vm = new DetailsViewModel(menuentry, _mainListVM);

            BindingContext = vm;
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var retval = await vm.ExecuteSaveCommand();

            await Navigation.PopToRootAsync();
        }

        void OnPickCommand(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuHistoryView(vm));
        }
    }
}

