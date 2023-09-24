using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InformationDirectoryOfСountries.Arhitecture;

public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsHavePropertyChangeListner => PropertyChanged != null;

    protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    protected void SetAndNotifieIfChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if ((field?.Equals(value) ?? false) == false)
        {
            field = value;
            OnPropertyChanged(propertyName);
        }
    }
}