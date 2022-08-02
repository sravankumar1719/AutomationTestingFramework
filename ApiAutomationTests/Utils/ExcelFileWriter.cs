namespace ApiAutomationTests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using OfficeOpenXml;

    public class ExcelFileWriter
    {
        private static ExcelFileWriter instance;
        private static int rowCounter;

        public static ExcelFileWriter GetInstance()
        {
            if (instance == null)
            {
                instance = new ExcelFileWriter();
            }

            return instance;
        }

        private ExcelFileWriter()
        {
            //var file = new FileInfo(@"C:\Users\C261008\Desktop\Learning\ApiAutomationTests\SuiteDetails.xlsx");

            //if (file.Exists)
            //{
            //    file.Delete();
            //}

            //Package = new ExcelPackage(file);

            //Worksheet = Package.Workbook.Worksheets.Add("SuiteLaunches");
            //Worksheet.Cells["A1"].LoadFromText("Launch Id");
            //Worksheet.Cells["B1"].LoadFromText("Suite Name");
            //Worksheet.Cells["C1"].LoadFromText("Environment");
            //Worksheet.Cells["D1"].LoadFromText("Total");
            //Worksheet.Cells["E1"].LoadFromText("Passed");
            //Worksheet.Cells["F1"].LoadFromText("Failed");
            //Worksheet.Cells["G1"].LoadFromText("Pass Percentage");
            //Worksheet.Cells["H1"].LoadFromText("Tag Updated");
            //rowCounter = 2;
        }

        private ExcelWorksheet Worksheet { get; set; }

        private ExcelPackage Package { get; set; }

        public void SaveExcelFile(List<ReportPortalModel> suiteLaunchesData)
        {
            int startingIndex = 0;
            Console.WriteLine("Starting index:" + rowCounter);
            //for (int rowIndex = 0; rowIndex < suiteLaunchesData.Count; rowIndex++)
            //{
            //    Worksheet.Cells[rowIndex + rowCounter, 1].Value = suiteLaunchesData[rowIndex].Id;
            //    Worksheet.Cells[rowIndex + rowCounter, 2].Value = suiteLaunchesData[rowIndex].Name;
            //    Worksheet.Cells[rowIndex + rowCounter, 3].Value = suiteLaunchesData[rowIndex].Attributes
            //        .Find(key => key.ContainsValue("Env")).Values.Last();
            //    Worksheet.Cells[rowIndex + rowCounter, 4].Value = suiteLaunchesData[rowIndex].Statistics.ExecutionStatus.Total;
            //    Worksheet.Cells[rowIndex + rowCounter, 5].Value = suiteLaunchesData[rowIndex].Statistics.ExecutionStatus.Passed;
            //    Worksheet.Cells[rowIndex + rowCounter, 6].Value = suiteLaunchesData[rowIndex].Statistics.ExecutionStatus.Failed;
            //    Worksheet.Cells[rowIndex + rowCounter, 7].Value = Math.Round(Convert.ToDouble(suiteLaunchesData[rowIndex].Statistics.ExecutionStatus.Passed) /
            //                                                                 Convert.ToDouble(suiteLaunchesData[rowIndex].Statistics.ExecutionStatus.Total) * 100);
            //    Worksheet.Cells[rowIndex + rowCounter, 8].Value = suiteLaunchesData[rowIndex].Attributes
            //        .Find(attribute => attribute.ContainsValue("Result"))["value"];

            //    Worksheet.Columns.AutoFit();
            //    startingIndex++;
            //}

            //rowCounter += startingIndex;
            //Console.WriteLine("Ending index:" + rowCounter);
            //Package.Save();
        }
    }
}
