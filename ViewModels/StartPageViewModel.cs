using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Reader.Controls;
using Reader.Models;
using System.Collections.Generic;
using Reader.Workers;

namespace Reader.ViewModels
{
    public class StartPageViewModel :NotifyPropertyChangedBase
    {
        private List<Feed> _feeds;
        /// <summary>
        /// Title of the view
        /// </summary>
        public string Title { get { return AppResources.AppName; }
        }

        private List<PivotItem> _pivotItems;
        private Pivot _pivot;

        public List<PivotItem> PivotItems
        {
            get { return _pivotItems; }
            set { _pivotItems = value;
            NotifyPropertyChanged(() => PivotItems);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pivot"> </param>
        public StartPageViewModel(Pivot pivot)
        {
            _pivot = pivot;
            InitStorage();

            var items = RetrieveItemsFromStorage();

            items.Add(new SettingsControl {DataContext = new SettingsViewModel()});
            PivotItems = items;
           
            
        }

        private List<PivotItem> RetrieveItemsFromStorage()
        {
            var items = new List<PivotItem>();
            _feeds = IsolatedStorageSettings.ApplicationSettings[Constants.RssData] as List<Feed>;
            PhoneApplicationService.Current.State[Constants.RssData] = _feeds;
            if (_feeds != null)
            {
                _feeds.ForEach(f => items.Add(new PivotItemControl { DataContext = new PivotItemViewModel(f) }));
            }
            return items;
        }

        /// <summary>
        /// Update all the feeds
        /// </summary>
        public void Update()
        {
            PivotItems.ForEach(p =>
                                   {
                                       var pm =  p.DataContext as PivotItemViewModel;
                                       if(pm!=null)
                                         pm.Update();
                                   });
        }

        private void InitStorage()
        {

      //      if (!IsolatedStorageSettings.ApplicationSettings.Contains(Constants.RssData))   
      //        {
                  AddSampleData();
       //       }
        }

        private void AddSampleData()
        {
            var feeds = new List<Feed>
                            {
                                new Feed
                                    {
                                        FeedUrl = @"http://channel9.msdn.com/feeds/rss",
                                        Name = "channel 9"
                                    },
                                new Feed
                                    {
                                        FeedUrl =
                                            @"http://feeds.feedburner.com/tweakers/mixed",
                                        Name = "tweakers"
                                    }
                            };


            IsolatedStorageSettings.ApplicationSettings[Constants.RssData] = feeds;
            IsolatedStorageSettings.ApplicationSettings.Save();
            
        }


        public void ReInitialize()
        {
            if (PhoneApplicationService.Current.State.ContainsKey(Constants.AddedItem))
            {
                var addedItem = (Feed) PhoneApplicationService.Current.State[Constants.AddedItem];
                PhoneApplicationService.Current.State.Remove(Constants.AddedItem);
                var pvvm = new PivotItemViewModel(addedItem);
                PivotItems.Add(new PivotItemControl {DataContext = pvvm});
                //NotifyPropertyChanged(() => PivotItems);
                pvvm.Update();
                _pivot.ItemsSource = null;
                _pivot.ItemsSource = PivotItems;
            }

            if (PhoneApplicationService.Current.State.ContainsKey(Constants.EditedItem))
            {
                var addedItem = (Feed)PhoneApplicationService.Current.State[Constants.EditedItem];
                PhoneApplicationService.Current.State.Remove(Constants.EditedItem);
                PivotItems.ForEach(p =>
                                                  {
                                                      var pm = p.DataContext as PivotItemViewModel;
                                                      if(pm != null)
                                                          if (pm.Feed == addedItem)
                                                              pm.Update();
                                                  });
            }
     
            
        }
    }
}
