using Budget.Data;
using Budget.Data.Entities;
using Budget.Dtos;
using Bumptech.Glide.Load.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public List<CategoryType> AllTypes { get; } = new()
    {
        CategoryType.Expense, CategoryType.Income
    };

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
}