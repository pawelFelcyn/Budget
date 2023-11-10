using CommunityToolkit.Mvvm.Input;

namespace Budget.ViewModels;

public sealed partial class HistoryViewModel : ViewModel
{
    public HistoryViewModel()
    {
        Title = "History";
    }

    [RelayCommand]
    private async Task GoToCreateNewPageAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            await Shell.Current.GoToAsync("//History/Create");
        }
        finally
        {
            IsBusy = false;
        }
    }
}