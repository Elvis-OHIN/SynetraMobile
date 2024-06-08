using System.Diagnostics;
using System.Net.Http;
using Auth0.OidcClient;
using MauiAuth0App;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using SynetraMobile.Models;
using SynetraMobile.Services;

namespace SynetraMobile.Views;

public partial class LoginView : ContentPage
{
	private readonly Auth0Client auth0Client;
    private readonly ClientService clientService = new ClientService();
    private HttpClient _httpClient;
	private UserManager _userManager;
	public LoginView()
	{
		InitializeComponent();
	}
	private async void LoginButtonClicked(object sender, EventArgs e)
	{
		LoginModel loginModel = new LoginModel();
		loginModel.Email = Email.Text;
		loginModel.Password = Password.Text;
        var token = await clientService.Login(loginModel);


        if (token == true)
        {
            var mainPage = new AppShell();
            NavigationPage.SetHasBackButton(mainPage, false);
            await Navigation.PushAsync(mainPage);
        }
        else
        {
            // Affichez un message d'erreur
            await DisplayAlert("Erreur", "Échec de la connexion", "OK");
        }

        /*var loginResult = await auth0Client.LoginAsync(new { audience = "https://localhost:7082/login" });

		if (!loginResult.IsError)
		{
			Email.Text = loginResult.User.Identity.Name;
			UserPictureImg.Source = loginResult.User
				.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

			try
			{
				await SecureStorage.Default.SetAsync("access_token", loginResult.AccessToken);
				await SecureStorage.Default.SetAsync("id_token", loginResult.IdentityToken);

				if (loginResult.RefreshToken != null)
				{
					await SecureStorage.Default.SetAsync("refresh_token", loginResult.RefreshToken); 
				}
			} catch (Exception ex)
			{
				await DisplayAlert("Error", ex.Message, "Ok");
			}
		}
		else
		{
			await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
		}*/
	}
	
	
}