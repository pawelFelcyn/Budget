using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Maui.DataForm;

namespace Budget.Dtos;


public sealed partial class CreateTransactionDto : ObservableObject
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