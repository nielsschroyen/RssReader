using Reader.Models;
using Reader.ViewModels;

namespace Reader.Controls
{
    public partial class PivotItemControl
    {
        public PivotItemControl()
        {
            InitializeComponent();
        }

        private void ListBoxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ((PivotItemViewModel) DataContext).OpenItem((RssFeedItem) _listbox.SelectedItem);
        }
    }
}
