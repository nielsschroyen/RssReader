using System;
using System.Windows.Input;
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
            new Uri("/Resources/Icons/appbar.save.rest.png", UriKind.Relative)) { Text = AppResources.Save };
            ApplicationBar.Buttons.Add(appBarSaveButton);
            var editChannelViewModel = ((EditChannelViewModel) DataContext);
            editChannelViewModel.SaveButton = appBarSaveButton;
            _tbFeedName.Text = editChannelViewModel.FeedItem.Name;
            _tvFeedUri.Text = editChannelViewModel.FeedItem.FeedUrl;
        }


        private void FeedNameUpdated(object sender, KeyEventArgs keyEventArgs)
        {
            ((EditChannelViewModel)DataContext).FeedItem.Name = _tbFeedName.Text;
        }

        private void FeedUrlUpdated(object sender, KeyEventArgs keyEventArgs)
        {
            ((EditChannelViewModel)DataContext).FeedItem.FeedUrl = _tvFeedUri.Text;
        }
    }
}