using SynetraMobile.ViewModels;
using SynetraUtils.Models.DataManagement;

namespace SynetraMobile.Views;

public partial class RoomsPage : ContentPage
{
	public RoomsPage()
	{
		InitializeComponent();
	}
    private void OnParcSelected(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedOption = (Parc)picker.SelectedItem;

        var viewModel = (RoomViewModel)this.BindingContext;

        viewModel.FilterBy(selectedOption);
    }
}