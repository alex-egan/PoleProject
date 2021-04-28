using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace PoleProject
{
    public class Excel
    {
        //Sets important variables.
        //Only change them if they don't line up with the details of the file.
        int EASTINGSCOLUMN = 3;
        int NORTHINGSCOLUMN = 2;
        int ELEVATIONSCOLUMN = 4;
        string sheetName = "Block 11_SECTION 3_OG";

        //Inserts columns to which the program writes final values
        public void insertColumns(FileInfo fileInfo)
        {
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

            worksheet.InsertColumn(8, 3);
        }

        //Calculates the amount of rows that have the same easting value
        // Used to read in the correct number of northings and elevations
        public int readEastings(System.IO.FileInfo fileInfo, int savedRow)
        {
            List<double> eastings = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

            //Gets the value of the easting for the next iteration (the original easting)
            double eastingValue = Convert.ToDouble(worksheet.Cells[savedRow, EASTINGSCOLUMN].Value.ToString());

            bool iterate = true;
            int rowCount = 0;

            //This while loop is responsible for looking at the next easting value in the excel file and makes sure
            // that it is the same as the original one for this iteration. The while loop breaks when it finds an
            // easting that is not the same as the original one
            while (iterate == true)
            {
                double nextEastingValue;
                try
                {
                    nextEastingValue = Convert.ToDouble(worksheet.Cells[savedRow, EASTINGSCOLUMN].Value.ToString());
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

            return rowCount;
        }

        //Ensures that the correct value for savedRow is returned to the main program
        public int returnSavedRow(int rowCount, int savedRow)
        {
            savedRow += rowCount;

            return savedRow;
        }


        //Reads the Northing Values from the Excel File into the Program
        public List<double> readNorthings(System.IO.FileInfo fileInfo, int rowCount, int savedRow)
        {
            List<double> northings = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

            //Loops through the rows from saved row to row count + saved row to extract the northings
            for (int i = savedRow - rowCount; i < savedRow; i++)
            {
                double northing = Convert.ToDouble(worksheet.Cells[i, NORTHINGSCOLUMN].Value.ToString());
                northings.Add(northing);
            }

            return northings;
        }

        //Reads the elevation values from the Excel File to the program
        public List<double> readElevations(System.IO.FileInfo fileInfo, int rowCount, int savedRow)
        {
            List<double> elevations = new List<double>();

            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

            //Loops through the rows from saved row to row count + saved row to extract the elevations
            for (int i = savedRow - rowCount; i < savedRow; i++)
            {
                double elevation = Convert.ToDouble(worksheet.Cells[i, ELEVATIONSCOLUMN].Value.ToString());
                elevations.Add(elevation);
            }

            return elevations;
        }

        //Writes the final values to the Excel File
        public void writeExcel(System.IO.FileInfo fileInfo, int savedRow, int rowCount, List<double> revealValues, List<bool> above49BoolValues, List<bool> under60BoolValues)
        {
            int row = savedRow - rowCount;
            int j = 0;
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
            for (int i=row; i<savedRow; i++)
            { 
                worksheet.Cells[i, 8].Value = revealValues[j];
                worksheet.Cells[i, 9].Value = above49BoolValues[j];
                worksheet.Cells[i, 10].Value = under60BoolValues[j];
                j++;
            }

            package.Save();
        }
    }
}

