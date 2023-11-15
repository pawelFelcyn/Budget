using Budget.Data;
using Budget.Dtos;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace Budget.ViewModels;

public sealed partial class OverviewViewModel : ViewModel
{
    private readonly DbContextFactory _dbContextFactory;
    private readonly ILogger<OverviewViewModel> _logger;

    public ObservableCollection<CategorySumChartDataDto> CategorySumChartData { get; } = new();

    public OverviewViewModel(DbContextFactory dbContextFactory, ILogger<OverviewViewModel> logger)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
        Title = "Overview";
        LoadDataAsync();
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            using var dbContext = _dbContextFactory();
            var data = dbContext
                      .Transactions
                      .Join(
                          dbContext.Categories,
                          transaction => transaction.CategoryId,
                          category => category.Id,
                          (transaction, category) => new { Transaction = transaction, Category = category })
                      .GroupBy(
                          grouped => new { grouped.Category.Name, grouped.Category.Color },
                          grouped => (double)grouped.Transaction.Amount)
                      .Select(grouped => new CategorySumChartDataDto
                      {
                          CategoryName = grouped.Key.Name,
                          CategoryColor = grouped.Key.Color,
                          TotalAmount = (decimal)grouped.Sum()
                      })
                      .AsAsyncEnumerable();

            await foreach (var d in data)
            {
                CategorySumChartData.Add(d);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error when loading chart data");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
