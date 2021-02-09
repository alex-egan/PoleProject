using System;
using System.Collections.Generic;
using OfficeOpenXml;
using IronXL;

namespace PoleProject
{
    public class Excel
    {
        public Excel()
        {
            
        }

        public void readExcelFile(System.IO.FileInfo fileInfo, int savedRow)
        {
            ExcelPackage package = new ExcelPackage(fileInfo);
            using (ExcelWorksheet worksheet = package.Workbook.Worksheets[1])
            {
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;
                Console.WriteLine(colCount);
            }
        }

        public List<double> readExcelFilePractice(string fileName, int savedRow)
        {
            List<double> northings = new List<double>();
            List<double> elevations = new List<double>();

            WorkBook workbook = WorkBook.Load(fileName);
            WorkSheet sheet = workbook.DefaultWorkSheet;

            // Make sure to use the savedRow integer here instead of "C2"
            double easting = Convert.ToDouble(sheet["C2"]);
            int rowCount = 3;

            while (Convert.ToDouble(sheet["C" + Convert.ToString(rowCount)]) == easting)
            {
                northings.Add(Convert.ToDouble(sheet["B" + Convert.ToString(rowCount)]));
                elevations.Add(Convert.ToDouble(sheet["D" + Convert.ToString(rowCount)]));
                rowCount += 1;
            }

            savedRow = rowCount;
            savedRow += 1;

            Console.WriteLine(northings);
            Console.WriteLine(elevations);

            return northings;

        }
    }
}
