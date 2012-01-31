using System;

namespace Reader.Workers
{
    public class Constants
    {
        public static readonly string RssData = "rssData.xml";
        public static readonly string StorageName = "rssReader";
        public static readonly string AddedItem = "addedItem" ;
        public static readonly string AddItem = "addtItem";
        public static readonly string EditItem = "editItem";
        public static readonly string EditedItem = "editedItem";
        public static readonly Uri EditPageUri = new Uri("/EditChannel.Xaml",UriKind.RelativeOrAbsolute);
    }
}
