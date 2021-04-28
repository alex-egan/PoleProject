using System;
using System.Collections.Generic;

namespace PoleProject
{
    public class Functions
    {
        //Defines many important variables for the methods in this class
        double sumOfStations = 0;
        double sumOfElevations = 0;
        double sumOfStationsTimesElevations = 0;
        double sumOfStationsSquared = 0;
        double sumOfSquaredStations = 0;
        double slope = 0;
        double yInt = 0;
        double MINREVEAL = (49.00 / 12);
        double MAXREVEAL = 5.00;
        double TESTCONSTANT = 0.05;
        List<double> stationValues = new List<double>();
        List<double> predictedYValues = new List<double>();
        List<double> minRevealValues = new List<double>();
        List<bool> checkGreaterThan49Inches = new List<bool>();
        List<bool> checkLessThan60Inches = new List<bool>();

        public Functions(List<double> northings, List<double> elevations)
        {
            List<double> northingValues = northings;
            List<double> elevationValues = elevations;
        }

        //Runs all functions and Returns the Official Reveal Values to Be Used
        public List<double> calculateRevealValues(List<double> northings, List<double> elevations)
        {

            calculateRegressionValues(northings, elevations);

            calculatePredictedYValues(elevations, stationValues, slope, yInt);

            double maxResidual = calculateResiduals(predictedYValues, elevations);

            List<double> minRevealValues = calculateMinRevealValues(elevations, stationValues, maxResidual, yInt, slope);

            List<double> checkedRevealValues = testMinRevealValues(minRevealValues);

            return checkedRevealValues;
        }

        //Runs all Calculations needed to find Regression Line Values
        // Then Calculates the Slope and Y-Int of the Regression Line
        public void calculateRegressionValues(List<double> northingValues, List<double> elevationValues)
        {

            int rowCount = northingValues.Count;

            stationValues = calculateStations(northingValues);

            sumAllStations(stationValues);
            sumAllElevations(elevationValues);
            sumNorthingsTimesElevations(stationValues, elevationValues);
            sumNorthingsSquared(sumOfStations);
            sumSquaredNorthings(stationValues);


            // Calculate slope and yInt
            slope = ((rowCount * sumOfStationsTimesElevations) - (sumOfStations * sumOfElevations)) / ((rowCount * sumOfSquaredStations) - sumOfStationsSquared);
            yInt = (sumOfElevations - (slope * sumOfStations)) / (rowCount);
        }

