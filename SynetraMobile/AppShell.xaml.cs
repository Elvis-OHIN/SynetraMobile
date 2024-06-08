namespace SynetraMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.LoginView), typeof(Views.LoginView));
        }
    }
}
