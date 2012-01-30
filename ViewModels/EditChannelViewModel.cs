using System;
using Microsoft.Phone.Shell;
using Reader.Models;

namespace Reader.ViewModels
{
    public class EditChannelViewModel
    {
        public Feed FeedItem { get; set; }
        public string Title { get; set; }

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
            }
        }


        private Feed _realFeed;

        private Action _saveAction;

        public EditChannelViewModel()
        {
            FeedItem = new Feed();
            FeedItem.FeedUrl = @"channel9.msdn.com/feeds/rss";
            Title = AppResources.AddChannelTitle;
            _saveAction = Add;

            FeedItem.PropertyChanged += Feed_PropertyChanged;
        }

        private void Feed_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SaveButton.IsEnabled = FeedItem.IsValid;
        }

        public EditChannelViewModel(Feed feed)
        {
            _realFeed = feed;
            FeedItem = feed.Clone();
            Title = AppResources.EditChannelTitle;
            _saveAction = Edit;
            FeedItem.PropertyChanged += Feed_PropertyChanged;
        }

        private void Add()
        {
            
        }

        private void Edit()
        {
            
        }


    }
}
