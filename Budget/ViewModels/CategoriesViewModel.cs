using Budget.Data;
using Budget.Data.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Data;
using DevExpress.Xpo.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using Xamarin.Google.Crypto.Tink.Signature;

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
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasCategoryNameErrors))]
    private string _categoryNameErrors;
    private readonly DbContextFactory _dbContextFactory;
    private readonly ILogger<CategoriesViewModel> _logger;

    public CategoryType SelectedCategoryTypeEnum => (CategoryType)SelectedCategoryType;
    public bool HasCategoryNameErrors => CategoryNameErrors is not null;
    public ObservableCollection<Category> ExpensesCategories { get; } = new();

    public ObservableCollection<Category> IncomsCategories { get; } = new();

    public CategoriesViewModel(DbContextFactory dbContextFactory, ILogger<CategoriesViewModel> logger)
    {
        Title = "Categories";
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    [RelayCommand]
    private void ExpandAddCategorySheet()
    {
        IsAddCategorySheetExpanded = true;
    }

    [RelayCommand]
    private async Task SaveCategoryAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            using var dbContext = _dbContextFactory();
            if (!await Validate(dbContext))
            {
                return;
            }
            var category = new Category
            {
                Name = NewCategoryName,
                Color = NewCategoryColor,
                Type = SelectedCategoryTypeEnum,
            };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            AddToLocalCollection(category);
            await Shell.Current.DisplayAlert("Success", "Category created.", "Ok");
            IsAddCategorySheetExpanded = false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured when creating category");
            await Shell.Current.DisplayAlert("Error", "Something went wrong.", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void AddToLocalCollection(Category category)
    {
        if (SelectedCategoryTypeEnum == CategoryType.Expense)
        {
            ExpensesCategories.Add(category);
            return;
        }

        IncomsCategories.Add(category);
    }

    private async Task<bool> Validate(BudgetDbContext dbContext)
    {
        if (string.IsNullOrEmpty(NewCategoryName))
        {
            CategoryNameErrors = "Category name must not be empty.";
            return false;
        }

        if (NewCategoryName.Length > 50)
        {
            CategoryNameErrors = "Max Length of category name is 50 characters.";
            return false;
        }

        if (await dbContext.Categories.AnyAsync(c => c.Name == NewCategoryName && c.Type == SelectedCategoryTypeEnum))
        {
            CategoryNameErrors = "Category with this name already exists.";
            return false;
        }

        CategoryNameErrors = null;
        return true;
    }

    [RelayCommand]
    private void ClearNewCategoryData()
    {
        NewCategoryName = null;
        NewCategoryColor = "#000000";
        CategoryNameErrors = null;
    }
}