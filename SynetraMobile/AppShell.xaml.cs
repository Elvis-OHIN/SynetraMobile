using SynetraMobile.Services;
using SynetraUtils.Models.DataManagement;

namespace SynetraMobile
{
    public partial class AppShell : Shell
    {
        private readonly UserService _userService;
        public AppShell()
        {
            InitializeComponent();
            _userService = new UserService();
            IsAuthorize();
            Routing.RegisterRoute(nameof(Views.ComputersPage), typeof(Views.ComputersPage));
            Routing.RegisterRoute(nameof(Views.ParcsPage), typeof(Views.ParcsPage));
            Routing.RegisterRoute(nameof(Views.RoomsPage), typeof(Views.RoomsPage));
            Routing.RegisterRoute(nameof(Views.LoginView), typeof(Views.LoginView));
        }

        private async void IsAuthorize()
        {
            
            var user = await _userService.GetMe();
            if (user is not null)
            {
                if (user.ParcId is null)
                {
                    Parc.IsVisible = true;
                    ViewParc.IsVisible = true;
                }
            }
        }
    }
}
