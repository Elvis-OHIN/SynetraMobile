namespace SynetraMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.ComputersPage), typeof(Views.ComputersPage));
            Routing.RegisterRoute(nameof(Views.ParcsPage), typeof(Views.ParcsPage));
            Routing.RegisterRoute(nameof(Views.RoomsPage), typeof(Views.RoomsPage));
            Routing.RegisterRoute(nameof(Views.LoginView), typeof(Views.LoginView));
        }
    }
}
