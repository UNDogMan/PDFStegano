using System;
using System.IO;
using System.Text;
using System.Linq;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;

namespace PDFSteganoLib
{
    public static class PdfLineSpacingStegano
    {
        public static string GetText(string src)
        {
            if (File.Exists(src))
            {
                using (var pdfDoc = new PdfDocument(new PdfReader(src)))
                {
                    StringBuilder sb = new StringBuilder();
                    var strategy = new LocationTextExtractionStrategy();
                    for (int i = 1; i <= pdfDoc.GetNumberOfPages(); ++i)
                    {
                        var page = pdfDoc.GetPage(i);
                        sb.Append(PdfTextExtractor.GetTextFromPage(page, strategy));
                    }

                    return sb.ToString();
                }
            }
            return "";
        }

        public static string GetText(string src, int pageNum)
        {
            if (File.Exists(src))
            {
                using (var pdfDoc = new PdfDocument(new PdfReader(src)))
                {
                    if (pageNum <= 0 || pageNum > pdfDoc.GetNumberOfPages())
                        return "";
                    var strategy = new LocationTextExtractionStrategy();
                    var page = pdfDoc.GetPage(pageNum);
                    return PdfTextExtractor.GetTextFromPage(page, strategy);
                }
            }
            return "";
        }

        public static void InsertMessage(string src, string dest, string msg)
        {
            if (File.Exists(src))
            {
                int i = GetText(src, 1).Count(x=> x == '\u00A0');
                using (PdfReader pdfReader = new PdfReader(src))
                using (PdfWriter pdfWriter = new PdfWriter(dest))
                {
                    using (PdfDocument pdfDoc = new PdfDocument(pdfReader, pdfWriter))
                    {
                        using (Document doc = new Document(pdfDoc))
                        {
                            var par = new Paragraph();
                            par.SetFixedLeading(-1000f - 111f * (i + 2));
                            par.SetCharacterSpacing(-4f);
                            par.SetWordSpacing(-4f);

                            Text text = new Text(string.Format("{0}{1}{0}", '\u00A0', msg));
                            text.SetFontColor(ColorConstants.WHITE, 0);
                            par.Add(text);
                            doc.Add(par);
                        }
                    }
                }
            }
        }

        public static string GetMessage(string src)
        {
            if (File.Exists(src))
            {
                string text = GetText(src, 1);
                if (text.Count(x => x == '\u00A0') < 2)
                {
                    return "";
                }
                return text.Split(new char[] { '\u00A0' }, StringSplitOptions.RemoveEmptyEntries)[0];
            }

            return "";
        }
    }
}