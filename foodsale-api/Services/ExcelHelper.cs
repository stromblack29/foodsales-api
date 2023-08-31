using foodsale_api.Interfaces;
using foodsale_api.Models;
using OfficeOpenXml;
using System.ComponentModel;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace foodsale_api.Services
{
    public class ExcelHelper : IExcelHelper<Food>
    {
        public List<Food> ReadExcel(string targetPath)
        {
            try
            {
                var resultList = new List<Food>();
                var d = new DirectoryInfo(targetPath);
                var files = d.GetFiles("*.xlsx");
                foreach (var file in files)
                {
                    var fileName = file.FullName;
                    using var package = new ExcelPackage(file);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;
                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                    {
                        var order = Convert.ToDateTime(workSheet.Cells[rowIterator, 1].Value);
                        var region = workSheet.Cells[rowIterator, 2].Value?.ToString();
                        var city = workSheet.Cells[rowIterator, 3].Value?.ToString();
                        var category = workSheet.Cells[rowIterator, 4].Value?.ToString();
                        var product = workSheet.Cells[rowIterator, 5].Value?.ToString();
                        var quantity = Convert.ToInt32(workSheet.Cells[rowIterator, 6].Value);
                        var unitprice = Convert.ToDecimal(workSheet.Cells[rowIterator, 7].Value);
                        var totalprice = workSheet.Cells[rowIterator, 8].Value?.ToString();
                        resultList.Add(new Food() 
                        {
                            OrderDate = order,
                            Region = region,
                            City = city,
                            Category = category,
                            Product = product,
                            Quantity = quantity,
                            UnitPrice = unitprice,
                        });
                    }
                }
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
