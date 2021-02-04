using System;
using System.Collections.Generic;

namespace PoleProject
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Excel excelFile = new Excel();
            //string fileName = @"Values.xlsx";

            //Testing List Output
            List<double> trial = new List<double> { 123.4 };
            Console.WriteLine(trial.Count);


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


            //Runs the calculations for the Regression Values
            functs.calculateRegressionValues(northings, elevations);
        }
    }
}
