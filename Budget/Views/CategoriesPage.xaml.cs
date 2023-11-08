using Budget.ViewModels;
using IeuanWalker.Maui.Switch.Events;
using IeuanWalker.Maui.Switch.Helpers;
using IeuanWalker.Maui.Switch;

namespace Budget.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(CategoriesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}