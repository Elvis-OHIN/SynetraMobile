using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SynetraMobile.Models;
using SynetraMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SynetraMobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private LoginModel loginModel;

        [ObservableProperty]
        private string email;
            
        [ObservableProperty]
        private bool isAuthenticated;

        private readonly ClientService clientService;
            
        public LoginViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            LoginModel = new();
            IsAuthenticated = false;
            GetEmailFromSecuredStorage();
        }

        [RelayCommand]
        private async Task Login()
        {
            await clientService.Login(LoginModel);
            GetEmailFromSecuredStorage();
        }

        [RelayCommand]
        private async Task Logout()
        {
            SecureStorage.Default.Remove("Authentication");
            IsAuthenticated = false;
            Email = "Guest";
            await Shell.Current.GoToAsync("..");
        }
        private async void GetEmailFromSecuredStorage()
        {
            if (!string.IsNullOrEmpty(Email) && Email! != "Guest")
            {
                IsAuthenticated = true;
                return;
            }
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null)
            {
                Email = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.Email!;
                IsAuthenticated = true;
                return;
            }
            Email = "Guest";
            IsAuthenticated = false;
        }
    }
}
