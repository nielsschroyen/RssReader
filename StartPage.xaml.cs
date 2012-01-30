using System;
using Reader.ViewModels;

namespace Reader
{
    public partial class StartPage
    {
        public StartPage()
        {
            InitializeComponent();
            DataContext = new StartPageViewModel();

        }

        private void UpdateClick(object sender, EventArgs e)
        {
            ((StartPageViewModel)DataContext).Update();
        }

        private void PhoneApplicationPageLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ((StartPageViewModel)DataContext).Update();
        }
    }
}