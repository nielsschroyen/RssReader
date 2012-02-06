using System;
using System.Runtime.Serialization;

namespace Reader.Models
{
    [DataContract]
    public class Feed:NotifyPropertyChangedBase
    {
        [DataMember]
        public Guid Id = Guid.NewGuid();
        private string _name;
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value;
                NotifyPropertyChanged(() => Name);
                NotifyPropertyChanged(() => IsValid);
            }
        }

        private string _feedUrl;
        [DataMember]
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
                return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(FeedUrl);
            }
        }


        public Feed Clone()
        {
            return new Feed {FeedUrl = _feedUrl, Name = _name, Id = Id};
        }

        public override bool Equals(object obj)
        {
            var f = obj as Feed;
            if (f != null)
                return f.Id == Id;
            return false;
        }
    }
}
