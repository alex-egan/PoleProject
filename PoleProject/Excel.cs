using System;
using IronXL;

namespace PoleProject
{
    public class Excel
    {
        public Excel()
        {
        }

        public void readExcelFile(string fileName)
        {
            WorkBook workbook = WorkBook.Load(fileName);
            WorkSheet sheet = workbook.DefaultWorkSheet;
            Range range = sheet["A2:A8"];
            decimal total = 0;
            //iterate over range of cells
            foreach (var cell in range)
            {
                Console.WriteLine("Cell {0} has value '{1}", cell.RowIndex, cell.Value);
                if (cell.IsNumeric)
                {
                    //Get decimal value to avoid floating numbers precision issue
                    total += cell.DecimalValue;
                }
            }
            //check formula evaluation
            if (sheet["A11"].DecimalValue == total)
            {
                Console.WriteLine("Basic Test Passed");
            }
        }
        
    }
}
