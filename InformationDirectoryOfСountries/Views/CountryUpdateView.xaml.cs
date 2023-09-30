using System.Windows;

namespace InformationDirectoryOfСountries.Views;

public partial class CountryUpdateView : Window
{
    public CountryUpdateView()
    {
        InitializeComponent();
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}