using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Reader.Models;
using Reader.Workers;

namespace Reader.ViewModels
{
    public class EditChannelViewModel:NotifyPropertyChangedBase
    {
        public Action SaveAction
        {
            get { return _saveAction; }
        }

        private Feed _feedItem;
        public Feed FeedItem
        {
            get { return _feedItem; }
            set { _feedItem = value;
                NotifyPropertyChanged(() => FeedItem);
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value;
                NotifyPropertyChanged(() => Title);
            }
        }

        /// <summary>
        /// Title of the view
        /// </summary>
        public string AppName { get { return AppResources.AppName; } }

        private ApplicationBarIconButton _saveButton;
        public ApplicationBarIconButton SaveButton
        {
            get { return _saveButton; }
            set { _saveButton = value;
                _saveButton.IsEnabled = FeedItem.IsValid;
                _saveButton.Click += SaveButtonClick;
            }
        }

        void SaveButtonClick(object sender, EventArgs e)
        {
            _saveAction();
        }


        private Feed _realFeed;

        private Action _saveAction;

        private void FeedPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveButton.IsEnabled = FeedItem.IsValid;
        }

        private void Add()
        {
            var feeds = IsolatedStorageSettings.ApplicationSettings[Constants.RssData] as List<Feed> ?? new List<Feed>();

            feeds.Add(FeedItem);
            IsolatedStorageSettings.ApplicationSettings[Constants.RssData] = feeds;
            IsolatedStorageSettings.ApplicationSettings.Save();


            PhoneApplicationService.Current.State[Constants.AddedItem] = FeedItem;
            ((PhoneApplicationFrame) Application.Current.RootVisual).GoBack();

        }

        private void Edit()
        {
            var feeds = IsolatedStorageSettings.ApplicationSettings[Constants.RssData] as List<Feed> ?? new List<Feed>();
            feeds.ForEach(f =>
                              {
                                  if (f.Equals(FeedItem))
                                  {
                                      f.FeedUrl = FeedItem.FeedUrl;
                                      f.Name = FeedItem.Name;
                                  }
                              });
            IsolatedStorageSettings.ApplicationSettings[Constants.RssData] = feeds;
            IsolatedStorageSettings.ApplicationSettings.Save();

            _realFeed.FeedUrl = FeedItem.FeedUrl;
            _realFeed.Name = FeedItem.Name;

            PhoneApplicationService.Current.State[Constants.EditedItem] = _realFeed;
            ((PhoneApplicationFrame)Application.Current.RootVisual).GoBack();
        }


        public void ReInitialize()
        {
            if (PhoneApplicationService.Current.State.ContainsKey(Constants.EditItem))
            {
                var editItem = (Feed)PhoneApplicationService.Current.State[Constants.EditItem];
                PhoneApplicationService.Current.State.Remove(Constants.EditItem);
                _realFeed = editItem;
                FeedItem = editItem.Clone();
                Title = AppResources.EditChannelTitle;
                _saveAction = Edit;
                FeedItem.PropertyChanged += FeedPropertyChanged;
            }
            else
            {
                FeedItem = new Feed();
                FeedItem.Name = "test";
                FeedItem.FeedUrl = @"http://channel9.msdn.com/feeds/rss";
                Title = AppResources.AddChannelTitle;
                _saveAction = Add;
                FeedItem.PropertyChanged += FeedPropertyChanged;
            }
        }
    }
}
