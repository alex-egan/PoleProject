//**********************************************************************************
// Project: Top of Pile Calculator
// Done For: Matt Eklund
// Created By: Trevor and Alex Egan
// Project Start Date: January 29, 2021
// Beta Test Version Released: April 27, 2021
//**********************************************************************************

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
            //Initializes lists to hold final values
            List<double> revealValues = new List<double>();
            List<bool> above49BoolValues = new List<bool>();
            List<bool> under60BoolValues = new List<bool>();

            //Initializes variables that are used for Reading/Writing to Excel
            string fileName = @"Block 11_SECTION 3_OG.xlsx";

            Excel excelFile = new Excel();
            FileInfo fileInfo = new FileInfo(fileName);

            //Adds columns to the excel file to write to
            excelFile.insertColumns(fileInfo);

            //Initializes the saved row which is used to remember which row of the
            // excel file the program is on
            int savedRow = 1;

            //Boolean variable for the while loop
            bool run = true;

            while (run)
            {
                try
                {
                    //Reads all information from the Excel File needed for the calculations
                    int rowCount = excelFile.readEastings(fileInfo, savedRow);
                    savedRow = excelFile.returnSavedRow(rowCount, savedRow);
                    List<double> northings = excelFile.readNorthings(fileInfo, rowCount, savedRow);
                    List<double> elevations = excelFile.readElevations(fileInfo, rowCount, savedRow);

                    //Defines an Instance of the Functions class to access Functions
                    Functions functs = new Functions(northings, elevations);


                    //Runs Calculations for the Regression Values, Tests Them, and Returns them to the Program
                    revealValues = functs.calculateRevealValues(northings, elevations);


                    //Returns the Boolean Values to the Program
                    above49BoolValues = functs.checkAbove49Inches(revealValues);
                    under60BoolValues = functs.checkUnder60Inches(revealValues);


                    //Returns the number of reveal values that were over 60 inches (useful for testing)
                    int numberOfFalses = functs.numberOfFalses(under60BoolValues);


                    //Prints out all the Regression Values (Commented out for convenience)
                    //for (int i = 0; i < northings.Count; i++)
                    //{
                    //    Console.WriteLine("*******************************************************");
                    //    Console.WriteLine("The " + Convert.ToString(i) + " Value is: " + Convert.ToString(revealValues[i]));
                    //    Console.WriteLine("The Above 49 in value is: " + Convert.ToString(above49BoolValues[i]));
                    //    Console.WriteLine("The Under 60 in Value is: " + Convert.ToString(under60BoolValues[i]));
                    //}


                    //Writes the calculated information to the Excel File
                    excelFile.writeExcel(fileInfo, savedRow, rowCount, revealValues, above49BoolValues, under60BoolValues);
                }

                //Catch here to conveniently give errors in the console window
                catch (Exception e)
                {
                    run = false;
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
        }    
    }
}
