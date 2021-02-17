﻿using System;
using ExcelDataReader;
using System.Collections.Generic;
using IronXL;
using System.IO;
using System.Data;

namespace PoleProject
{
    public class ExcelPractice
    {
        public ExcelPractice()
        {
        }

        public void readExcelFilePrac(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
            string result = Convert.ToString(excelReader.GetData(5));

            Console.WriteLine(result);

            //4. DataSet - Create column names from first row
            //excelReader.IsFirstRowAsColumnNames = true;
            //DataSet result = excelReader.AsDataSet();

            //5. Data Reader methods
            while (excelReader.Read())
            {
                //excelReader.GetInt32(0);
            }

            //6. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close();
        }
    }
}
