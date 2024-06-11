using Microsoft.Maui.Controls;
using SynetraMobile.ViewModels;
using SynetraUtils.Models.DataManagement;

namespace SynetraMobile.Views;


public partial class ComputersPage : ContentPage
{
	public ComputersPage()
	{
		InitializeComponent();
	}

    private void OnParcSelected(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedOption = (Parc)picker.SelectedItem;

        var viewModel = (ComputerViewModel)this.BindingContext;

        viewModel.FilterBy(selectedOption);
    }
}