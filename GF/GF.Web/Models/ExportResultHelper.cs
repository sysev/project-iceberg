﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using GF.Web.Infrastructure;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ExcelGenerator.SpreadSheet;

namespace GF.Web.Models
{

   
    /// <summary>
    /// This class represents an MVC Action result for
    /// a jquery.datatables response.
    /// </summary>
    public class ExportResultHelper 
    {
        private static string ITEM = "Item";
        private static string REPORT = "Report";


        //custom excel formatting (sort of)
        public static ActionResult GetXMLResult(IList<IList<KeyValuePair<string, string>>> table)
        {
            StringBuilder sb = new StringBuilder(Utils.XMLHeader);
            sb.Append(Utils.TagStart(REPORT));
            foreach (var row in table)
            {
                sb.Append(Utils.TagStart(ITEM));
                foreach (var column in row)
                {
                    sb.Append(Utils.XmlElement(column));
                }
                sb.Append(Utils.TagEnd(ITEM));
            }
            sb.Append(Utils.TagEnd(REPORT));
            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            var ms = new MemoryStream(bytes, false);
            return new FileStreamResult(ms, "text/xml")
            {
                FileDownloadName = "report.xml"
            };
        }

         
        public static ActionResult GetCSVResult(IList<IList<string>> table)
        {
            return GetCSVOrXLSResult(table, "text/csv", "report.csv");
        }

        public static ActionResult GetXLSAsCSVResult(IList<IList<string>> table)
        {
            return GetCSVOrXLSResult(table, "application/excel", "report.csv");
        }

        //returns an XLS Stream
        public static byte[] GetXLS(IList<IList<string>> table)
        {
            //Create a workbook
            Workbook workbook = new Workbook();

            //Create a worksheet
            Worksheet worksheet = new Worksheet("Report");

            //Create a new row
            foreach (var row in table)
            {
                 var xlsRow = new ExcelGenerator.SpreadSheet.Row();
                 foreach (var col in row)
                 {
                     xlsRow.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(col));
                 }
                 worksheet.Rows.Add(xlsRow);
            }
             
            //Add worksheet to Workbook
            workbook.Worksheets.Add(worksheet);

            return workbook.getBytes();
        }

        private static ActionResult GetCSVOrXLSResult(IList<IList<string>> table, string type, string name)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var row in table)
            {
                for(int i=0; i<row.Count; i++)
                {
                    var item = row[i];
                    if (i > 0)
                        sb.Append(",");
                    sb.Append(Utils.Quote(item));
                }
                sb.Append(Environment.NewLine);
            }
            var bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            var ms = new MemoryStream(bytes, false);
            return new FileStreamResult(ms, type)
            {
                FileDownloadName = name
            };
        }

        public static byte[] GetPDF( IList<IList<string>> table)
        {
            var outputStream = new MemoryStream();

            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
           
            PdfWriter writer = PdfWriter.GetInstance(document, outputStream);

            document.Open();

            var font = FontFactory.GetFont("Arial", 8, Color.GRAY);
            var Hdrfont = FontFactory.GetFont("Arial", 10, Color.BLACK);
            

            var header = table[0];
            PdfPTable pdfTable = new PdfPTable(header.Count);

            foreach (var column in header)
            {
                pdfTable.AddCell(new Phrase(column, Hdrfont));
            }

            for (int i = 1; i < table.Count; i++)
            {
                var row = table[i];
                foreach (var column in row)
                {
                    var cell = new Phrase(column, font);
                    pdfTable.AddCell(cell);
                }
            }
             
            document.Add(pdfTable);
            document.Close();

           return outputStream.ToArray();
           
        }

      

    }
}