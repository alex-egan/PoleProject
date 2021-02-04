using System;
using System.Collections.Generic;


namespace PoleProject
{
    public class Functions
    {
        double sumOfNorthings = 0;
        double sumOfElevations = 0;
        double sumOfNorthingsTimesElevations = 0;
        double sumOfNorthingsSquared = 0;
        double sumOfSquaredNorthings = 0;

        public Functions(List<double> northings, List<double> elevations)
        {
            List<double> northingValues = northings;
            List<double> elevationValues = elevations;
        }

        public void randomStatement()
        {
            Console.WriteLine("Yo.");
        }

        //Calculates Sum of Northing Values
        public void sumAllNorthings(List<double> northings)
        {
            Console.WriteLine("Inside of sumAllNorthings: " + Convert.ToString(northings[0]));

            for (int i = 0; i < northings.Count; i++)
            {
                sumOfNorthings += northings[i];
                Console.WriteLine(Convert.ToString(i) + ": " + Convert.ToString(sumOfNorthings));
            }

            Console.WriteLine("Sum of Northings: " + Convert.ToString(sumOfNorthings));
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
        public void sumNorthingsTimesElevations(List<double> northings, List<double> elevations)
        {
            

            for (int i = 0; i < northings.Count; i++)
            {
                sumOfNorthingsTimesElevations += northings[i] * elevations[i];
            }

            Console.WriteLine("Sum of Northings Times Elevations: " + Convert.ToString(sumOfNorthingsTimesElevations));
        }

        //Calculates (Sum of Northing Values)^2
        public void sumNorthingsSquared(double sumOfNorthings)
        {

            sumOfNorthingsSquared = sumOfNorthings * sumOfNorthings;

            Console.WriteLine("Sum of Northings Squared: " + Convert.ToString(sumOfNorthingsSquared));
        }

        //Calculates Sum of (Northing Values^2)
        public void sumSquaredNorthings(List<double> northings)
        {

            for (int i = 0; i < northings.Count; i++)
            {
                sumOfSquaredNorthings += (northings[i] * northings[i]);
            }

            Console.WriteLine("Sum of Squared Northings: " + Convert.ToString(sumOfSquaredNorthings));
        }

        //Runs all Calculations needed to find Regression Line Values
        //Then Calculates the Slope and Y-Int of the Regression Line
        public void calculateRegressionValues(List<double> northings, List<double> elevations)
        {
            double slope;
            double yInt;
            int rowCount = northings.Count;
            Console.WriteLine("Row Count: " + Convert.ToString(rowCount));

            sumAllNorthings(northings);
            sumAllElevations(elevations);
            sumNorthingsTimesElevations(northings, elevations);
            sumNorthingsSquared(sumOfNorthings);
            sumSquaredNorthings(northings);


            // Calculate 'a' value
            slope = ((rowCount * sumOfNorthingsTimesElevations) - (sumOfNorthings * sumOfElevations)) / ((rowCount * sumOfSquaredNorthings) - sumOfNorthingsSquared);
            yInt = (sumOfElevations - (slope * sumOfNorthings)) / (rowCount);

            Console.WriteLine(slope);
            Console.WriteLine(yInt);

            //Giving Wrong Values.. What is wrong?
        }
    }
}
