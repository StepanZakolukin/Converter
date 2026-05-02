using Converter.BusinessLogic.Logic.AppOrchestrator;
using Converter.BusinessLogic.Logic.DataEnricher;
using Converter.BusinessLogic.Logic.EmployeeRepository;
using Converter.BusinessLogic.Logic.XmlTransformer;
using Microsoft.Extensions.DependencyInjection;

namespace Converter.BusinessLogic;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDataEnricher, DataEnricher>()
            .AddSingleton<IXmlTransformer, XsltTransformer>()
            .AddSingleton<IEmployeeRepository, EmployeeRepository>()
            .AddSingleton<IAppOrchestrator, AppOrchestrator>();
    }
}