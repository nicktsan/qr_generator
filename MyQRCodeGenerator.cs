using System;
using System.Drawing;
using System.Linq;
using SkiaSharp;
using SkiaSharp.QrCode;
using System.Text.RegularExpressions;

namespace MyQRCodeGenerator
{
    public class Generator
    {
        public String[] url_list;
        public Generator(String path)
        {
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            url_list = System.IO.File.ReadAllLines(path);
        }
        public void Generate()
        {
            Console.WriteLine("Generating QR Code.");
            QRCodeGenerator generator = new QRCodeGenerator();

            //Set the Error Correction Capability (ECC) Level to H (High): 30% of data bytes can be restored. 
            //Higher ECC level is needed to combat potentially harsh conditions or visual obstructions such as weather and dirt.
            ECCLevel level = ECCLevel.H;
            SKImageInfo info = new SKImageInfo(512, 512);
            SKSurface surface = SKSurface.Create(info);
            SKCanvas canvas = surface.Canvas;
            //public static void Render(this SKCanvas canvas, QRCodeData data, int width, int hight, SKColor clearColor, SKColor codeColor);
            //Creates a 512px by 512px qr code with white background and black code color for each url in the list.
            foreach(String url in url_list)
            {
                if(!String.IsNullOrEmpty(url))
                {
                    try 
                    {
                        String parsedUrl = parseURL(url);
                        QRCodeData qr = generator.CreateQrCode(url, level);
                        canvas.Render(qr, 512, 512, SKColors.White, SKColors.Black);

                        using var image = surface.Snapshot();
                        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
                        using var stream = File.OpenWrite(parsedUrl + @".png");
                        data.SaveTo(stream);
                    } catch (System.UriFormatException e)
                    {
                        Console.WriteLine("Found a scuffed url: " + url);
                        Console.WriteLine(e.GetType().FullName);
                        Console.WriteLine(e.Message);
                    }
                }
            }
            
            Console.WriteLine("QR Codes Generated");
        }

        //parse subdomain from url_list
        public String parseURL(String url)
        {
            String result = url;
            var uri=new Uri(url);
            var host = uri.Host;
            var split = host.Split('.');
            var secondLast = split.Skip(split.Length - 2).FirstOrDefault();
            return secondLast;
        }
    }
}

