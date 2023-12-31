﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace InformationDirectoryOfСountries.Arhitecture;

public sealed class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private Window? _windowDialog;

    private Dictionary<string, Type> _windows { get; } = new Dictionary<string, Type>();


    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Configure(string key, Type windowType) => _windows.Add(key, windowType);

    public async Task ShowAsync(string windowKey, object parameter = null)
    {
        var window = await GetAndActivateWindowAsync(windowKey, parameter);
        window?.Show();
    }

    public async Task<bool?> ShowDialogAsync(string windowKey, object parameter = null)
    {
        Window window = await GetAndActivateWindowAsync(windowKey, parameter);
        window.Name = windowKey;

        _windowDialog = window;

        return window?.ShowDialog() ?? false;
    }

    private async Task<Window> GetAndActivateWindowAsync(string windowKey, object parameter = null)
    {
        if(_windows.TryGetValue(windowKey, out Type typeWindow) && _serviceProvider.GetRequiredService(typeWindow) is Window window)
        {

            if (window.DataContext is IActivable activable)
            {
                await activable.ActivateAsync(parameter);
            }

            return window;
        }

        return null;
    }

    public void CloseOpenedWindowDialog(string window)
    {
        if (_windowDialog?.Name == window)
        {
            _windowDialog?.Close();

            _windowDialog = null;
        }
    }
}