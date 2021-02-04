using System;
using System.Collections.Generic;
using IronXL;

namespace PoleProject
{
    public class Excel
    {
        public Excel()
        {
            
        }
        
        public List<double> readExcelFile(string fileName, int savedRow)
        {
            List<double> northings = new List<double>();
            List<double> elevations = new List<double>();

            WorkBook workbook = WorkBook.Load(fileName);
            WorkSheet sheet = workbook.DefaultWorkSheet;

            // Make sure to use the savedRow integer here instead of "C2"
            double easting = Convert.ToDouble(sheet["C2"]);
            int rowCount = 3;

            while (Convert.ToDouble(sheet["C" + Convert.ToString(rowCount)]) == easting)
            {
                northings.Add(Convert.ToDouble(sheet["B" + Convert.ToString(rowCount)]));
                elevations.Add(Convert.ToDouble(sheet["D" + Convert.ToString(rowCount)]));
                rowCount += 1;
            }

            savedRow = rowCount;
            savedRow += 1;

            Console.WriteLine(northings);
            Console.WriteLine(elevations);

            return northings;

        }
    }
}
