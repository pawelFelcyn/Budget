using Budget.ViewModels;

namespace Budget.Views;

public partial class OverviewPage : ContentPage
{
	public OverviewPage(OverviewViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}