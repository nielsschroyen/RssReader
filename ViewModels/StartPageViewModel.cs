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
using Reader.Workers;
using Reader.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace Reader.ViewModels
{
    public class StartPageViewModel :INotifyPropertyChanged
    {

        /// <summary>
        /// Title of the view
        /// </summary>
        public string Title { get { return Text.AppName; } }

        /// <summary>
        /// Listbox items
        /// </summary>
        public List<RssFeedItem> FeedItems {get;set;}


        /// <summary>
        /// Constructor
        /// </summary>
        public StartPageViewModel()
        {
            Update();
            
        }

        /// <summary>
        /// Update the feed
        /// </summary>
        public void Update()
        {
           RssManager.Instance.ReadFeed(@"http://channel9.msdn.com/feeds/rss", RssRetrieved);
        }


        /// <summary>
        /// Callback that is executed after thee feed is downloaded
        /// </summary>
        /// <param name="args"></param>
        private void RssRetrieved(ReadFeedCallbackArguments args)
        {
            if(String.IsNullOrEmpty(args.ErrorMessage))
            {
            FeedItems = args.FeedItems;
            PropertyChanged(null, new PropertyChangedEventArgs("FeedItems"));
            }
         }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
