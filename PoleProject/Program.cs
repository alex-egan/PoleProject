using System;
using System.IO;
using System.Collections.Generic;
using OfficeOpenXml;

namespace PoleProject
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("///////////////////////////////////");
            Console.WriteLine(DateTime.Now.ToString());
            //string fileName = @"Block A-B-45-46 TOP.xlsx";
            //string fileName = @"TestMinReveals.xlsx";
            //string fileName = @"2B TOP.xlsx";
            //string fileName = @"NewValues.xlsx";
            string fileName = @"NewValues.xlsx";
            List<double> revealValues = new List<double>();
            List<bool> above49BoolValues = new List<bool>();
            List<bool> under60BoolValues = new List<bool>();

            Excel excelFile = new Excel();

            FileInfo fileInfo = new FileInfo(fileName);
            excelFile.insertColumns(fileInfo);

            int savedRow = 1;
            bool run = true;

            while (run)
            {
                try
                {
                    int rowCount = excelFile.readEastings(fileInfo, savedRow);
                    savedRow = excelFile.returnSavedRow(rowCount, savedRow);
                    List<double> northings = excelFile.readNorthings(fileInfo, rowCount, savedRow);
                    List<double> elevations = excelFile.readElevations(fileInfo, rowCount, savedRow);

                    Console.WriteLine("****************************************");

                    //for (int i = 0; i < northings.Count; i++)
                    //{
                    //    Console.WriteLine(i);
                    //    Console.WriteLine(northings[i]);
                    //    Console.WriteLine(elevations[i]);
                    //    Console.WriteLine("****************************************");
                    //}

                    //Defines an Instance of the Functions class to access Functions
                    Functions functs = new Functions(northings, elevations);


                    //Runs Calculations for the Regression Values and Tests Them
                    revealValues = functs.calculateRevealValues(northings, elevations);

                    above49BoolValues = functs.checkAbove49Inches(revealValues);

                    under60BoolValues = functs.checkUnder60Inches(revealValues);

                    int numberOfFalses = functs.numberOfFalses(under60BoolValues);

                    //Prints out all the Regression Values
                    for (int i = 0; i < northings.Count; i++)
                    {
                        Console.WriteLine("*******************************************************");
                        Console.WriteLine("The " + Convert.ToString(i) + " Value is: " + Convert.ToString(revealValues[i]));
                        Console.WriteLine("The Above 49 in value is: " + Convert.ToString(above49BoolValues[i]));
                        Console.WriteLine("The Under 60 in Value is: " + Convert.ToString(under60BoolValues[i]));
                    }

                    Console.WriteLine(numberOfFalses);

                    excelFile.writeExcel(fileInfo, savedRow, rowCount, revealValues, above49BoolValues, under60BoolValues);

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    run = false;
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
            excelFile.addExcelLabels(fileInfo, savedRow);

            Console.WriteLine(savedRow);
        }
            
    }
}
