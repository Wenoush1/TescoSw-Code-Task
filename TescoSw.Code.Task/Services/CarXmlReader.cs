using System.Xml;
using System.Xml.Linq;
using TescoSw.Code.Task.Models;

namespace TescoSw.Code.Task.Services;

public static class CarXmlReader
{
    public static List<Car> Load(string filePath)
    {
        XDocument doc = XDocument.Load(filePath);
        XElement root = doc.Root ?? throw new InvalidDataException("The XML file is empty.");

        return root.Elements("Car")
            .Select(c => new Car
            (
                ModelName: GetRequiredElement(c, "ModelName").Value,
                SaleDate: XmlConvert.ToDateTime(GetRequiredElement(c, "SaleDate").Value,
                    XmlDateTimeSerializationMode.RoundtripKind),
                Price: XmlConvert.ToDouble(GetRequiredElement(c, "Price").Value),
                VatRate: XmlConvert.ToDouble(GetRequiredElement(c, "VatRate").Value)
            ))
            .ToList();
    }

    private static XElement GetRequiredElement(XElement parent, string name)
    {
        return parent.Element(name)
            ?? throw new InvalidDataException($"Missing element <{name}> in <{parent.Name}>.");
    }
}