using System;
using Microsoft.Phone.Shell;
using Reader.ViewModels;

namespace Reader
{
    public partial class StartPage
    {
        private ApplicationBarIconButton _updateButton;
        private ApplicationBarIconButton _editButton;
        private ApplicationBarIconButton _deleteButton;
        public ApplicationBarIconButton EditButton { get { return _editButton; } }
        public ApplicationBarIconButton DeleteButton { get { return _deleteButton; } }
        public ApplicationBarIconButton UpdateButton { get { return _updateButton; } }

        public StartPage()
        {
            InitializeComponent();
            InitializeAppBar();
            DataContext = new StartPageViewModel(this);
           
        }

        private void InitializeAppBar()
        {
          //Hack to dynamically change buttons
            _updateButton = new ApplicationBarIconButton
                                {IconUri = new Uri("/Resources/Icons/appbar.sync.rest.png", UriKind.RelativeOrAbsolute), 
                                    Text = AppResources.Update};
            _updateButton.Click += UpdateClick;
            ApplicationBar.Buttons.Add(_updateButton);

            _editButton = new ApplicationBarIconButton(
           new Uri("/Resources/Icons/appbar.edit.rest.png", UriKind.Relative)) { Text = AppResources.Edit };
            _editButton.Click += UpdateClick;
            ApplicationBar.Buttons.Add(_editButton);

            _deleteButton = new ApplicationBarIconButton(
           new Uri("/Resources/Icons/appbar.delete.rest.png", UriKind.Relative)) { Text = AppResources.Delete };
            _deleteButton.Click += UpdateClick;
            ApplicationBar.Buttons.Add(_deleteButton);

        }

        private void UpdateClick(object sender, EventArgs e)
        {
            ((StartPageViewModel)DataContext).Update();
        }

        private void PhoneApplicationPageLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ((StartPageViewModel)DataContext).UpdateAll();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            ((StartPageViewModel)DataContext).ReInitialize();
            base.OnNavigatedTo(e);
        }
    }
}