using System;
using Reader.ViewModels;

namespace Reader
{
    public partial class StartPage
    {
        public StartPage()
        {
            InitializeComponent();
            DataContext = new StartPageViewModel(this._pivot);

        }

        private void UpdateClick(object sender, EventArgs e)
        {
            ((StartPageViewModel)DataContext).Update();
        }

        private void PhoneApplicationPageLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ((StartPageViewModel)DataContext).Update();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            ((StartPageViewModel)DataContext).ReInitialize();
            base.OnNavigatedTo(e);
        }
    }
}