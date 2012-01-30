using Microsoft.Phone.Controls;
using Reader.Controls;
using Reader.Models;
using System.Collections.Generic;

namespace Reader.ViewModels
{
    public class StartPageViewModel :NotifyPropertyChangedBase
    {

        /// <summary>
        /// Title of the view
        /// </summary>
        public string Title { get { return Text.AppName; } }

        private List<PivotItem> _pivotItems;
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
        public StartPageViewModel()
        {
            var items = new List<PivotItem>
                            {
                                new PivotItemControl
                                    {
                                        DataContext =
                                            new PivotItemViewModel(new Feed
                                                                       {
                                                                           FeedUrl = @"http://channel9.msdn.com/feeds/rss",
                                                                           Name = "channel 9"
                                                                       })
                                    }        ,
                                new PivotItemControl
                                    {
                                        DataContext =
                                            new PivotItemViewModel(new Feed
                                                                       {
                                                                           FeedUrl =
                                                                               @"http://feeds.feedburner.com/tweakers/mixed",
                                                                           Name = "tweakers"
                                                                       })
                                    }
                            };


            PivotItems = items;
            Update();
            
        }

        /// <summary>
        /// Update all the feeds
        /// </summary>
        public void Update()
        {
            PivotItems.ForEach(p => ((PivotItemViewModel) p.DataContext).Update());
        }
    }
}
