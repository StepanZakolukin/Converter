using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Converter.BusinessLogic.Constants;
using Converter.BusinessLogic.Extensions;
using Converter.BusinessLogic.Logic.AppOrchestrator;
using Converter.BusinessLogic.Logic.EmployeeRepository;
using Converter.BusinessLogic.Models;
using Converter.Constants;
using Converter.Extensions;

namespace Converter.ViewModels;

public partial class MainViewModel(
    IAppOrchestrator orchestrator,
    IEmployeeRepository employeeRepository)
    : ObservableObject
{
    private Dictionary<string, string> monthMap { get; } = new() 
    {
        { MonthRu.January, MonthEn.January },
        { MonthRu.February, MonthEn.February },
        { MonthRu.March, MonthEn.March },
        { MonthRu.April, MonthEn.April },
        { MonthRu.May, MonthEn.May },
        { MonthRu.June, MonthEn.June },
        { MonthRu.July, MonthEn.July },
        { MonthRu.August, MonthEn.August },
        { MonthRu.September, MonthEn.September },
        { MonthRu.October, MonthEn.October },
        { MonthRu.November, MonthEn.November },
        { MonthRu.December, MonthEn.December }
    };

    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddEntryCommand))]
    [NotifyCanExecuteChangedFor(nameof(TransformFileCommand))] 
    private string sourcePath;
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddEntryCommand))]
    private string newName;
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddEntryCommand))]
    private string newSurname;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddEntryCommand))]
    private string newAmount;
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddEntryCommand))]
    private string selectedMonthRu;
    
    [ObservableProperty] 
    private ObservableCollection<EmployeeGridRow> _employees = new();

    public IReadOnlyCollection<string> MonthList => monthMap.Keys;
    
    [RelayCommand]
    private void LoadFile()
    {
        var dialog = new Microsoft.Win32.OpenFileDialog { Filter = FileFilters.XML };
        if (dialog.ShowDialog() != true) return;
        SourcePath = dialog.FileName;
        RefreshUIFromSource();
    }
    
    [RelayCommand(CanExecute = nameof(CanAddEntry))]
    private void AddEntry()
    {
        if (string.IsNullOrEmpty(SourcePath)) return;
        
        employeeRepository.AddRecord(
            SourcePath,
            new FullName { FirstName = NewName, LastName = NewSurname},
            new Salary { Month = monthMap[SelectedMonthRu], Amount = DoubleExtensions.Parse(NewAmount) });
        
        RefreshUIFromSource();
        
        NewName = NewSurname = NewAmount = string.Empty;
    }
    
    private bool CanAddEntry()
    {
        var isFieldsFilled = !string.IsNullOrWhiteSpace(NewName) && 
                             !string.IsNullOrWhiteSpace(NewSurname);
        
        var isAmountValid = DoubleExtensions.TryParse(NewAmount, out var result) && result > 0;

        return isFieldsFilled && isAmountValid && !string.IsNullOrEmpty(SourcePath);
    }
    
    [RelayCommand(CanExecute = nameof(CanTransformFile))]
    private void TransformFile()
    {
        if (string.IsNullOrEmpty(SourcePath)) return;

        const string baseFileName = "employees.xml";
        var saveDialog = new Microsoft.Win32.SaveFileDialog { Filter = FileFilters.XML, FileName = baseFileName };
        if (saveDialog.ShowDialog() != true) return;
        var data = orchestrator.RunFullCycle(SourcePath, saveDialog.FileName);
        
        UpdateGrid(data);
    }

    private bool CanTransformFile() => !string.IsNullOrEmpty(SourcePath);

    private void RefreshUIFromSource()
    {
        UpdateGrid(employeeRepository.GetAllRow(SourcePath));
    }

    private void UpdateGrid(IEnumerable<Employee> data)
    {
        Employees.Clear();
        foreach (var employee in data)
            Employees.Add(employee.ToPresentationModel());
    }
}