using Converter.BusinessLogic.Constants;
using Converter.BusinessLogic.Models;
using Converter.ViewModels;

namespace Converter.Extensions;

public static class EmployeeExtensions
{
    public static EmployeeGridRow ToPresentationModel(this Employee employee) => new()
    {
        LastName = employee.Name.LastName,
        FirstName = employee.Name.FirstName,
        Total = employee.SalaryInfo.TotalSalary,
        January = employee.SalaryInfo.GetAmountForMonth(MonthEn.January),
        February = employee.SalaryInfo.GetAmountForMonth(MonthEn.February),
        March = employee.SalaryInfo.GetAmountForMonth(MonthEn.March),
        April = employee.SalaryInfo.GetAmountForMonth(MonthEn.April),
        May = employee.SalaryInfo.GetAmountForMonth(MonthEn.May),
        June = employee.SalaryInfo.GetAmountForMonth(MonthEn.June),
        July = employee.SalaryInfo.GetAmountForMonth(MonthEn.July),
        August = employee.SalaryInfo.GetAmountForMonth(MonthEn.August),
        September = employee.SalaryInfo.GetAmountForMonth(MonthEn.September),
        October = employee.SalaryInfo.GetAmountForMonth(MonthEn.October),
        November = employee.SalaryInfo.GetAmountForMonth(MonthEn.November),
        December = employee.SalaryInfo.GetAmountForMonth(MonthEn.December),
    };
}