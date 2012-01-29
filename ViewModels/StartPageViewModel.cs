using System;
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
          var manager = new RssManager();
            manager.ReadRssCompleted+=ManagerReadRssCompleted;

            manager.ReadFeedAsync(@"http://channel9.msdn.com/feeds/rss");
        }

        private void ManagerReadRssCompleted(object sender, ReadFeedCallbackArguments args)
        {
           var manager = sender as RssManager;
           if (manager != null)
               manager.ReadRssCompleted -= ManagerReadRssCompleted;

           if (String.IsNullOrEmpty(args.ErrorMessage))
           {
               FeedItems = args.FeedItems;
               PropertyChanged(null, new PropertyChangedEventArgs("FeedItems"));
           }
        }


  
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
