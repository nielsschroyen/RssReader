using Reader.Models;
using System.Collections.Generic;

namespace Reader.Workers
{
    public class ReadFeedCallbackArguments
    {
        public string ErrorMessage { get; set; }
        public List<RssFeedItem> FeedItems {get; set; }
    }
}
