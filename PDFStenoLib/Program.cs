using System;
using System.Linq;
using System.Text;
using iText;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PDFSteganoLib
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            InsertInfo();
            var str = PdfLineSpacingStegano.GetText(@"D:/out4.pdf");
            Console.WriteLine('\u00A0');
            Console.WriteLine("Number of \\u00A0 " + str.Count(x => x == '\u00A0'));
            Console.WriteLine(str);
            Console.WriteLine(str.Split(new char[] { '\u00A0' }, StringSplitOptions.RemoveEmptyEntries).Length);
            Console.WriteLine(PdfLineSpacingStegano.GetMessage(@"D:/out4.pdf"));
        }

        public static void SomeTry()
        {
            //PdfReader pdfReader = new PdfReader(@"file:///D:/Main/Downloads/resume_45806935.pdf");
            PdfWriter pdfWriter = new PdfWriter(@"D:/out.pdf");
            PdfDocument pdfDoc = new PdfDocument(/*pdfReader,*/ pdfWriter);
            Document doc = new Document(pdfDoc);
            Paragraph pr1 = new Paragraph(
                "Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen Lorem ipsum dor sir amen "
            );
            //pr1.SetMultipliedLeading(1.2f);
            doc.Add(pr1);
            var par = new Paragraph();
            par.SetCharacterSpacing(-4f);
            par.SetWordSpacing(-4f);
            var font = PdfFontFactory.CreateFont();
            var text = new Text(
                "Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me Allo its me ");
            text.SetFont(font);
            text.SetFontColor(ColorConstants.WHITE, 0f);
            par.Add(text);
            par.SetFixedLeading(-1000f);
            doc.Add(par);
            doc.Add(new Paragraph("Lorem ipsum dor sir amen").SetFixedLeading(1000f));
            pdfDoc.Close();
        }

        public static void InsertInfo()
        {
            PdfReader pdfReader = new PdfReader(@"D:/out3.pdf");
            PdfWriter pdfWriter = new PdfWriter(@"D:/out4.pdf");
            PdfDocument pdfDoc = new PdfDocument(pdfReader, pdfWriter);
            Document doc = new Document(pdfDoc);

            var par = new Paragraph();
            par.SetFixedLeading(-1000f - 111*2f);
            par.SetCharacterSpacing(-4f);
            par.SetWordSpacing(-4f);

            Text text = new Text(string.Format("{0}{1}{0}", '\u00A0', PrepareMsg("Allo its secret message12223\nDfdfdf123")));
            text.SetFontColor(ColorConstants.WHITE, 0);
            par.Add(text);
            doc.Add(par);
            
            doc.Close();
            pdfDoc.Close();
            pdfWriter.Close();
            pdfReader.Close();
        }

        public static string PrepareMsg(string msg)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var str in msg.Split('\n').Reverse())
            {
                sb.Append(str).Append('\n');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        public static void SomeTryAllo()
        {
            using (var reader = new PdfReader(@"D:/out1.pdf"))
            using (var writer = new PdfWriter(@"D:/out2.pdf"))
            {
                using (var document = new PdfDocument(reader, writer))
                {
                    int pages = document.GetNumberOfPages();
                    for (int i = 1; i <= pages; i++)
                    {
                        var page = document.GetPage(i);
                        var dict = page.GetPdfObject();
                        var xcontent = dict.Get(PdfName.Contents);
                        if (xcontent != null)
                        {
                            PdfArray thearray= xcontent as PdfArray;
                            foreach (PdfObject obj in thearray)
                            {
                                // these objects actually are PdfIndirectReferences
                                // converting them leads nowhere, so here is the point
                                // where I would have to resolve the reference and use whatever
                                // objects I might obtain that way.
                                PdfStream strm = obj as PdfStream;
                                if(strm != null)
                                {
                                    byte[] data = strm.GetBytes();
                                    UTF8Encoding enc = new UTF8Encoding();
                                    Console.WriteLine("-------");
                                    Console.WriteLine(obj);
                                    Console.WriteLine("-------");
                                    string test = enc.GetString(data);
                                    Console.WriteLine(test);
                                    Console.WriteLine("-------");
                                    if (test[0] == 'a')
                                    {
                                        strm.SetData(enc.GetBytes(test.Replace('a', 'A')));
                                        strm.Flush();
                                        obj.Flush();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}