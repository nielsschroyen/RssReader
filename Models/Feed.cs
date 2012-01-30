namespace Reader.Models
{
    public class Feed:NotifyPropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;
                NotifyPropertyChanged(() => Name);
            }
        }

        private string _feedUrl;
        public string FeedUrl
        {
            get { return _feedUrl; }
            set { _feedUrl = value;
                NotifyPropertyChanged(() => FeedUrl);
            }
        }
    }
}
