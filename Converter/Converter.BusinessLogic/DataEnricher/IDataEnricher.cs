namespace Converter.BusinessLogic.DataEnricher;

public interface IDataEnricher
{
    void EnrichReport(string resultPath);
    void EnrichSource(string sourcePath);
}