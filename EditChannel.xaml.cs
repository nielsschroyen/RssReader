using System;
using Microsoft.Phone.Shell;
using Reader.ViewModels;

namespace Reader
{
    public partial class EditChannel
    {
        public EditChannel()
        {
            InitializeComponent();
            DataContext = new EditChannelViewModel();
            Loaded += EditChannelLoaded;
        }

        void EditChannelLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Hack to dynamically change the save button
            var appBarSaveButton = new ApplicationBarIconButton(
            new Uri("/resources/icons/appbar.check.rest.png", UriKind.Relative)) { Text = AppResources.Save };
            ApplicationBar.Buttons.Add(appBarSaveButton);
            ((EditChannelViewModel)DataContext).SaveButton = appBarSaveButton;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ((EditChannelViewModel)DataContext).ReInitialize();
        }

    }
}