using System;
using System.Collections.Generic;
using IronXL;

namespace PoleProject
{
    public class ExcelPractice
    {
        public ExcelPractice()
        {
        }

        public void readExcelFilePrac(string fileName)
        {
            double value = 0;

            WorkBook workbook = WorkBook.Load(fileName);
            WorkSheet sheet = workbook.DefaultWorkSheet;

            value = Convert.ToDouble(sheet["A2"]);

            Console.WriteLine(value);
        }
    }
}
