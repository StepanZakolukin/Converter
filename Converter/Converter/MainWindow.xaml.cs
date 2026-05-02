using System.Windows;

namespace Converter;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(ViewModels.MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}