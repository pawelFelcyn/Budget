using Budget.Data.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Budget.ViewModels;

public sealed partial class CategoriesViewModel : ViewModel
{
    [ObservableProperty]
    private bool _isAddCategorySheetExpanded;
    [ObservableProperty]
    private string _newCategoryName;
    [ObservableProperty]
    private string _newCategoryColor = "#000000";
    //0 for expense, 1 for incom
    [ObservableProperty]
    private int _selectedCategoryType;
    public ObservableCollection<Category> ExpensesCategories { get; } = new()
    {
        new Category()
        {
            Name = "Test expense category 1",
            Color = "asdsad"
        },
        new Category()
        {
            Name = "Test expense category 2",
            Color = "asdsad"
        },
        new Category()
        {
            Name = "Test expense category 3",
            Color = "asdsad"
        },
    };

    public ObservableCollection<Category> IncomsCategories { get; } = new()
    {
        new Category()
        {
            Name = "Test incom category 1",
            Color = "asdsad"
        },
        new Category()
        {
            Name = "Test incom category 2",
            Color = "asdsad"
        },
        new Category()
        {
            Name = "Test incom category 3",
            Color = "asdsad"
        },
    };

    public CategoriesViewModel()
    {
        Title = "Categories";
    }

    [RelayCommand]
    private void ExpandAddCategorySheet()
    {
        IsAddCategorySheetExpanded = true;
    }

    [RelayCommand]
    private Task SaveCategoryAsync()
    {
        IsAddCategorySheetExpanded = false;
        return Task.CompletedTask;
    }

    [RelayCommand]
    private void ClearNewCategoryData()
    {
        NewCategoryName = null;
        NewCategoryColor = "#000000";
    }
}