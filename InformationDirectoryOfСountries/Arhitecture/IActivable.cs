using System.Threading.Tasks;

namespace InformationDirectoryOfСountries.Arhitecture;

public interface IActivable
{
    Task ActivateAsync(object parameter);
}