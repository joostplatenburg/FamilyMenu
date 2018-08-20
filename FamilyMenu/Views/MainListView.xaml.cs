using System;
using System.Diagnostics;
using Xamarin.Forms;
//using Xamarin.Forms.Labs.Controls;

namespace FamilyMenu.Views
{
    public partial class MainListView : ContentPage
    {
        private MainListViewModel vm;

        private string CurrentOrientation = string.Empty;
        private string NewOrientation = string.Empty;

        public MainListView()
        {
            Debug.WriteLine("Start MainListView()");

            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

            vm = new MainListViewModel(DateTime.Now.Date);

            BindingContext = vm;

            psZaterdag.GestureRecognizers.Add(
                new TapGestureRecognizer()
            { Command = new Command(() => { Navigation.PushAsync(new DetailsView(vm.Zaterdag, vm)); }) });

            psZondag.GestureRecognizers.Add(
                new TapGestureRecognizer()
            { Command = new Command(() => { Navigation.PushAsync(new DetailsView(vm.Zondag, vm)); }) });

            psMaandag.GestureRecognizers.Add(
                new TapGestureRecognizer()
            { Command = new Command(() => { Navigation.PushAsync(new DetailsView(vm.Maandag, vm)); }) });

            psDinsdag.GestureRecognizers.Add(
                new TapGestureRecognizer()
            { Command = new Command(() => { Navigation.PushAsync(new DetailsView(vm.Dinsdag, vm)); }) });

            psWoensdag.GestureRecognizers.Add(
                new TapGestureRecognizer()
            { Command = new Command(() => { Navigation.PushAsync(new DetailsView(vm.Woensdag, vm)); }) });

            psDonderdag.GestureRecognizers.Add(
                new TapGestureRecognizer()
            { Command = new Command(() => { Navigation.PushAsync(new DetailsView(vm.Donderdag, vm)); }) });

            psVrijdag.GestureRecognizers.Add(
                new TapGestureRecognizer()
            { Command = new Command(() => { Navigation.PushAsync(new DetailsView(vm.Vrijdag, vm)); }) });
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("Start MainListView.OnAppearing()");

            base.OnAppearing();
        }

        protected override void OnBindingContextChanged()
        {
            Debug.WriteLine("Start MainListView.OnBindingContextChanged()");

            base.OnBindingContextChanged();
        }

        public double width { get; set; }
        public double height { get; set; }

        protected override void OnSizeAllocated(double width, double height)
        {
            Debug.WriteLine("JAP001 - Start MainListView.OnSizeAllocated(" + width + "," + height + ")");

            base.OnSizeAllocated(width, height);

            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;

                if (width > height)
                {
                    /// ========================================================
                    /// === landscape
                    /// ===
                    Debug.WriteLine("Landscape width: " + width);
                    Debug.WriteLine("Landscape height: " + height);

                    PreviousWeekButton.WidthRequest = (width / 10);
                    NextWeekButton.WidthRequest = (width / 10);
                    ThisWeekButton.WidthRequest = (width * 8.5 / 10);
                }
                else
                {
                    /// ========================================================
                    /// === portrait
                    /// ===
                    Debug.WriteLine("Portrait width: " + width);
                    Debug.WriteLine("Portrait height: " + height);

                    PreviousWeekButton.WidthRequest = ((width / 9) * 1.5);
                    NextWeekButton.WidthRequest = ((width / 9) * 1.5);
                    ThisWeekButton.WidthRequest = ((width / 9) * 5);
                }
            }

            vm.CurrentWeekCommand.Execute(null);

            //var orientation = DependencyService.Get<IDeviceOrientation>().GetOrientation();
            //Debug.WriteLine("Orientation: " + orientation);
        }

        void OnClick(object sender, EventArgs e)
        {
            ToolbarItem tbi = (ToolbarItem)sender;

            //this.DisplayAlert("Show options page", tbi.Name,"OK");

            Navigation.PushAsync(new OptionsPage());
        }

        double x, y;

        internal void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            // Handle the pan
            //switch (e.StatusType)
            //{
            //    case GestureStatus.Running:
            //        // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
            //        //Content.TranslationX =
            //        //  Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - App.ScreenWidth));

            //        Math.Min(0, x + e.TotalX);

            //        if(e.TotalX > 0) {
            //            SwapDirection = "Right";
            //        } else {
            //            SwapDirection = "Left";
            //        }

            //        break;

            //    case GestureStatus.Completed:

            //        // Store the translation applied during the pan
            //        x = e.TotalX;

            //        Debug.WriteLine("Swiped Direction: " + SwapDirection);

            //        if (SwapDirection == "Right") {
            //            vm.ExecutePreviousWeekCommand();

            //        } else if (SwapDirection == "Left")  {
            //            vm.ExecuteNextWeekCommand();

            //        }

            //        break;
            //}
        }

        string SwapDirection = string.Empty;
    }
}

