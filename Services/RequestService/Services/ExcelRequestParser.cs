using OfficeOpenXml;
using RequestService.Models.Domain;
using RequestService.Services.Interfaces;

namespace RequestService.Services;

public class ExcelRequestParser : IExcelRequestParser
{
    public List<ExcelRequest> GetExcelRequestsAsync(byte[] fileBytes)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        if (fileBytes == null || fileBytes.Length == 0 || !IsExcelFile(fileBytes))
        {
            return [];
        }
        
        var requests = new List<ExcelRequest>();
        
        using var stream = new MemoryStream(fileBytes);
        using var package = new ExcelPackage(stream);
        
        var worksheet = package.Workbook.Worksheets[0];
        var rowCount = worksheet.Dimension.Rows;
        
        for (int row = 2; row <= rowCount; row++)
        {
            var request = new ExcelRequest
            {
                City = worksheet.Cells[row, 2].Text,
                Street = worksheet.Cells[row, 3].Text,
                House = worksheet.Cells[row, 4].Text,
                Flat = worksheet.Cells[row, 5].Text,
                Room = worksheet.Cells[row, 6].Text,
                Device = worksheet.Cells[row, 7].Text,
                WorkType = worksheet.Cells[row, 8].Text,
                Inspector1 = worksheet.Cells[row, 9].Text,
                Inspector2 = worksheet.Cells[row, 10].Text
            };

            requests.Add(request);
        }

        return requests;
    }
    
    private bool IsExcelFile(byte[] fileBytes)
    {
        using (var stream = new MemoryStream(fileBytes))
        {
            using (var reader = new BinaryReader(stream))
            {
                
                var header = reader.ReadBytes(8);
                
                if (header[0] == 0x50 && header[1] == 0x4B)
                {
                    return true;
                }
                
                if (header[0] == 0xD0 && header[1] == 0xCF)
                {
                    return true;
                }
            }
        }
        return false;
    }
}