// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Tesseract;

class Program
{
    static void Main()
    {
        
        Document doc = new Document();
        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("barcode.pdf", FileMode.Create));

        doc.Open();

        
        Barcode39 barcode = new Barcode39();
        Console.WriteLine("Word or sentence to encode: ");
        string s = Console.ReadLine();
        barcode.Code = s; 
        barcode.StartStopText = false;

        Image img = barcode.CreateImageWithBarcode(writer.DirectContent, null, null);
        img.ScalePercent(200); 

        
        doc.Add(img);

        doc.Close();

        Console.WriteLine("Barcode PDF created.");




        string imagePath = "C:\\Users\\HP\\source\\repos\\BarcodeCreatorandReader\bin\\Debug\net6.0\barcode.pdf"; 

        using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
        {
            using (var img2 = Pix.LoadFromFile(imagePath))
            {
                using (var page = engine.Process(img2))
                {
                    string extractedText = page.GetText();
                    Console.WriteLine("Extracted text from barcode: " + extractedText);
                }
            }
        }
    }
}


