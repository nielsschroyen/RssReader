using System;
using Reader.ViewModels;

namespace Reader
{
    public partial class StartPage
    {
        public StartPage()
        {
            InitializeComponent();
            this.DataContext = new StartPageViewModel();

        }

        private void update_Click(object sender, EventArgs e)
        {
            ((StartPageViewModel)DataContext).Update();
        }
    }
}