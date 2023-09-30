using System.Windows;

namespace InformationDirectoryOfСountries.Views;

public partial class CountryView : Window
{
    public CountryView()
    {
        InitializeComponent();
    }

    private void ButtonClickClose(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}