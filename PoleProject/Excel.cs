using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace PoleProject
{
    public class Excel
    {
        public Excel()
        {

        }

        int EASTINGSCOLUMN = 3;
        int NORTHINGSCOLUMN = 2;
        int ELEVATIONSCOLUMN = 4;
        string sheetName = "Sheet1";
        //string sheetName = "Values";

        public int readEastings(System.IO.FileInfo fileInfo, int savedRow)
        {
            List<double> eastings = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

            double eastingValue = Convert.ToDouble(worksheet.Cells[savedRow, EASTINGSCOLUMN].Value.ToString());
            bool iterate = true;
            int rowCount = 0;

            while (iterate == true)
            {
                double nextEastingValue;
                try
                {
                    nextEastingValue = Convert.ToDouble(worksheet.Cells[savedRow, EASTINGSCOLUMN].Value.ToString());
                    //Console.WriteLine(nextEastingValue);
                }
                catch
                {
                    Console.WriteLine("about to break");
                    break;
                }

                if (nextEastingValue != eastingValue)
                {
                    iterate = false;
                    break;
                }
                rowCount++;
                savedRow++;
            }
            //Console.WriteLine(rowCount);
            //Console.WriteLine(savedRow);

            return rowCount;
        }

        public int returnSavedRow(int rowCount, int savedRow)
        {
            savedRow = savedRow + rowCount;
            //Console.WriteLine("SavedRow: " + savedRow.ToString());

            return savedRow;
        }

        public List<double> readNorthings(System.IO.FileInfo fileInfo, int rowCount, int savedRow)
        {
            List<double> northings = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

            // get number of rows and columns in the sheet
            int rows = worksheet.Dimension.Rows; // 20
            int columns = worksheet.Dimension.Columns; // 7

            //Console.WriteLine("*******************************");

            // loop through the worksheet rows and columns
            for (int i = savedRow - rowCount; i < savedRow; i++)
            {
                double content = Convert.ToDouble(worksheet.Cells[i, NORTHINGSCOLUMN].Value.ToString());
                northings.Add(content);
                //Console.WriteLine(content);
                //Console.WriteLine("*******************************");
            }

            return northings;
        }

        public List<double> readElevations(System.IO.FileInfo fileInfo, int rowCount, int savedRow)
        {
            List<double> elevations = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

            //Console.WriteLine("*******************************");

            // loop through the worksheet rows and columns
            for (int i = savedRow - rowCount; i < savedRow; i++)
            {
                double content = Convert.ToDouble(worksheet.Cells[i, ELEVATIONSCOLUMN].Value.ToString());
                elevations.Add(content);
                //Console.WriteLine(content);
                //Console.WriteLine("*******************************");
            }

            return elevations;
        }

        public void writeExcel(System.IO.FileInfo fileInfo, int savedRow, int rowCount, List<double> revealValues)
        {
            int row = savedRow - rowCount;
            int j = 0;
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
            for (int i=row; i<rowCount; i++)
            { 
                worksheet.Cells[i, 7].Value = revealValues[j];
                j++;

            }


        }
        }
    }

