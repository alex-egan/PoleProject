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
        List<double> stations = new List<double>();
        List<double> predictedYValues = new List<double>();
        List<double> minRevealValues = new List<double>();
        List<double> actualRevealValues = new List<double>();


        public Functions(List<double> northings, List<double> elevations)
        {
            List<double> northingValues = northings;
            List<double> elevationValues = elevations;
        }

        //Calculate Station Values and put them in a list
        public List<double> calculateStations(List<double> northings)
        {
            stations.Add(0);

            for (int i = 1; i < northings.Count; i++)
            {
                stations.Add((northings[i - 1] - northings[i]) + stations[i - 1]);
                //Console.WriteLine(stations[i]);
            }

            return stations;
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
        public void sumAllElevations(List<double> elevations)
        {

            for (int i = 0; i < elevations.Count; i++)
            {
                sumOfElevations += elevations[i];
            }

            //Console.WriteLine("Sum of Elevations: " + Convert.ToString(sumOfElevations));
        }

        //Calculates the Sum of Northing Values Times Elevation Values
        public void sumNorthingsTimesElevations(List<double> stations, List<double> elevations)
        {


            for (int i = 0; i < stations.Count; i++)
            {
                sumOfStationsTimesElevations += stations[i] * elevations[i];
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
        public void sumSquaredNorthings(List<double> stations)
        {

            for (int i = 0; i < stations.Count; i++)
            {
                sumOfSquaredStations += (stations[i] * stations[i]);
            }

            //Console.WriteLine("Sum of Squared Northings: " + Convert.ToString(sumOfSquaredStations));
        }

        //Runs all Calculations needed to find Regression Line Values
        //Then Calculates the Slope and Y-Int of the Regression Line
        public void calculateRegressionValues(List<double> northings, List<double> elevations)
        {

            int rowCount = northings.Count;
            Console.WriteLine("Row Count: " + Convert.ToString(rowCount));


            List<double> stations = calculateStations(northings);

            sumAllStations(stations);
            sumAllElevations(elevations);
            sumNorthingsTimesElevations(stations, elevations);
            sumNorthingsSquared(sumOfStations);
            sumSquaredNorthings(stations);


            // Calculate 'a' value
            slope = ((rowCount * sumOfStationsTimesElevations) - (sumOfStations * sumOfElevations)) / ((rowCount * sumOfSquaredStations) - sumOfStationsSquared);
            yInt = (sumOfElevations - (slope * sumOfStations)) / (rowCount);

            //Console.WriteLine(slope);
            //Console.WriteLine(yInt);

            //Runs the calculations for the Predicted Y Values
            calculatePredictedY(stations, slope, yInt, elevations);

            double maxResidual = calculateResiduals(predictedYValues, elevations);
            //Console.WriteLine("maxResiduals: " + Convert.ToString(maxResidual));

            calculateMinRevealValues(maxResidual, yInt, slope, stations);

            checkMinRevealValues(minRevealValues, elevations);

            testMinReveal(actualRevealValues);
        }

        //calculate the predicted y values
        public void calculatePredictedY(List<double> stations, double slope, double yInt, List<double> elevations)
        {

            for (int i = 0; i < stations.Count; i++)
            {
                predictedYValues.Add(stations[i] * slope + yInt);
                //Console.WriteLine("predictedYValue: " + Convert.ToString(predictedYValues[i]));
            }
        }

        //How to keep max value:
        public double calculateResiduals(List<double> predictedYValues, List<double> elevations)
        {
            double maxResidual = -100000000;
            List<double> residuals = new List<double>();

            for (int i = 0; i < stations.Count; i++)
            {
                residuals.Add(elevations[i] - predictedYValues[i]);
                //Console.WriteLine("residual: " + Convert.ToString(residuals[i]));

                if (residuals[i] > maxResidual)
                {
                    maxResidual = residuals[i];
                }

            }

            return maxResidual;
        }

        public void calculateMinRevealValues(double maxResidual, double yInt, double slope, List<double> stations)
        {
            for (int i = 0; i < stations.Count; i++)
            {
                minRevealValues.Add(stations[i] * slope + yInt + maxResidual + MINREVEAL);
                //Console.WriteLine("minRevealValues: " + Convert.ToString(minRevealValues[i]));
            }
        }

        public void checkMinRevealValues(List<double> minRevealValues, List<double> elevations)
        {
            for(int i=0; i<elevations.Count; i++)
            {
                actualRevealValues.Add(minRevealValues[i] - elevations[i]);
                //Console.WriteLine("actualReveal: " + Convert.ToString(actualRevealValues[i]));
            }
        }

        public void testMinReveal(List<double> actualRevealValues)
        {
            List<double> testMinRevealValues = new List<double>();
            List<double> officialMinRevealValues = new List<double>();
            bool test = true;
            for (int i = 0; i < actualRevealValues.Count; i++)
            {
                testMinRevealValues.Add(actualRevealValues[i]);
                officialMinRevealValues.Add(0);
            }

            while (test == true)
            {
                for (int i = 0; i < actualRevealValues.Count; i++)
                {
                    if (testMinRevealValues[i] > MAXREVEAL)
                    {
                        test = false;
                        Console.WriteLine(Convert.ToString(testMinRevealValues[i]));
                        break;
                    }
                }
                if (test == false)
                {
                    break;
                }
                //Console.WriteLine(TESTCONSTANT);
                for (int j = 0; j < actualRevealValues.Count; j++)
                {
                    officialMinRevealValues[j] = testMinRevealValues[j];
                    testMinRevealValues[j] += TESTCONSTANT;

                }
            }
            for (int i = 0; i < actualRevealValues.Count; i++)
            {
                Console.WriteLine("officialValue: " + Convert.ToString(officialMinRevealValues[i]));
            }
        }
            
            //return officialMinRevealValues;
        
        
    }
}
