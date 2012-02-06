using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Controls;
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
        private readonly StartPage _pivotPage;

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
        /// <param name="pivotPage"> </param>
        public StartPageViewModel(StartPage pivotPage)
        {
            _pivotPage = pivotPage;
            var items = RetrieveItemsFromStorage();
            items.Add(new SettingsControl {DataContext = new SettingsViewModel()});
            PivotItems = items;
            _pivotPage._pivot.SelectionChanged+=PivotSelectionChanged;
            _pivotPage.EditButton.Click += EditItem;
            _pivotPage.DeleteButton.Click += DeleteItem;
        }

        private void DeleteItem(object sender, EventArgs e)
        {
            var selectedItem = _pivotPage._pivot.SelectedItem;
            if (selectedItem is PivotItemControl)
            {
                var index = _pivotPage._pivot.SelectedIndex;
                var feed = ((PivotItemViewModel) ((PivotItemControl) selectedItem).DataContext).Feed;

                ApplicationStorageManager.DeleteFeed(feed);

                var newItems = PivotItems.Where(p =>
                {
                    var pm = p.DataContext as PivotItemViewModel;
                    if (pm != null)
                    {
                        if (pm.Feed == feed)
                            return false;
                    }
                    return true;
                });

                PivotItems = newItems.ToList();
                _pivotPage._pivot.ItemsSource = PivotItems;
                _pivotPage._pivot.SelectedIndex = index  % (PivotItems.Count);
                _pivotPage._pivot.UpdateLayout();
            }
        }

        void EditItem(object sender, System.EventArgs e)
        {
        //    throw new System.NotImplementedException();
        }

        private void PivotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool canEdit = _pivotPage._pivot.SelectedItem is PivotItemControl;
                _pivotPage.EditButton.IsEnabled = canEdit;
                _pivotPage.DeleteButton.IsEnabled = canEdit;
                _pivotPage.UpdateButton.IsEnabled = canEdit;
        }


        private List<PivotItem> RetrieveItemsFromStorage()
        {
            var items = new List<PivotItem>();

            ApplicationStorageManager.InitializeStore(items);
            _feeds = ApplicationStorageManager.GetFeeds();

            if (_feeds != null)
            {
                _feeds.ForEach(f => items.Add(new PivotItemControl { DataContext = new PivotItemViewModel(f) }));
            }
            return items;
        }

        /// <summary>
        /// Update the selected feed
        /// </summary>
        public void Update()
        {
            var selectedItem = _pivotPage._pivot.SelectedItem;

            var pivotItemControl = selectedItem as PivotItemControl;
            if (pivotItemControl == null) return;
            var pm = pivotItemControl.DataContext as PivotItemViewModel;
            if (pm != null) pm.Update();
        }


   


        public void ReInitialize()
        {
            if (PhoneApplicationService.Current.State.ContainsKey(Constants.AddedItem))
            {
                var addedItem = (Feed) PhoneApplicationService.Current.State[Constants.AddedItem];
                PhoneApplicationService.Current.State.Remove(Constants.AddedItem);
                var pvvm = new PivotItemViewModel(addedItem);
                PivotItems.Add(new PivotItemControl {DataContext = pvvm});
                pvvm.Update();
                _pivotPage._pivot.ItemsSource = null;
                _pivotPage._pivot.ItemsSource = PivotItems;
                _pivotPage._pivot.SelectedIndex = _pivotPage._pivot.Items.Count - 1;
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
