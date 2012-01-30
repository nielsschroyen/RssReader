using Microsoft.Phone.Controls;
using Reader.Controls;
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

        public List<PivotItem> PivotItems { get; set; }

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

        public void Update()
        {
            PivotItems.ForEach(p => ((PivotItemViewModel) p.DataContext).Update());
        }




        public event PropertyChangedEventHandler PropertyChanged;
    }
}
