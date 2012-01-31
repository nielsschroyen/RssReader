using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Reader.Models;
using Reader.Workers;

namespace Reader
{
    public partial class FeedItemPage
    {
        public FeedItemPage()
        {
            InitializeComponent();
             DataContext =  PhoneApplicationService.Current.State[Constants.OpenFeed];
             PhoneApplicationService.Current.State.Remove(Constants.OpenFeed);
        }
    }

    


}