namespace Converter.BusinessLogic.Logic.DataEnricher;

public interface IDataEnricher
{
    void EnrichEmployeesXml(string resultPath);
    void EnrichSourceXml(string sourcePath);
}