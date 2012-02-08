using System;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Reader.Models;
using Reader.Workers;

namespace Reader
{
    public partial class FeedItemPage
    {
        private readonly RssFeedItem _feedItem;

        public FeedItemPage()
        {
            InitializeComponent();
            _feedItem = (RssFeedItem) PhoneApplicationService.Current.State[Constants.OpenFeed];
            DataContext =  _feedItem;
             PhoneApplicationService.Current.State.Remove(Constants.OpenFeed);

             Loaded += FeedItemPageLoaded;
        }

        void FeedItemPageLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Hack to dynamically change the save button
            var appOpenSiteButton = new ApplicationBarIconButton(
            new Uri("/Resources/Icons/appbar.web.rest.png", UriKind.Relative)) { Text = AppResources.OpenFeed };
            ApplicationBar.Buttons.Add(appOpenSiteButton);
            if (!String.IsNullOrEmpty(_feedItem.Link))
            {
                appOpenSiteButton.Click += AppOpenSiteButtonClick;
            }
            else
            {
                appOpenSiteButton.IsEnabled = false;
            }
        }

        void AppOpenSiteButtonClick(object sender, EventArgs e)
        {
            try
            {
                var browserTask = new WebBrowserTask {URL = _feedItem.Link};
                browserTask.Show();

            }
            catch
            {
                
            
            }
    
        }
    }

    


}