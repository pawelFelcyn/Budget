using Budget.ViewModels;

namespace Budget.Views;

public partial class CreateNewTransactionPage : ContentPage
{
    public CreateNewTransactionPage(CreateNewTansactionViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
    }
}