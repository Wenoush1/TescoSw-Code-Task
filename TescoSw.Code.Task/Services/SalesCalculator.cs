using TescoSw.Code.Task.Models;

namespace TescoSw.Code.Task.Services;

public static class SalesCalculator
{
    public static List<ModelSummary> GetWeekendTotalsByModel(IEnumerable<Car> cars)
    {
        return cars
            .GroupBy(c => c.ModelName)
            .Select(modelGroup =>
            {
                List<Car> weekendSales = modelGroup.Where(c => IsWeekend(c.SaleDate)).ToList();

                return new ModelSummary
                (
                    ModelName: modelGroup.Key,
                    TotalExcludingVat: weekendSales.Sum(c => c.Price),
                    TotalIncludingVat: weekendSales.Sum(PriceInclVat)
                );
            })
            .ToList();
    }

    private static double PriceInclVat(Car car)
    {
        return car.Price * (1 + car.VatRate / 100.0);
    }

    private static bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday
            || date.DayOfWeek == DayOfWeek.Sunday;
    }
}
