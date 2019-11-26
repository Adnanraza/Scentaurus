using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NReco.PdfGenerator;
using System.IO;

namespace App_Code.BusinessClasses
{
    public static class PDFGenerator
    {
        public static void GeneratePDFFile(string content, string outputFile , string header = "", string footer ="")
        {
            HtmlToPdfConverter obj = new HtmlToPdfConverter();
            obj.Orientation = PageOrientation.Portrait;
            obj.Size = PageSize.Letter;
            var margin = new PageMargins();
            margin.Left = 5;
            margin.Right = 5;
            margin.Top = 5;
            margin.Bottom = 5;

            if (header == string.Empty)
            {
                obj.PageHeaderHtml = @"<div style='height: 50px; font-size:30px color:white'> Adding Header</div>";
            }
            else
            {
                obj.PageFooterHtml = header;
            }

            if (footer == string.Empty)
            {
                obj.PageFooterHtml = @"<div style='height: 50px; font-size:30px; color:white'> Adding Header</div>";
            }
            else
            {
                margin.Bottom = 8;
                obj.PageFooterHtml = footer;
            }

            obj.Margins = margin;            
            string outPDFFile = outputFile;
           
            try
            {
                byte[] outPdfBuffer = obj.GeneratePdf(content);
                File.WriteAllBytes(outPDFFile, outPdfBuffer);
                GC.Collect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}