using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Reader.Models;
using Reader.Workers;

namespace Reader.ViewModels
{
    public class PivotItemViewModel : NotifyPropertyChangedBase
    {
        private List<RssFeedItem> _feedItems;

        /// <summary>
        /// Feeditems for the feed
        /// </summary>
        public List<RssFeedItem> FeedItems
        {
            get { return _feedItems; }
            set { _feedItems = value;
                NotifyPropertyChanged(() => FeedItems);
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                NotifyPropertyChanged(() => IsLoading);
            }
        }

        private bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value;
                NotifyPropertyChanged(() => HasError);
            }
        }

        /// <summary>
        /// The feed
        /// </summary>
        public Feed Feed { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="feed"></param>
        public PivotItemViewModel(Feed feed)
        {
            Feed = feed;
        }

        /// <summary>
        /// Update the feed
        /// </summary>
        public void Update()
        {
            IsLoading = true;
            HasError = false;
            var manager = new RssManager();
            manager.ReadRssCompleted += ManagerReadRssCompleted;
            manager.ReadFeedAsync(Feed.FeedUrl);
        }

      

        /// <summary>
        /// Called when the feedmanager downloaded the items
        /// </summary>
        /// <param name="sender">Should be the RssManager</param>
        /// <param name="args">Contains the downloaded feeditems</param>
        private void ManagerReadRssCompleted(object sender, ReadFeedCallbackArguments args)
        {
            IsLoading = false;
            var manager = sender as RssManager;
            if (manager != null)
            {
                manager.ReadRssCompleted -= ManagerReadRssCompleted;
            }
            if (String.IsNullOrEmpty(args.ErrorMessage))
            {
                FeedItems = args.FeedItems;
            }
            else
            {
                HasError = true;
            }
        }

        public void OpenItem(RssFeedItem selectedItem)
        {
            if (selectedItem != null)
            {
                PhoneApplicationService.Current.State[Constants.OpenFeed] = selectedItem;
                ((PhoneApplicationFrame) Application.Current.RootVisual).Navigate(Constants.FeedItemPageUri);
            }
        }
    }
}
