using Converter.BusinessLogic.Constants;
using Converter.BusinessLogic.DataEnricher;
using Converter.BusinessLogic.XmlTransformer;

namespace Converter.BusinessLogic.AppOrchestrator;

internal class AppOrchestrator(
    IXmlTransformer xmlTransformer,
    IDataEnricher dataEnricher) : IAppOrchestrator
{
    public void RunFullCycle(string sourceFilePath, string resultFilePath)
    {
        xmlTransformer.Transform(sourceFilePath, Resources.TransformFileUri, resultFilePath);
        dataEnricher.EnrichReport(resultFilePath);
        dataEnricher.EnrichSource(sourceFilePath);
    }
}