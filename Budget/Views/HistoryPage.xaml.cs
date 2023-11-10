using Budget.ViewModels;

namespace Budget.Views;

public partial class HistoryPage : ContentPage
{
	public HistoryPage(HistoryViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}