using CommunityToolkit.Mvvm.ComponentModel;

namespace Budget.ViewModels;

public sealed partial class CategoriesViewModel : ViewModel
{
    [ObservableProperty]
    private bool _expensesToggled;

    public CategoriesViewModel()
    {
        Title = "Categories";
    }
}