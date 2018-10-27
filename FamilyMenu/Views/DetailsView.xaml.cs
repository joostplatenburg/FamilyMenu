using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace FamilyMenu.Views
{
    public partial class DetailsView : ContentPage
    {
        private MainListViewModel mlvm = new MainListViewModel();
        private DetailsViewModel vm;

        #region Constructors
        public DetailsView()
        {
            Debug.WriteLine(string.Format("JAP001 - Start DetailsView()"));

            InitializeComponent();

            vm = new DetailsViewModel();

            BindingContext = vm;
        }

        public DetailsView(MenuEntry menuentry, MainListViewModel _mainListVM)
        {
            Debug.WriteLine(string.Format("JAP001 - Start DetailsView({0}", menuentry.Datum));

            InitializeComponent();

            vm = new DetailsViewModel(menuentry, _mainListVM);

            BindingContext = vm;
        }

        public DetailsView(MainListViewModel _mlvm)
        {
            Debug.WriteLine(string.Format("JAP001 - Start DetailsView({0}", _mlvm.SelectedDay.Datum));

            InitializeComponent();

            mlvm = _mlvm;

            vm = new DetailsViewModel(mlvm.SelectedDay);

            BindingContext = vm;

            list.ItemTapped += (sender, e) => {

                Debug.WriteLine("Start - MainListView.ItemTapped");

                MenuOmschrijving currentItem = e.Item as MenuOmschrijving;

                vm.Omschrijving = currentItem.Omschrijving;

                ((ListView)sender).SelectedItem = null; // de-select the row

                Debug.WriteLine("End - MainListView.ItemTapped");
            };
        }
        #endregion Constructors

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var retval = await vm.ExecuteSaveCommand();

            Debug.WriteLine(vm.Datum + " - " + vm.Chef + " - " + vm.Omschrijving);

            await Navigation.PopToRootAsync();
        }

        void OnPickCommand(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MenuHistoryView(vm));
        }
    }
}

