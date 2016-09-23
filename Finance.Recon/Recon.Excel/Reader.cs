using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;
using Recon.Core;
using Recon.File;
using Recon.Interface;
using Recon.Model;

namespace Recon.Excel
{
    public class Reader
    {
        private readonly IReconFile _fromFile;
        private readonly IReconFile _toFile;

        private IList<IRecon> _fromRecon;
        public IList<IRecon> FromRecon
        {
            get
            {
                if (_fromRecon == null && _fromFile != null)
                {
                    _fromRecon = Process(_fromFile, ReconType.From);
                }
                return _fromRecon;
            }
        }

        private IList<IRecon> _toRecon;
        public IList<IRecon> ToRecon
        {
            get
            {
                if (_toRecon == null && _toFile != null)
                {
                    _toRecon = Process(_toFile, ReconType.To);
                }
                return _toRecon;
            }
        }

        public Reader(IReconFile fromFile, IReconFile toFile)
        {
            _fromFile = fromFile;
            _toFile = toFile;
        }

        private IList<IRecon> Process(IReconFile file, ReconType reconType)
        {
            IList<IRecon> result = new List<IRecon>();
            IExcelDataReader excelReader = null;
            FileHelper fileHelper = new FileHelper();
            using (Stream fileStream = FileHelper.GetFileStream(file.FileInfo))
            {
                if (file.FileInfo.Extension.Equals(".xls"))
                {
                    //Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(fileStream);
                }
                else if (file.FileInfo.Extension.Equals(".xlsx"))
                {
                    //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                }
                if (excelReader != null)
                {
                    //DataSet - The result of each spreadsheet will be created in the result.Tables
                    DataSet dataSet = excelReader.AsDataSet();

                    //Data Reader methods
                    foreach (DataTable table in dataSet.Tables)
                    {
                        if (columnsExists(table, ref file))
                        {
                            for (int row = file.HearderRow + 1; row < table.Rows.Count; row++)
                            {
                                var columnIdValue = table.Rows[row][file.ColumnIdIndex].ToString();
                                var columnValueString = table.Rows[row][file.ColumnValueIndex].ToString();

                                if (!string.IsNullOrEmpty(columnIdValue) && !string.IsNullOrEmpty(columnValueString))
                                {
                                    decimal columnValue = columnValueString.ToDecimal();

                                    IRecon newRecon = null;
                                    if (reconType == ReconType.From)
                                    {
                                        newRecon = new ReconFrom();
                                    }
                                    else if (reconType == ReconType.To)
                                    {
                                        newRecon = new ReconTo();
                                    }
                                    newRecon.Reference = columnIdValue;
                                    newRecon.Amount = columnValue;
                                    result.Add(newRecon);
                                }
                            }
                        }
                    }

                    excelReader.Close();
                    excelReader.Dispose();
                    Console.WriteLine("File {0} read complete", file.FileInfo.FullName);
                }
            }
            return result;
        }

        private bool columnsExists(DataTable table, ref IReconFile file)
        {
            for (int row = 0; row < table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    var cellValue = table.Rows[row][col].ToString().Trim();
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        if (cellValue.Equals(file.ColumnId.Trim()))
                        {
                            file.ColumnIdIndex = col;
                            file.HearderRow = row;
                        }
                        else if (cellValue.Equals(file.ColumnValue.Trim()))
                        {
                            file.ColumnValueIndex = col;
                        }
                        if (file.ColumnIdIndex > -1 && file.ColumnValueIndex > -1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}