using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Recon.File;
using Recon.Interface;
using Recon.Shared.Common;

namespace Recon.Excel
{
    public class Writer
    {
        private IList<IReconResult> _results;
        private string _resultFile;

        public Writer(IList<IReconResult> results)
        {
            _results = results;
        }

        public bool SaveData(string resultFullName)
        {
            _resultFile = resultFullName;
            if (!string.IsNullOrEmpty(resultFullName))
            {
                FileInfo saveFi = FileHelper.GetFileInfo(resultFullName);
                if (saveFi != null)
                {
                    System.IO.File.Delete(resultFullName);
                }
                startExport();

            }
            return true;
        }

        private void startExport()
        {
            /*Set up work book, work sheets, and excel application*/
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Add(misValue);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = new Microsoft.Office.Interop.Excel.Worksheet();

                excelWorkbook.Sheets.Add();
                
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[excelWorkbook.Sheets.Count];

                excelSheet.Name = "Result " + excelWorkbook.Sheets.Count;

                exportToExcel(excelSheet);

                //Release and terminate excel

                excelWorkbook.SaveAs(_resultFile);
                excelWorkbook.Close();
                excelApp.Quit();
                excelSheet = null;
                excelWorkbook = null;
                excelApp = null;
                GC.Collect();
            }

            catch (Exception ex)
            {
                excelApp.Quit();
                ExceptionWriter.Display(ex);
            }
        }

        private void exportToExcel(Microsoft.Office.Interop.Excel.Worksheet excelSheet)
        {
            int colIndex = 0;
            int rowIndex = 1;

            // Write Headers
            foreach (var header in ColumnHeaders)
            {
                excelSheet.Cells[1, header.Key] = header.Value;
            }
            // Write Data
            colIndex = 0;
            foreach (var result in _results)
            {
                rowIndex++;
                excelSheet.Cells[rowIndex, 1] = result.ReferenceFrom;
                excelSheet.Cells[rowIndex, 2] = result.AmountFrom;
                excelSheet.Cells[rowIndex, 3] = result.ReferenceTo;
                excelSheet.Cells[rowIndex, 4] = result.AmountTo;
                excelSheet.Cells[rowIndex, 5] = result.Match;
                excelSheet.Cells[rowIndex, 6] = result.Difference;
            }

            excelSheet.Columns.AutoFit();

        }

        private Dictionary<int, string> _columnHeaders;
        private Dictionary<int, string> ColumnHeaders
        {
            get
            {
                if (_columnHeaders == null)
                {
                    _columnHeaders = new Dictionary<int, string>();
                    _columnHeaders.Add(1, "ReferenceFrom");
                    _columnHeaders.Add(2, "AmountFrom");
                    _columnHeaders.Add(3, "ReferenceTo");
                    _columnHeaders.Add(4, "AmountTo");
                    _columnHeaders.Add(5, "Match");
                    _columnHeaders.Add(6, "Difference");
                }
                return _columnHeaders;
            }
        }

        private int GetColumnPosition(string name)
        {
            foreach (var hearer in ColumnHeaders)
            {
                if (hearer.Value == name)
                {
                    return hearer.Key;
                }
            }
            return -1;
        }

        private string GetPropertyDescription<T>(string propertyName)
        {
            var item = TypeDescriptor.GetProperties(typeof(T))[propertyName];
            foreach (Attribute attrib in item.Attributes)
            {
                return item.Description;
            }
            return String.Empty;
        }
    }
}