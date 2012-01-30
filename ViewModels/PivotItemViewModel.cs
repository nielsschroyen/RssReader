using System;
using System.Collections.Generic;
using System.ComponentModel;
using Reader.Models;
using Reader.Workers;

namespace Reader.ViewModels
{
    public class PivotItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Feeditems for the feed
        /// </summary>
        public List<RssFeedItem> FeedItems { get; set; }


        public Feed Feed { get; private set; }

        public PivotItemViewModel(Feed feed)
        {
            this.Feed = feed;
        }

        /// <summary>
        /// Update the feed
        /// </summary>
        public void Update()
        {
            var manager = new RssManager();
            manager.ReadRssCompleted += ManagerReadRssCompleted;
            manager.ReadFeedAsync(Feed.FeedUrl);
        }

        private void ManagerReadRssCompleted(object sender, ReadFeedCallbackArguments args)
        {
            var manager = sender as RssManager;
            if (manager != null)
                manager.ReadRssCompleted -= ManagerReadRssCompleted;

            if (String.IsNullOrEmpty(args.ErrorMessage))
            {
                FeedItems = args.FeedItems;
                if (PropertyChanged!=null)
                PropertyChanged(null, new PropertyChangedEventArgs("FeedItems"));
            }
        }


    }
}
