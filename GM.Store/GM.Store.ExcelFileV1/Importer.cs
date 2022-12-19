using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GM.Store.ExcelFileV1
{


    class XmlParsingModel
    {
        public XmlParsingModel()
        {
            HeaderRow = new Dictionary<int, string>();
        }
        public Dictionary<string, string> Root { get; set; }

        [JsonIgnore]
        public Dictionary<int, string> HeaderRow { get; set; }
    }

    public class Importer
    {
        public Importer()
        {

        }
        /// <summary>
        /// Converts excel data to object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="mapperXmlFilePath">Mapper file nodes must be the object property names</param>
        /// <returns></returns>
        public void ImportToObjectList(string filePath, string mapperXmlFilePath, Func<List<Dictionary<string, string>>, Task<int>> onRowRead)
        {

            var mapperDoc = new XmlDocument();
            mapperDoc.Load(mapperXmlFilePath);
            var mapperJson = JsonConvert.SerializeXmlNode(mapperDoc);
            var mapperObject = JsonConvert.DeserializeObject<XmlParsingModel>(mapperJson);
            //var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //var resultList = ParseExcelToListNew(filePath, mapperObject, onRowRead);
            var resultList = ParseExcelToList(filePath, mapperObject, onRowRead);
        }

        List<Dictionary<string, string>> ParseExcelToList(string fileName, XmlParsingModel mapper, Func<List<Dictionary<string, string>>, Task<int>> onRowRead)
        {
            var batchSize = 2000;
            var exRowList = new List<Dictionary<string, string>>();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

                foreach (WorksheetPart worksheetPart in workbookPart.WorksheetParts)
                {
                    OpenXmlReader reader = OpenXmlReader.Create(worksheetPart);
                    int rowIndex = 0;
                    int batchCount = 0;
                    while (reader.Read())
                    {
                        try
                        {
                            if (reader.ElementType == typeof(Row))
                            {
                                reader.ReadFirstChild();
                                int columnIndex = 0;
                                var exRow = new Dictionary<string, string>();

                                do
                                {
                                    if (reader.ElementType == typeof(Cell))
                                    {
                                        Cell c = (Cell)reader.LoadCurrentElement();
                                        string cellReference = c.CellReference.Value;
                                        int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cellReference));
                                        string cellValue;

                                        try
                                        {
                                            if (c.DataType != null && c.DataType == CellValues.SharedString)
                                            {
                                                if (c.CellValue == null)
                                                {
                                                    columnIndex++;
                                                    continue;
                                                }

                                                SharedStringItem ssi = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(int.Parse(c.CellValue.InnerText));
                                                cellValue = ssi.Text.Text;
                                            }
                                            else
                                            {
                                                if (c.StyleIndex != null)
                                                {
                                                    var cellFormat = workbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ChildElements[int.Parse(c.StyleIndex.InnerText)] as CellFormat;
                                                    if (cellFormat != null)
                                                    {
                                                        var dateFormat = GetDateTimeFormat(cellFormat.NumberFormatId);
                                                        cellValue = DateTime.FromOADate(double.Parse(c.CellValue.InnerXml)).ToString("dd-MM-yyyy");
                                                    }
                                                    else
                                                    {
                                                        cellValue = c.CellValue.InnerXml;
                                                    }
                                                }
                                                else
                                                {
                                                    cellValue = c.CellValue.InnerXml;
                                                }
                                                
                                            }
                                            //else
                                            //{
                                            //    if (c.CellValue == null)
                                            //    {
                                            //        columnIndex++;
                                            //        continue;
                                            //    }
                                            //    if (double.TryParse(c.CellValue.InnerText, out double result))
                                            //    {
                                            //        DateTime conv = DateTime.FromOADate(result);
                                            //        if (conv != DateTime.MinValue)
                                            //        {
                                            //            cellValue = conv.ToString();
                                            //        }
                                            //        cellValue = c.CellValue.InnerText;
                                            //    }
                                            //    else
                                            //    {

                                            //        cellValue = c.CellValue.InnerText;
                                            //    }

                                            //   // double d = double.Parse(cellValue);

                                            //}

                                            if (rowIndex == 0)
                                            {
                                                foreach (var item in mapper.Root)
                                                {
                                                    if (item.Value == null)
                                                    {
                                                        columnIndex++;
                                                        continue;
                                                    }
                                                    if (item.Value.Trim().ToLower() == cellValue.Trim().ToLower())
                                                        mapper.HeaderRow.Add(cellColumnIndex, item.Key);
                                                }
                                            }
                                            else
                                            {
                                                if (mapper.HeaderRow.Keys.Any(a => a == cellColumnIndex))
                                                {
                                                    exRow.Add(mapper.HeaderRow[cellColumnIndex], cellValue);
                                                }
                                                else
                                                {
                                                    exRow.Add(mapper.HeaderRow[cellColumnIndex], null);
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                        //Console.Out.Write("{0}: {1} ", c.CellReference, cellValue);
                                        
                                    }
                                    columnIndex++;
                                }
                                while (reader.ReadNextSibling());

                                if (rowIndex > 0 && exRow.Count>0)
                                {
                                    exRowList.Add(exRow);
                                }
                                if (batchCount == batchSize)
                                {
                                    onRowRead(exRowList);
                                    exRowList = new List<Dictionary<string, string>>();
                                    batchCount = 0;
                                }

                                //exRowList.Add(exRow);
                                rowIndex++;
                                batchCount++;

                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                    ///if count less than batch size list will keep the value
                    if (exRowList.Count > 0)
                    {
                        onRowRead(exRowList);
                        exRowList = null;
                    }
                    
                }
            }
            return exRowList;
            //Console.ReadKey();
        }

        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }
        public static int? GetColumnIndexFromName(string columnName)
        {
            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }

        private string GetDateTimeFormat(uint numberFormatId)
        {
            return DateFormatDictionary.ContainsKey(numberFormatId) ? DateFormatDictionary[numberFormatId] : string.Empty;
        }

        //// https://msdn.microsoft.com/en-GB/library/documentformat.openxml.spreadsheet.numberingformat(v=office.14).aspx
        private readonly Dictionary<uint, string> DateFormatDictionary = new Dictionary<uint, string>()
        {
            [14] = "dd/MM/yyyy",
            [15] = "d-MMM-yy",
            [16] = "d-MMM",
            [17] = "MMM-yy",
            [18] = "h:mm AM/PM",
            [19] = "h:mm:ss AM/PM",
            [20] = "h:mm",
            [21] = "h:mm:ss",
            [22] = "M/d/yy h:mm",
            [30] = "M/d/yy",
            [34] = "yyyy-MM-dd",
            [45] = "mm:ss",
            [46] = "[h]:mm:ss",
            [47] = "mmss.0",
            [51] = "MM-dd",
            [52] = "yyyy-MM-dd",
            [53] = "yyyy-MM-dd",
            [55] = "yyyy-MM-dd",
            [56] = "yyyy-MM-dd",
            [58] = "MM-dd",
            [165] = "M/d/yy",
            [166] = "dd MMMM yyyy",
            [167] = "dd/MM/yyyy",
            [168] = "dd/MM/yy",
            [169] = "d.M.yy",
            [170] = "yyyy-MM-dd",
            [171] = "dd MMMM yyyy",
            [172] = "d MMMM yyyy",
            [173] = "M/d",
            [174] = "M/d/yy",
            [175] = "MM/dd/yy",
            [176] = "d-MMM",
            [177] = "d-MMM-yy",
            [178] = "dd-MMM-yy",
            [179] = "MMM-yy",
            [180] = "MMMM-yy",
            [181] = "MMMM d, yyyy",
            [182] = "M/d/yy hh:mm t",
            [183] = "M/d/y HH:mm",
            [184] = "MMM",
            [185] = "MMM-dd",
            [186] = "M/d/yyyy",
            [187] = "d-MMM-yyyy"
        };
        public static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue?.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
        private static int CellReferenceToIndex(Cell cell)
        {
            int index = 0;
            string reference = cell.CellReference.ToString().ToUpper();
            foreach (char ch in reference)
            {
                if (Char.IsLetter(ch))
                {
                    int value = (int)ch - (int)'A';
                    index = (index == 0) ? value : ((index + 1) * 26) + value;
                }
                else
                {
                    return index;
                }
            }
            return index;
        }
       
    }
}
