using System;
using System.Collections.Generic;

namespace PoleProject
{
    public class Functions
    {
        double sumOfStations = 0;
        double sumOfElevations = 0;
        double sumOfStationsTimesElevations = 0;
        double sumOfStationsSquared = 0;
        double sumOfSquaredStations = 0;
        double slope = 0;
        double yInt = 0;
        double MINREVEAL = (49.00 / 12);
        double MAXREVEAL = 5.00;
        double TESTCONSTANT = 0.1;
        List<double> stationValues = new List<double>();
        List<double> predictedYValues = new List<double>();
        List<double> minRevealValues = new List<double>();


        public Functions(List<double> northings, List<double> elevations)
        {
            List<double> northingValues = northings;
            List<double> elevationValues = elevations;
        }


        //Calculate Station Values and put them in a list
        public List<double> calculateStations(List<double> northingValues)
        {
            stationValues.Add(0);

            for (int i = 1; i < northingValues.Count; i++)
            {
                stationValues.Add((northingValues[i - 1] - northingValues[i]) + stationValues[i - 1]);
                //Console.WriteLine(stations[i]);
            }

            return stationValues;
        }


        //Calculates Sum of Northing Values
        public void sumAllStations(List<double> stations)
        {
            for (int i = 0; i < stations.Count; i++)
            {
                sumOfStations += stations[i];
            }

            //Console.WriteLine("Sum of Stations: " + Convert.ToString(sumOfStations));
        }


        //Calculates Sum of Elevation Values
        public void sumAllElevations(List<double> elevationValues)
        {

            for (int i = 0; i < elevationValues.Count; i++)
            {
                sumOfElevations += elevationValues[i];
            }

            //Console.WriteLine("Sum of Elevations: " + Convert.ToString(sumOfElevations));
        }


        //Calculates the Sum of Northing Values Times Elevation Values
        public void sumNorthingsTimesElevations(List<double> stationValues, List<double> elevationValues)
        {


            for (int i = 0; i < stationValues.Count; i++)
            {
                sumOfStationsTimesElevations += stationValues[i] * elevationValues[i];
            }

            //Console.WriteLine("Sum of Northings Times Elevations: " + Convert.ToString(sumOfStationsTimesElevations));
        }


        //Calculates (Sum of Northing Values)^2
        public void sumNorthingsSquared(double sumOfStations)
        {

            sumOfStationsSquared = sumOfStations * sumOfStations;

            //Console.WriteLine("Sum of Northings Squared: " + Convert.ToString(sumOfStationsSquared));
        }


        //Calculates Sum of (Northing Values^2)
        public void sumSquaredNorthings(List<double> stationValues)
        {

            for (int i = 0; i < stationValues.Count; i++)
            {
                sumOfSquaredStations += (stationValues[i] * stationValues[i]);
            }

            //Console.WriteLine("Sum of Squared Northings: " + Convert.ToString(sumOfSquaredStations));
        }


        //Runs all Calculations needed to find Regression Line Values
        //Then Calculates the Slope and Y-Int of the Regression Line
        public void calculateRegressionValues(List<double> northingValues, List<double> elevationValues)
        {

            int rowCount = northingValues.Count;
            //Console.WriteLine("Row Count: " + Convert.ToString(rowCount));

            List<double> stationValues = calculateStations(northingValues);

            sumAllStations(stationValues);
            sumAllElevations(elevationValues);
            sumNorthingsTimesElevations(stationValues, elevationValues);
            sumNorthingsSquared(sumOfStations);
            sumSquaredNorthings(stationValues);


            // Calculate slope and yInt
            slope = ((rowCount * sumOfStationsTimesElevations) - (sumOfStations * sumOfElevations)) / ((rowCount * sumOfSquaredStations) - sumOfStationsSquared);
            yInt = (sumOfElevations - (slope * sumOfStations)) / (rowCount);
        }


        //calculate the predicted y values
        public void calculatePredictedYValues(List<double> elevationValues, List<double> stationValues, double slope, double yInt)
        {

            for (int i = 0; i < stationValues.Count; i++)
            {
                predictedYValues.Add(stationValues[i] * slope + yInt);
                //Console.WriteLine("predictedYValue: " + Convert.ToString(predictedYValues[i]));
            }
        }


        //Calculates All Residuals and Holds the Max Value
        public double calculateResiduals(List<double> predictedYValues, List<double> elevationValues)
        {
            double maxResidual = -100000000;
            List<double> residuals = new List<double>();

            for (int i = 0; i < elevationValues.Count; i++)
            {
                residuals.Add(elevationValues[i] - predictedYValues[i]);
                //Console.WriteLine("residual: " + Convert.ToString(residuals[i]));

                if (residuals[i] > maxResidual)
                {
                    maxResidual = residuals[i];
                }

            }

            //Console.WriteLine("maxResidual: " + Convert.ToString(maxResidual));

            return maxResidual;
        }


        //Calculates the Minimum Reveal Values by finding the High Points
        // Then adding the MINREVEAL Constant (49 inches) in feet.
        // Elevation is subtracted to find the actual Reveal Values.
        public void calculateMinRevealValues(List<double> elevationValues, List<double> stationValues, double maxResidual, double yInt, double slope)
        {
            for (int i = 0; i < stationValues.Count; i++)
            {
                minRevealValues.Add(((stationValues[i] * slope) + yInt + maxResidual + MINREVEAL) - elevationValues[i]);
                //Console.WriteLine("minRevealValues: " + Convert.ToString(minRevealValues[i]));
            }
        }


        //Tests the Minimum Reveal Values to ensure they are all as high as possible
        // While still being under the 60 Inch Maximum Pile Height Requirement
        public List<double> testMinRevealValues(List<double> minRevealValues)
        {
            List<double> testMinRevealValues = new List<double>();
            List<double> officialMinRevealValues = new List<double>();
            bool test = true;
            for (int i = 0; i < minRevealValues.Count; i++)
            {
                testMinRevealValues.Add(minRevealValues[i]);
                officialMinRevealValues.Add(0);
            }

            while (test == true)
            {
                for (int i = 0; i < minRevealValues.Count; i++)
                {
                    if (testMinRevealValues[i] > MAXREVEAL)
                    {
                        test = false;
                        //Console.WriteLine(Convert.ToString(testMinRevealValues[i]));
                        break;
                    }
                }
                if (test == false)
                {
                    break;
                }
                //Console.WriteLine(TESTCONSTANT);
                for (int j = 0; j < minRevealValues.Count; j++)
                {
                    officialMinRevealValues[j] = testMinRevealValues[j];
                    testMinRevealValues[j] += TESTCONSTANT;
                }
            }

            return officialMinRevealValues;
        }
            
            //return officialMinRevealValues;

        //Runs all functions and Returns the Official Reveal Values to Be Used
        public List<double> calculateAndTestRevealValues(List<double> northings, List<double> elevations)
        {
            List<double> returnRevealValues = new List<double>();

            calculateRegressionValues(northings, elevations);

            calculatePredictedYValues(elevations, stationValues, slope, yInt);

            double maxResidual = calculateResiduals(predictedYValues, elevations);

            calculateMinRevealValues(elevations, stationValues, maxResidual, yInt, slope);

            returnRevealValues = testMinRevealValues(minRevealValues);

            return returnRevealValues;
        }


    }
}