        //Calculate Station Values and put them in a list
        public List<double> calculateStations(List<double> northingValues)
        {
            stationValues.Add(0);

            for (int i = 1; i < northingValues.Count; i++)
            {
                stationValues.Add((northingValues[i - 1] - northingValues[i]) + stationValues[i - 1]);
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
        }


        //Calculates Sum of Elevation Values
        public void sumAllElevations(List<double> elevationValues)
        {
            for (int i = 0; i < elevationValues.Count; i++)
            {
                sumOfElevations += elevationValues[i];
            }
        }

        //Calculates the Sum of Northing Values Times Elevation Values
        public void sumNorthingsTimesElevations(List<double> stationValues, List<double> elevationValues)
        {
            for (int i = 0; i < stationValues.Count; i++)
            {
                sumOfStationsTimesElevations += stationValues[i] * elevationValues[i];
            }
        }

        //Calculates (Sum of Northing Values)^2
        public void sumNorthingsSquared(double sumOfStations)
        {
            sumOfStationsSquared = sumOfStations * sumOfStations;
        }

        //Calculates Sum of (Northing Values^2)
        public void sumSquaredNorthings(List<double> stationValues)
        {
            for (int i = 0; i < stationValues.Count; i++)
            {
                sumOfSquaredStations += (stationValues[i] * stationValues[i]);
            }
        }



        //calculate the predictedY values
        public void calculatePredictedYValues(List<double> elevationValues, List<double> stationValues, double slope, double yInt)
        {

            for (int i = 0; i < stationValues.Count; i++)
            {
                predictedYValues.Add(stationValues[i] * slope + yInt);
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

                if (residuals[i] > maxResidual)
                {
                    maxResidual = residuals[i];
                }

            }
            return maxResidual;
        }



        //Calculates the Minimum Reveal Values by finding the High Points
        // Then adding the MINREVEAL Constant (49 inches) in feet.
        // Elevation is subtracted to find the actual Reveal Values.
        public List<double> calculateMinRevealValues(List<double> elevationValues, List<double> stationValues, double maxResidual, double yInt, double slope)
        {
            for (int i = 0; i < stationValues.Count; i++)
            {
                minRevealValues.Add(((stationValues[i] * slope) + yInt + maxResidual + MINREVEAL) - elevationValues[i]);
            }

            return minRevealValues;
        }

        //This method tests the MinRevealValues (Deeper Explanation throughout the method)
        public List<double> testMinRevealValues(List<double> minRevealValues)
        {
            List<double> testMinRevealValues = new List<double>();
            List<double> officialMinRevealValues = new List<double>();

            bool test = true;

            //First, all the lists are created to length and initiliazed with values.
            for (int i = 0; i < minRevealValues.Count; i++)
            {
                testMinRevealValues.Add(minRevealValues[i]);
                officialMinRevealValues.Add(0);
                checkLessThan60Inches.Add(true);
                checkGreaterThan49Inches.Add(true);
            }

            //The first while loop checks to see if all the values are greater than 49 inches.
            // If there are any values that are over 49 inches, the under49FalseCount is incremented by 1
            // This causes all of the MinRevealValues to be incremented by the TestConstant
            // If all the values are greater than 49 inchers, then it carries on to the next loop
            while (test == true)
            {
                int under49FalseCount = 0;

                for (int i = 0; i < testMinRevealValues.Count; i++)
                {
                    if (testMinRevealValues[i] < MINREVEAL)
                    {
                        checkGreaterThan49Inches[i] = false;
                        under49FalseCount++;
                    }
                    else
                    {
                        checkGreaterThan49Inches[i] = true;
                    }
                }

                if (under49FalseCount != 0)
                {
                    for (int i = 0; i < testMinRevealValues.Count; i++)
                    {
                        testMinRevealValues[i] += TESTCONSTANT;
                    }
                }
                else
                {
                    break;
                }
            }

            int over60FalseCount = 0;

            //This loop is a preliminary check to see if any of the values are greater than 60 inches
            // If there are, then over60FalseCount will be nonzero
            for (int i = 0; i < testMinRevealValues.Count; i++)
            {
                if (testMinRevealValues[i] > MAXREVEAL)
                {
                    over60FalseCount++;
                }
            }

            test = true;

            //The final loop checks the over60FalseCount to see if any of the values are over 60 inches.
            // If there are any, then the testMinRevealValues are assigned to officialMinRevealValues and the method ends
            // Otherwise, the values are incremented until one of the values is over 60 inches, and the previous iteration is saved and used.
            if (over60FalseCount != 0)
            {
                for (int i = 0; i < testMinRevealValues.Count; i++)
                {
                    officialMinRevealValues[i] = testMinRevealValues[i];

                    if (officialMinRevealValues[i] > MAXREVEAL)
                    {
                        checkLessThan60Inches[i] = false;
                    }
                }
            }
            else
            {
                while (test)
                {
                    over60FalseCount = 0;

                    for (int i = 0; i < testMinRevealValues.Count; i++)
                    {
                        if (testMinRevealValues[i] > MAXREVEAL)
                        {
                            checkLessThan60Inches[i] = false;
                            over60FalseCount++; ;
                        }

                        else
                        {
                            checkLessThan60Inches[i] = true;
                        }
                    }

                    if (over60FalseCount != 0)
                    {
                        for (int i = 0; i < testMinRevealValues.Count; i++)
                        {
                            if (officialMinRevealValues[i] < MAXREVEAL)
                            {
                                checkLessThan60Inches[i] = true;
                            }
                        }
                        break;
                    }

                    else
                    {
                        for (int i = 0; i < testMinRevealValues.Count; i++)
                        {
                            officialMinRevealValues[i] = testMinRevealValues[i];
                            testMinRevealValues[i] += TESTCONSTANT;
                        }
                    }
                }
            }

            return officialMinRevealValues;
        }

        //Checks to see how many false values are in the list.
        public int numberOfFalses(List<bool> checkLessThan60Inches)
        {
            int falseCount = 0;

            for (int i = 0; i < checkLessThan60Inches.Count; i++)
            {
                if (checkLessThan60Inches[i] == false)
                {
                    falseCount++;
                }

                else
                {
                    continue;
                }
            }

            return falseCount;
        }

        //Returns the checkLessThan60Inches list to the main program
        public List<bool> checkUnder60Inches(List<double> officialMinRevealValues)
        {
            return checkLessThan60Inches;
        }

        //Returns the checkAbove49Inches list to the main program
        public List<bool> checkAbove49Inches(List<double> officialMinRevealValues)
        {
            return checkGreaterThan49Inches;
        }
    }
}
