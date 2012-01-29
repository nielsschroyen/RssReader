using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
