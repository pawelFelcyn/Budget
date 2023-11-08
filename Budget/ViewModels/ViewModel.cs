using CommunityToolkit.Mvvm.ComponentModel;

namespace Budget.ViewModels;

public partial class ViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;
    [ObservableProperty]
    private string _title;

    public bool IsNotBusy => !IsBusy;
}