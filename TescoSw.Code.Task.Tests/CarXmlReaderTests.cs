using System.Globalization;
using TescoSw.Code.Task.Models;
using TescoSw.Code.Task.Services;

namespace TescoSw.Code.Task.Tests
{
    public class CarXmlReaderTests : IDisposable
    {
        private readonly List<string> _tempFiles = new List<string>();

        [Fact]
        public void Load_ValidFile_ReadsAllCarsWithCorrectValues()
        {
            string path = WriteTempFile(SoldCarsXml(
                CarXml("Škoda Oktávia", "2010-12-02T00:00:00", "500000", "20"),
                CarXml("Škoda Forman", "2000-12-04T00:00:00", "100000", "19")));

            List<Car> cars = CarXmlReader.Load(path);

            Assert.Equal(2, cars.Count);

            Assert.Equal("Škoda Oktávia", cars[0].ModelName);
            Assert.Equal(new DateTime(2010, 12, 2), cars[0].SaleDate);
            Assert.Equal(500000.0, cars[0].Price);
            Assert.Equal(20.0, cars[0].VatRate);

            Assert.Equal("Škoda Forman", cars[1].ModelName);
            Assert.Equal(new DateTime(2000, 12, 4), cars[1].SaleDate);
            Assert.Equal(100000.0, cars[1].Price);
            Assert.Equal(19.0, cars[1].VatRate);
        }

        [Fact]
        public void Load_ParsesNumbersIndependentlyOfCurrentCulture()
        {
            string path = WriteTempFile(SoldCarsXml(
                CarXml("Škoda Fabia", "2010-12-04T00:00:00", "350000.5", "20")));

            CultureInfo originalCulture = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("cs-CZ");

                List<Car> cars = CarXmlReader.Load(path);

                Assert.Equal(350000.5, cars[0].Price);
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }
        }

        [Fact]
        public void Load_FileWithoutCars_ReturnsEmptyList()
        {
            string path = WriteTempFile(SoldCarsXml());

            List<Car> cars = CarXmlReader.Load(path);

            Assert.Empty(cars);
        }

        [Fact]
        public void Load_CarWithoutPrice_ThrowsWithNameOfMissingElement()
        {
            string carWithoutPrice =
                "<Car>" +
                "<ModelName>Škoda Fabia</ModelName>" +
                "<SaleDate>2010-12-04T00:00:00</SaleDate>" +
                "<VatRate>20</VatRate>" +
                "</Car>";
            string path = WriteTempFile(SoldCarsXml(carWithoutPrice));

            InvalidDataException ex = Assert.Throws<InvalidDataException>(() => CarXmlReader.Load(path));

            Assert.Contains("<Price>", ex.Message);
        }

        [Theory]
        [InlineData("abc", "2010-12-04T00:00:00")]      // price is not a number
        [InlineData("350.000,-", "2010-12-04T00:00:00")] // price in the format from the assignment table
        [InlineData("350000", "4.12.2010")]             // Czech date format, must not be misread as 12 April
        public void Load_InvalidValue_ThrowsFormatException(string price, string saleDate)
        {
            string path = WriteTempFile(SoldCarsXml(
                CarXml("Škoda Fabia", saleDate, price, "20")));

            Assert.Throws<FormatException>(() => CarXmlReader.Load(path));
        }

        [Fact]
        public void Load_NonExistentFile_ThrowsFileNotFoundException()
        {
            string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xml");

            Assert.Throws<FileNotFoundException>(() => CarXmlReader.Load(path));
        }

        public void Dispose()
        {
            foreach (string file in _tempFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
        }

        // --- Helpers ---

        private string WriteTempFile(string content)
        {
            string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xml");
            File.WriteAllText(path, content);
            _tempFiles.Add(path);
            return path;
        }

        private static string SoldCarsXml(params string[] cars)
        {
            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                   "<SoldCars>" + string.Concat(cars) + "</SoldCars>";
        }

        private static string CarXml(string modelName, string saleDate, string price, string vatRate)
        {
            return "<Car>" +
                   "<ModelName>" + modelName + "</ModelName>" +
                   "<SaleDate>" + saleDate + "</SaleDate>" +
                   "<Price>" + price + "</Price>" +
                   "<VatRate>" + vatRate + "</VatRate>" +
                   "</Car>";
        }
    }
}
