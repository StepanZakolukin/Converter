using System.Windows;
using Converter.BusinessLogic;
using Converter.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Converter;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private ServiceProvider serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services
            .AddBusinessLogicServices()
            .AddTransient<MainViewModel>()
            .AddSingleton<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var mainWindow = serviceProvider.GetService<MainWindow>();
        mainWindow!.Show();
    }
}