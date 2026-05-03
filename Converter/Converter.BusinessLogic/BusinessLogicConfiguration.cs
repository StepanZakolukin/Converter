using Converter.BusinessLogic.AppOrchestrator;
using Converter.BusinessLogic.DataEnricher;
using Converter.BusinessLogic.XmlTransformer;
using Microsoft.Extensions.DependencyInjection;

namespace Converter.BusinessLogic;

public static class BusinessLogicConfiguration
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDataEnricher, DataEnricher.DataEnricher>()
            .AddSingleton<IXmlTransformer, XsltTransformer>()
            .AddSingleton<IAppOrchestrator, AppOrchestrator.AppOrchestrator>();
    }
}