using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Reader.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Reader.Workers
{
    /// <summary>
    /// RSS manager to read rss feeds
    /// Unsafe Singleton implementation
    /// So Refactor!
    /// </summary>
    public class RssManager
    {

        /// <summary>
        /// Tracks callbacks
        /// </summary>
        private Dictionary<Guid, Action<ReadFeedCallbackArguments>> _callbacks = new Dictionary<Guid, Action<ReadFeedCallbackArguments>>();

        /// <summary>
        /// Reads the relevant Rss feed and returns a list off RssFeedItems
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public void ReadFeed(string url, Action<ReadFeedCallbackArguments> callback)
        {
            Guid trackGuid = Guid.NewGuid();
            _callbacks.Add(trackGuid, callback);

            //create a new list of the rss feed items to return
            List<RssFeedItem> rssFeedItems = new List<RssFeedItem>();

            WebClient client = new WebClient();

            // Add a user agent header in case the 
            // requested URI contains a query.

            client.DownloadStringCompleted += Downloaded;
            client.DownloadStringAsync(new Uri(url), trackGuid);
        }

        /// <summary>
        /// Download is completed
        ///  *Craate list
        ///  *Do callback
        ///  *Remove callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">arguments of download call</param>
        private void Downloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (!e.Cancelled)
                {
                    var items = new List<RssFeedItem>();
                    XElement xml;


                    xml = XElement.Parse(e.Result);
                    foreach (var element in xml.Elements("channel").Elements("item"))
                    {
                        items.Add(new RssFeedItem
                        {
                            Description = Regex.Replace(element.Element("description").Value, @"<[^>]+>", ""),
                            Title = element.Element("title").Value,
                            //     ChannelId = int.Parse(element.Element("channel_id").Value),
                            //     ItemId = int.Parse(element.Element("item_id").Value),
                            Link = element.Element("link").Value,
                            PublishDate = DateTime.Parse(element.Element("pubDate").Value)
                        });

                    }
                    var trackGuid = (Guid)e.UserState;
                    _callbacks[trackGuid](new ReadFeedCallbackArguments { FeedItems = items });
                    _callbacks.Remove(trackGuid);

                }

            }
            catch (Exception ex)
            {
            }

        }

        #region Singleton stuff
        private static RssManager instance;

           private RssManager() { }

           public static RssManager Instance
           {
              get 
              {
                 if (instance == null)
                 {
                     instance = new RssManager();
                 }
                 return instance;
              }
           }

        #endregion

    }
}
