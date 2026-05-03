namespace Converter.BusinessLogic.AppOrchestrator;

public interface IAppOrchestrator
{
    void RunFullCycle(string sourceFilePath, string resultFilePath);
}