using Budget.Data;
using Budget.Data.Entities;
using Budget.Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Budget.Mappers;
using Microsoft.Extensions.Logging;
using Android.Icu.Util;

namespace Budget.ViewModels;

public sealed partial class CreateNewTansactionViewModel : ViewModel
{
    [ObservableProperty]
    private CreateTransactionDto _model = new();
    [ObservableProperty]
    private CategoryType _type;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasCategoryIdError))]
    private string _categoryIdError;
    private readonly DbContextFactory _dbContextFactory;
    private readonly ILogger<CreateNewTansactionViewModel> _logger;
    private readonly List<Category> _allCategories = new();

    public bool HasCategoryIdError => CategoryIdError is not null;
    public ObservableCollection<Category> Categories { get; } = new();
    public DateTime MinDate { get; } = new DateTime(2000, 1, 1);
    public DateTime MaxDate { get; } = DateTime.Now;
    public List<CategoryType> AllTypes { get; } = new()
    {
        CategoryType.Expense, CategoryType.Income
    };
    public Action ValidateUI { get; set; }

    public CreateNewTansactionViewModel(DbContextFactory dbContextFactory, ILogger<CreateNewTansactionViewModel> logger)
    {
        Title = "Create transaction";
        _dbContextFactory = dbContextFactory;
        _logger = logger;
        LadDataAsync();
    }

    [RelayCommand]
    private async Task LadDataAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            Categories.Clear();
            _allCategories.Clear();
            using var dbContext = _dbContextFactory();
            await foreach (var category in dbContext.Categories.AsAsyncEnumerable())
            {
                _allCategories.Add(category);

                if (category.Type == Type)
                {
                    Categories.Add(category);
                }
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsBusy) 
        {
            return;
        }

        try
        {
            if (!Validate())
            {
                return;
            }

            using var dbContext = _dbContextFactory();
            var transaction = Model.ToDomain();
            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            ClearModel();
            await Shell.Current.DisplayAlert("Info", "Succesfully created transaction", "Ok");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception thrown when trying to save transaction");
            await Shell.Current.DisplayAlert("Error", "Something went wrong", "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ClearModel()
    {
        Model.Name = null;
        Model.Description = null;
        Model.Amount = 0.01m;
        Model.TransactionDate = DateTime.Now;
        Model.CategoryId = null;
    }

    private bool Validate()
    {
        return ValidateCategoryId();
    }

    private bool ValidateCategoryId()
    {
        if (Model.CategoryId is null)
        {
            CategoryIdError = "Category must not be empty.";
            return false;
        }

        CategoryIdError = null;
        return true;
    }

    partial void OnTypeChanged(CategoryType oldValue, CategoryType newValue)
    {
        Categories.Clear();
        Model.CategoryId = null;
        foreach (var category in _allCategories)
        {
            if (category.Type == newValue)
            {
                Categories.Add(category);
            }
        }
    }
}