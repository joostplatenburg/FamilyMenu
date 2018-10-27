using System;
using System.Diagnostics;
using Xamarin.Essentials;
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
            Debug.WriteLine("JAP001 - MainListView()");

            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

            vm = new MainListViewModel(DateTime.Now.Date, stackFooter.Height);

            BindingContext = vm;

            currentweekList.ItemTapped += (sender, e) =>
            {
                Navigation.PushAsync(new DetailsView(vm));

                ((ListView)sender).SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("JAP001 - MainListView.OnAppearing()");

            base.OnAppearing();

            foreach (MenuEntryViewModel mevm in vm.CurrentWeek)
            {
                Debug.WriteLine("JAP001 - " + mevm.Chef + " - " + mevm.Omschrijving);
            }

            Debug.WriteLine("Footer.Heigth: " + stackFooter.Height);
        }

        protected override void OnBindingContextChanged()
        {
            Debug.WriteLine("JAP001 - MainListView.OnBindingContextChanged()");

            base.OnBindingContextChanged();
        }

        public double _width { get; set; }
        public double _height { get; set; }

        protected override void OnSizeAllocated(double width, double height)
        {
            Debug.WriteLine("JAP001 - MainListView.OnSizeAllocated(" + width + "," + height + ")");

            base.OnSizeAllocated(width, height);

            if (width != this._width || height != this._height)
            {
                this._width = width;
                this._height = height;

                if (width > height)
                {
                    /// ========================================================
                    /// === landscape
                    /// ===
                    PreviousWeekButton.WidthRequest = (width / 10);
                    NextWeekButton.WidthRequest = (width / 10);
                    ThisWeekButton.WidthRequest = (width * 8.5 / 10);
                }
                else
                {
                    /// ========================================================
                    /// === portrait
                    /// ===
                    PreviousWeekButton.WidthRequest = ((width / 9) * 1.5);
                    NextWeekButton.WidthRequest = ((width / 9) * 1.5);
                    ThisWeekButton.WidthRequest = ((width / 9) * 5);
                }
            }
        }

        void OnClick(object sender, EventArgs e)
        {
            Debug.WriteLine("JAP001 - MainListView.OnClick()");

            ToolbarItem tbi = (ToolbarItem)sender;

            //this.DisplayAlert("Show options page", tbi.Name,"OK");

            Navigation.PushAsync(new OptionsPage());
        }

        double x, y;

        internal void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Debug.WriteLine("JAP001 - MainListView.OnPanUpdated()");

            // Handle the pan
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    //Content.TranslationX =
                    //  Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - App.ScreenWidth));

                    Math.Min(0, x + e.TotalX);

                    if(e.TotalX > 0) {
                        SwapDirection = "Right";
                    } else {
                        SwapDirection = "Left";
                    }

                    break;

                case GestureStatus.Completed:

                    // Store the translation applied during the pan
                    x = e.TotalX;

                    Debug.WriteLine("Swiped Direction: " + SwapDirection);

                    if (SwapDirection == "Right") {
                        vm.ExecutePreviousWeekCommand();

                    } else if (SwapDirection == "Left")  {
                        vm.ExecuteNextWeekCommand();

                    }

                    break;
            }
        }

        string SwapDirection = string.Empty;
    }
}

