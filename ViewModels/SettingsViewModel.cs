using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using Reader.Models;
using Reader.Workers;

namespace Reader.ViewModels
{
    public class SettingsViewModel : NotifyPropertyChangedBase
    {
        public ICommand AddFeedCommand { get { return new DelegateCommand(o => AddFeed()); } }
        private bool _isAutoRefresh = true;
        public bool IsAutoRefresh
        {
            get { return _isAutoRefresh; }
            set { _isAutoRefresh = value;
                NotifyPropertyChanged(() => IsAutoRefresh);
            }
        }

       private void AddFeed()
       {
           ((PhoneApplicationFrame) Application.Current.RootVisual).Navigate(Constants.EditPageUri);
       }
    }
}
