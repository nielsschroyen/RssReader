using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using Microsoft.Phone.Controls;
using Reader.Models;

namespace Reader.Workers
{
    public class ApplicationStorageManager
    {
        public static List<Feed> GetFeeds()
        {
            return IsolatedStorageSettings.ApplicationSettings[Constants.RssData] as List<Feed>;
        }

        public static void InitializeStore(List<PivotItem> items)
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(Constants.RssData))
            {
                IsolatedStorageSettings.ApplicationSettings.Add(Constants.RssData, items);
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

        public static void AddFeed(Feed feedItem)
        {
            var feeds = IsolatedStorageSettings.ApplicationSettings[Constants.RssData] as List<Feed> ?? new List<Feed>();


            feeds.Add(feedItem);
            IsolatedStorageSettings.ApplicationSettings[Constants.RssData] = feeds;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static void EditFeed(Feed feedItem)
        {
            var feeds = IsolatedStorageSettings.ApplicationSettings[Constants.RssData] as List<Feed> ?? new List<Feed>();
            feeds.ForEach(f =>
            {
                if (f.Equals(feedItem))
                {
                    f.FeedUrl = feedItem.FeedUrl;
                    f.Name = feedItem.Name;
                }
            });
            IsolatedStorageSettings.ApplicationSettings[Constants.RssData] = feeds;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static void DeleteFeed(Feed feed)
        {
            var feeds = IsolatedStorageSettings.ApplicationSettings[Constants.RssData] as List<Feed> ?? new List<Feed>();
            var newFeeds = feeds.Where(f => !f.Equals(feed)).ToList();
            IsolatedStorageSettings.ApplicationSettings[Constants.RssData] = newFeeds;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
