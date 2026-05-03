using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Converter.BusinessLogic;
using Converter.BusinessLogic.AppOrchestrator;
using Converter.BusinessLogic.Constants;
using Converter.BusinessLogic.Models;
using Converter.BusinessLogic.Repositories;
using Converter.Constants;
using Converter.Extensions;

namespace Converter.ViewModels;

public partial class MainViewModel : ObservableObject
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
    private string newFirstName;
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddEntryCommand))]
    private string newLastName;
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddEntryCommand))]
    private string newAmount;
    [ObservableProperty] 
    [NotifyCanExecuteChangedFor(nameof(AddEntryCommand))]
    private string selectedMonthRu;
    
    [ObservableProperty] 
    private ObservableCollection<EmployeeGridRow> _employees = [];

    private readonly IAppOrchestrator _orchestrator;
    
    public MainViewModel(IAppOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
        SelectedMonthRu = MonthList.First();
    }

    public IReadOnlyCollection<string> MonthList => monthMap.Keys;
    
    [RelayCommand]
    private async Task LoadFile()
    {
        var dialog = new Microsoft.Win32.OpenFileDialog { Filter = FileFilters.XML };
        if (dialog.ShowDialog() != true) return;
        SourcePath = dialog.FileName;
        await RefreshUIFromSourceAsync();
    }
    
    [RelayCommand(CanExecute = nameof(CanAddEntry))]
    private async Task AddEntry()
    {
        if (string.IsNullOrEmpty(SourcePath)) return;
        
        new XmlPayRepository(SourcePath).AddPayment(
            new FullName
            {
                FirstName = NewFirstName, 
                LastName = NewLastName
            },
            new Salary 
            { 
                Month = monthMap[SelectedMonthRu],
                Amount = XmlNumberParser.Parse(NewAmount)
            });
        
        await RefreshUIFromSourceAsync();
        
        NewFirstName = NewLastName = NewAmount = string.Empty;
    }
    
    private bool CanAddEntry()
    {
        var isFieldsFilled = !string.IsNullOrWhiteSpace(NewFirstName) && 
                             !string.IsNullOrWhiteSpace(NewLastName);
        
        var isAmountValid = XmlNumberParser.TryParse(NewAmount, out var result) && result > 0;

        return isFieldsFilled && isAmountValid && !string.IsNullOrEmpty(SourcePath);
    }
    
    [RelayCommand(CanExecute = nameof(CanTransformFile))]
    private async Task TransformFile()
    {
        if (string.IsNullOrEmpty(SourcePath)) return;

        const string baseFileName = "employees.xml";
        var saveDialog = new Microsoft.Win32.SaveFileDialog 
        { 
            Filter = FileFilters.XML, 
            FileName = baseFileName 
        };

        if (saveDialog.ShowDialog() != true) return;

        await Task.Run(() =>
        {
            _orchestrator.RunFullCycle(SourcePath, saveDialog.FileName);
        });
    }

    private bool CanTransformFile() => !string.IsNullOrEmpty(SourcePath);

    private async Task RefreshUIFromSourceAsync()
    {
        var data = await Task.Run(() =>
            new XmlPayRepository(SourcePath).GetRawData());

        UpdateGrid(data);
    }

    private void UpdateGrid(IEnumerable<Employee> data)
    {
        Employees.Clear();
        foreach (var employee in data)
            Employees.Add(employee.ToPresentationModel());
    }
}