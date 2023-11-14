using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Budget.Dtos;


public sealed partial class CreateTransactionDto : Dto
{
    [ObservableProperty]
    private string _name;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    private decimal _amount = 0.01m;
    [ObservableProperty]
    private DateTime _transactionDate = DateTime.Now;
    [ObservableProperty]
    private Guid _categoryId;
}