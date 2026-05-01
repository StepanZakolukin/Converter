using Converter.Application.Logic.AppOrchestrator;
using Converter.Application.Logic.DataEnricher;
using Converter.Application.Logic.EmployeeRepository;
using Converter.Application.Logic.XmlTransformer;
using Microsoft.Extensions.DependencyInjection;

namespace Converter.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDataEnricher, DataEnricher>()
            .AddSingleton<IXmlTransformer, XsltTransformer>()
            .AddSingleton<IEmployeeRepository, EmployeeRepository>()
            .AddSingleton<IAppOrchestrator, AppOrchestrator>();
    }
}