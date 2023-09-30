using System.Threading.Tasks;
using System.Windows;

namespace InformationDirectoryOfСountries.Arhitecture;

public interface INavigationService
{
    Task ShowAsync(string windowKey, object parameter = null);

    Task<bool?> ShowDialogAsync(string windowKey, object parameter = null);

    void CloseOpenedWindowDialog(string window);
}