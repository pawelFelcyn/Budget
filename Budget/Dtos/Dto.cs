using Android.Icu.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace Budget.Dtos;

public class Dto : ObservableObject, INotifyDataErrorInfo
{
    private readonly Dictionary<string, string> _errors = new();

    public void SetError(string propertyName, string value)
    {
        _errors[propertyName] = value;
        ErrorsChanged?.Invoke(this, new(propertyName));
    }

    public bool HasErrors => _errors.Any(e => e.Value is not null);

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public IEnumerable GetErrors(string propertyName)
    {
        if (_errors.ContainsKey(propertyName))
        {
            return new string[] { _errors[propertyName] };
        }

        return null;
    }

    public void ClearErrors()
    {
        _errors.Clear();
        ErrorsChanged?.Invoke(this, new(null));
    }
}