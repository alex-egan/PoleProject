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

        public Functions(List<double> northings, List<double> elevations)
        {
            List<double> northingValues = northings;
            List<double> elevationValues = elevations;
        }

        //Calculate Station Values and put them in a list
        public List<double> calculateStations(List<double> northings)
        {
            List<double> stations = new List<double>();
            stations.Add(0);

            for (int i = 1; i < northings.Count; i++)
            {
                stations.Add((northings[i - 1] - northings[i]) + stations[i - 1]);
                Console.WriteLine(stations[i]);
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

            Console.WriteLine("Sum of Stations: " + Convert.ToString(sumOfStations));
        }

        //Calculates Sum of Elevation Values
        public void sumAllElevations(List<double> elevations)
        {

            for (int i = 0; i < elevations.Count; i++)
            {
                sumOfElevations += elevations[i];
            }

            Console.WriteLine("Sum of Elevations: " + Convert.ToString(sumOfElevations));
        }

        //Calculates the Sum of Northing Values Times Elevation Values
        public void sumNorthingsTimesElevations(List<double> stations, List<double> elevations)
        {


            for (int i = 0; i < stations.Count; i++)
            {
                sumOfStationsTimesElevations += stations[i] * elevations[i];
            }

            Console.WriteLine("Sum of Northings Times Elevations: " + Convert.ToString(sumOfStationsTimesElevations));
        }

        //Calculates (Sum of Northing Values)^2
        public void sumNorthingsSquared(double sumOfStations)
        {

            sumOfStationsSquared = sumOfStations * sumOfStations;

            Console.WriteLine("Sum of Northings Squared: " + Convert.ToString(sumOfStationsSquared));
        }

        //Calculates Sum of (Northing Values^2)
        public void sumSquaredNorthings(List<double> stations)
        {

            for (int i = 0; i < stations.Count; i++)
            {
                sumOfSquaredStations += (stations[i] * stations[i]);
            }

            Console.WriteLine("Sum of Squared Northings: " + Convert.ToString(sumOfSquaredStations));
        }

        //Runs all Calculations needed to find Regression Line Values
        //Then Calculates the Slope and Y-Int of the Regression Line
        public void calculateRegressionValues(List<double> northings, List<double> elevations)
        {
            double slope;
            double yInt;
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

            Console.WriteLine(slope);
            Console.WriteLine(yInt);
        }

        //How to keep max value:


        //double maxResidual = -100000000;
        //List<double> residuals = new List<double>;

            //for (int i = 0; i<stations.Count; i++)
            //{
                //residuals[i] = predictedY[i] - elevations[i];

                //if residuals[i] > maxResidual

                    //maxResidual = residuals[i];

            //}
    }
}
