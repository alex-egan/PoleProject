using System;
using System.Collections.Generic;
using OfficeOpenXml;

namespace PoleProject
{
    public class Excel
    {
        public Excel()
        {
            
        }

        public List<double> readEastings(System.IO.FileInfo fileInfo, int savedRow)
        {
            List<double> eastings = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Values"];

            // get number of rows and columns in the sheet
            int rows = worksheet.Dimension.Rows; // 20
            int columns = worksheet.Dimension.Columns; // 7

            //Console.WriteLine("*******************************");

            // loop through the worksheet rows and columns
            for (int i = 2; i < 15; i++)
            {
                double content = Convert.ToDouble(worksheet.Cells[i, 3].Value.ToString());
                eastings.Add(content);
                //Console.WriteLine(content);
                //Console.WriteLine("*******************************");
            }

            return eastings;
        }

        public List<double> readNorthings(System.IO.FileInfo fileInfo, int savedRow)
        {
            List<double> northings = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Values"];

            // get number of rows and columns in the sheet
            int rows = worksheet.Dimension.Rows; // 20
            int columns = worksheet.Dimension.Columns; // 7

            //Console.WriteLine("*******************************");

            // loop through the worksheet rows and columns
            for (int i = 2; i < 15; i++)
            {
                double content = Convert.ToDouble(worksheet.Cells[i, 2].Value.ToString());
                northings.Add(content);
                //Console.WriteLine(content);
                //Console.WriteLine("*******************************");
            }

            return northings;
        }

        public List<double> readElevations(System.IO.FileInfo fileInfo, int savedRow)
        {
            List<double> elevations = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets["Values"];

            // get number of rows and columns in the sheet
            int rows = worksheet.Dimension.Rows; // 20
            int columns = worksheet.Dimension.Columns; // 7

            //Console.WriteLine("*******************************");

            // loop through the worksheet rows and columns
            for (int i = 2; i < 15; i++)
            {
                double content = Convert.ToDouble(worksheet.Cells[i, 4].Value.ToString());
                elevations.Add(content);
                //Console.WriteLine(content);
                //Console.WriteLine("*******************************");
            }

            return elevations;
        }
    }
}
