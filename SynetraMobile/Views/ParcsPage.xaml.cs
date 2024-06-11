using SynetraMobile.ViewModels;

namespace SynetraMobile.Views;

public partial class ParcsPage : ContentPage
{
	public ParcsPage()
	{
		InitializeComponent();
    }
    void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = BindingContext as ParcViewModel;
        if (viewModel != null)
        {
            viewModel.OnSearchTextChanged(e.NewTextValue);
        }
    }
}