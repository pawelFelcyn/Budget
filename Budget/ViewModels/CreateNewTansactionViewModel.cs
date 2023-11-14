using Budget.Data;
using Budget.Data.Entities;
using Budget.Dtos;
using Bumptech.Glide.Load.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DevExpress.Maui.DataForm;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Budget.ViewModels;

public sealed partial class CreateNewTansactionViewModel : ViewModel
{
    [ObservableProperty]
    private CreateTransactionDto _model = new();
    [ObservableProperty]
    private CategoryType _type;
    private readonly DbContextFactory _dbContextFactory;
    private readonly List<Category> _allCategories = new();

    public ObservableCollection<Category> Categories { get; } = new();
    public DateTime MinDate { get; } = new DateTime(2000, 1, 1);
    public DateTime MaxDate { get; } = DateTime.Now;
    public List<CategoryType> AllTypes { get; } = new()
    {
        CategoryType.Expense, CategoryType.Income
    };
    public Action ValidateUI { get; set; }

    public CreateNewTansactionViewModel(DbContextFactory dbContextFactory)
    {
        Title = "Create transaction";
        _dbContextFactory = dbContextFactory;
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
            using var dbContext = _dbContextFactory();
            await foreach (var category in dbContext.Categories.AsAsyncEnumerable())
            {
                Categories.Add(category);
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
            Validate();
            ValidateUI?.Invoke();
            if (Model.HasErrors)
            {
                return;
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void Validate()
    {
        Model.ClearErrors();
        ValidateCaterogyId();
    }

    private void ValidateCaterogyId()
    {
        if (!Categories.Any(c => c.Id == Model.CategoryId))
        {
            Model.SetError(nameof(CreateTransactionDto.CategoryId), "You must specify category.");
        }
    }
}