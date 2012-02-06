using System;
using System.Linq;
using System.Net;
using Reader.Models;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Reader.Workers
{
    /// <summary>
    /// RSS manager to read rss feeds
    /// </summary>
    public class RssManager
    {
        public delegate void ReadRssEventHandler(object sender, ReadFeedCallbackArguments e);
        public event ReadRssEventHandler ReadRssCompleted;
    
        /// <summary>
        /// Reads the relevant Rss feed and returns a list off RssFeedItems
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public void ReadFeedAsync(string url)
        {
            var client = new WebClient();

            // Add a user agent header in case the 
            // requested URI contains a query.

            client.DownloadStringCompleted += Downloaded;
            try
            {
                client.DownloadStringAsync(new Uri(url));
            }
            catch (Exception)
            {
                
             //   throw;
            }
         
        }

        /// <summary>
        /// Download is completed
        ///  *Craate list
        ///  *sent event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">arguments of download call</param>
        private void Downloaded(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (!e.Cancelled)
                {
                    var xml = XElement.Parse(e.Result);
                    var items = xml.Elements("channel").Elements("item").Select(element => new RssFeedItem
                                                                                               {
                                                                                                   Description = Regex.Replace(ReadElement(element, "description"), @"<[^>]+>", ""),
                                                                                                   Title = ReadElement(element, "title"),
                                                                                                   Link = ReadElement(element, "link"), 
                                                                                                   PublishDate = DateTime.Parse(ReadElement(element, "pubDate"))
                                                                                               }).ToList();

                    if (ReadRssCompleted != null)
                        ReadRssCompleted(this, new ReadFeedCallbackArguments {FeedItems = items});
                }

            }
            catch (Exception)
            {
                  if (ReadRssCompleted != null)
                        ReadRssCompleted(this, new ReadFeedCallbackArguments {ErrorMessage = "Error occurred..."});
            }

        }

        /// <summary>
        /// Read element into string
        /// </summary>
        /// <param name="element">Element to read</param>
        /// <param name="elementName">Name of the element</param>
        /// <returns></returns>
        private static string ReadElement(XElement element, string elementName)
        {
            var val = element.Element(elementName);
            return val==null? "" : val.Value;
        }
    }
}
