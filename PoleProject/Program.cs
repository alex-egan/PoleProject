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
            int savedRow = 11;
            string fileName = @"Block A-B-45-46 TOP.xlsx";
            //string fileName = @"Values.xlsx";
            List<double> revealValues = new List<double>();
            List<bool> above49BoolValues = new List<bool>();
            List<bool> under60BoolValues = new List<bool>();

            Excel excelFile = new Excel();

            FileInfo fileInfo = new FileInfo(fileName);

            int rowCount = excelFile.readEastings(fileInfo, savedRow);
            savedRow = excelFile.returnSavedRow(rowCount, savedRow);
            List<double> northings = excelFile.readNorthings(fileInfo, rowCount, savedRow);
            List<double> elevations = excelFile.readElevations(fileInfo, rowCount, savedRow);

            Console.WriteLine("****************************************");
            
            for (int i = 0; i < northings.Count; i++)
            {
                Console.WriteLine(i);
                Console.WriteLine(northings[i]);
                Console.WriteLine(elevations[i]);
                Console.WriteLine("****************************************");
            }

            //Defines an Instance of the Functions class to access Functions
            Functions functs = new Functions(northings, elevations);


            //Runs Calculations for the Regression Values and Tests Them
            revealValues = functs.calculateRevealValues(northings, elevations);

            above49BoolValues = functs.checkAbove49Inches(revealValues);

            under60BoolValues = functs.checkUnder60Inches(revealValues);

            //Prints out all the Regression Values
            for (int i = 0; i < northings.Count; i++)
            {
                Console.WriteLine("*******************************************************");
                Console.WriteLine("The " + Convert.ToString(i) + " Value is: " + Convert.ToString(revealValues[i]));
                Console.WriteLine("The Above 49 in value is: " + Convert.ToString(above49BoolValues[i]));
                Console.WriteLine("The Under 60 in Value is: " + Convert.ToString(under60BoolValues[i]));

            }
            excelFile.writeExcel(fileInfo, savedRow, rowCount, revealValues);

        }
    }
}
