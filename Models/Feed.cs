using System;

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
                NotifyPropertyChanged(() => IsValid);
            }
        }

        private string _feedUrl;
        public string FeedUrl
        {
            get { return _feedUrl; }
            set { _feedUrl = value;
                NotifyPropertyChanged(() => FeedUrl);
                NotifyPropertyChanged(() => IsValid);
            }
        }

        public bool IsValid
        {
            get
            {
                if(!string.IsNullOrEmpty(Name)&& Uri.IsWellFormedUriString(FeedUrl,UriKind.RelativeOrAbsolute))
                    return true;
                return false;
            }
        }


        public Feed Clone()
        {
            return new Feed {FeedUrl = _feedUrl, Name = _name};
        }
    }
}
