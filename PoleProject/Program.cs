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

            int savedRow = 0;
            string file = @"Values.xlsx";
            List<double> revealValues = new List<double>();
            List<bool> above49BoolValues = new List<bool>();
            List<bool> under60BoolValues = new List<bool>();

            FileInfo fileInfo = new FileInfo(file);

            //Excel excelFile = new Excel();

            //excelFile.readExcelFile(fileInfo, savedRow);

            //Creating Lists of Practice Values (Since Excel Import isn't working)
            List<double> northings = new List<double> { 26680370.87,
                                               26680341.56,
                                               26680312.25,
                                               26680282.94,
                                               26680253.62,
                                               26680224.31,
                                               26680196.43,
                                               26680168.55,
                                               26680139.24,
                                               26680109.93,
                                               26680080.61,
                                               26680051.30,
                                               26680021.99
            };

            List<double> elevations = new List<double> { 1862.856,
                                                1862.114,
                                                1861.092,
                                                1860.115,
                                                1859.364,
                                                1858.573,
                                                1857.713,
                                                1857.18,
                                                1856.616,
                                                1855.861,
                                                1855.226,
                                                1854.511,
                                                1853.963
            };


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

        }
    }
}
