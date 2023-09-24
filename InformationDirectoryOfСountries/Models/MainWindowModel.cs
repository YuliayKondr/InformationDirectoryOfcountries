using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationDirectoryOfСountries.Arhitecture;

namespace InformationDirectoryOfСountries.Models
{
    public sealed class MainWindowModel : BaseNotifyPropertyChanged, IActivable
    {
        public MainWindowModel()
        {
            LoadContriesCommand = new AsyncRelayCommand(LoadCountriesAsync);
        }

        private IAsyncRelayCommand LoadContriesCommand { get; }

        public Task ActivateAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        private async Task LoadCountriesAsync()
        {

        }
    }
}