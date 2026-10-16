using TescoSw.Code.Task.Models;
using TescoSw.Code.Task.Services;

namespace TescoSw.Code.Task.Tests
{
    public class SalesCalculatorTests
    {
        [Fact]
        public void GetWeekendTotalsByModel_SumsOnlySaturdayAndSundaySales()
        {
            List<Car> cars = new List<Car>
            {
                CreateCar("Škoda Fabia", 2010, 12, 3, 100000),  // Friday
                CreateCar("Škoda Fabia", 2010, 12, 4, 200000),  // Saturday
                CreateCar("Škoda Fabia", 2010, 12, 5, 300000),  // Sunday
                CreateCar("Škoda Fabia", 2010, 12, 6, 400000)   // Monday
            };

            ModelSummary summary = Assert.Single(SalesCalculator.GetWeekendTotalsByModel(cars));

            Assert.Equal(500000, summary.TotalExcludingVat, precision: 2);
        }

        [Theory]
        [InlineData(2010, 12, 4, true)]    // Saturday
        [InlineData(2010, 12, 5, true)]    // Sunday
        [InlineData(2010, 12, 6, false)]   // Monday
        [InlineData(2010, 12, 7, false)]   // Tuesday
        [InlineData(2010, 12, 8, false)]   // Wednesday
        [InlineData(2010, 12, 9, false)]   // Thursday
        [InlineData(2010, 12, 10, false)]  // Friday
        public void GetWeekendTotalsByModel_IncludesSaleOnlyWhenSoldOnWeekend(
            int year, int month, int day, bool isWeekend)
        {
            List<Car> cars = new List<Car> { CreateCar("Škoda Fabia", year, month, day, 350000) };

            ModelSummary summary = Assert.Single(SalesCalculator.GetWeekendTotalsByModel(cars));

            double expectedTotal = isWeekend ? 350000 : 0;
            Assert.Equal(expectedTotal, summary.TotalExcludingVat, precision: 2);
        }

        [Fact]
        public void GetWeekendTotalsByModel_AppliesVatRateOfEachCarSeparately()
        {
            List<Car> cars = new List<Car>
            {
                CreateCar("Škoda Felicia", 2000, 12, 2, 100000, vatRate: 20),  // Saturday
                CreateCar("Škoda Felicia", 2000, 12, 3, 100000, vatRate: 19)   // Sunday
            };

            ModelSummary summary = Assert.Single(SalesCalculator.GetWeekendTotalsByModel(cars));

            Assert.Equal(200000, summary.TotalExcludingVat, precision: 2);
            Assert.Equal(120000 + 119000, summary.TotalIncludingVat, precision: 2);
        }

        [Fact]
        public void GetWeekendTotalsByModel_ModelWithoutWeekendSales_IsIncludedWithZeroTotals()
        {
            List<Car> cars = new List<Car>
            {
                CreateCar("Škoda Favorit", 2000, 12, 5, 80000, vatRate: 19)  // Tuesday
            };

            ModelSummary summary = Assert.Single(SalesCalculator.GetWeekendTotalsByModel(cars));

            AssertSummary(summary, "Škoda Favorit", 0, 0);
        }

        [Fact]
        public void GetWeekendTotalsByModel_ReturnsOneSummaryPerModelInOrderOfFirstAppearance()
        {
            List<Car> cars = new List<Car>
            {
                CreateCar("Škoda Fabia", 2010, 12, 4, 350000),    // Saturday
                CreateCar("Škoda Oktávia", 2010, 12, 4, 500000),  // Saturday
                CreateCar("Škoda Fabia", 2010, 12, 5, 350000)     // Sunday
            };

            List<ModelSummary> summaries = SalesCalculator.GetWeekendTotalsByModel(cars);

            Assert.Equal(2, summaries.Count);
            AssertSummary(summaries[0], "Škoda Fabia", 700000, 840000);
            AssertSummary(summaries[1], "Škoda Oktávia", 500000, 600000);
        }

        [Fact]
        public void GetWeekendTotalsByModel_NoCars_ReturnsEmptyList()
        {
            List<ModelSummary> summaries = SalesCalculator.GetWeekendTotalsByModel(new List<Car>());

            Assert.Empty(summaries);
        }

        [Fact]
        public void GetWeekendTotalsByModel_AssignmentData_ReturnsExpectedTotals()
        {
            // The full table from the assignment
            List<Car> cars = new List<Car>
            {
                CreateCar("Škoda Oktávia", 2010, 12, 2, 500000, 20),
                CreateCar("Škoda Felicia", 2000, 12, 3, 210000, 20),
                CreateCar("Škoda Fabia",   2010, 12, 4, 350000, 20),
                CreateCar("Škoda Oktávia", 2010, 12, 4, 500000, 20),
                CreateCar("Škoda Oktávia", 2010, 12, 5, 500000, 20),
                CreateCar("Škoda Fabia",   2010, 12, 5, 350000, 20),
                CreateCar("Škoda Fabia",   2010, 12, 6, 350000, 20),
                CreateCar("Škoda Forman",  2000, 12, 4, 100000, 19),
                CreateCar("Škoda Favorit", 2000, 12, 5, 80000, 19),
                CreateCar("Škoda Forman",  2000, 12, 6, 100000, 19),
                CreateCar("Škoda Felicia", 2000, 12, 3, 210000, 19),
                CreateCar("Škoda Felicia", 2000, 12, 2, 210000, 19),
                CreateCar("Škoda Oktávia", 2010, 12, 7, 500000, 20)
            };

            List<ModelSummary> summaries = SalesCalculator.GetWeekendTotalsByModel(cars);

            Assert.Equal(5, summaries.Count);
            AssertSummary(summaries[0], "Škoda Oktávia", 1000000, 1200000);
            AssertSummary(summaries[1], "Škoda Felicia", 630000, 751800);
            AssertSummary(summaries[2], "Škoda Fabia", 700000, 840000);
            AssertSummary(summaries[3], "Škoda Forman", 0, 0);
            AssertSummary(summaries[4], "Škoda Favorit", 0, 0);
        }

        private static Car CreateCar(string modelName, int year, int month, int day,
            double price, double vatRate = 20)
        {
            return new Car
            (
                ModelName: modelName,
                SaleDate: new DateTime(year, month, day),
                Price: price,
                VatRate: vatRate
            );
        }

        private static void AssertSummary(ModelSummary summary, string expectedModelName,
            double expectedExclVat, double expectedInclVat)
        {
            Assert.Equal(expectedModelName, summary.ModelName);
            Assert.Equal(expectedExclVat, summary.TotalExcludingVat, precision: 2);
            Assert.Equal(expectedInclVat, summary.TotalIncludingVat, precision: 2);
        }
    }
}
