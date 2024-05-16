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
	private HttpClient _httpClient;
	private UserManager _userManager;
	public LoginView(Auth0Client client, HttpClient httpClient, UserManager userManager)
	{
		InitializeComponent();
		auth0Client = client;
		_httpClient = httpClient;
		_userManager = userManager;
	}
	private async void LoginButtonClicked(object sender, EventArgs e)
	{
		var loginResult = await auth0Client.LoginAsync(new { audience = "<YOUR_API_IDENTIFIER"});

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
		}
	}
	
	
}