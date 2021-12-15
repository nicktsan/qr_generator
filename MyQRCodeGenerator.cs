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
        public String url;
        public Generator(String url)
        {
            this.url = url;
        }
        public void Generate()
        {
            Console.WriteLine("Generating QR Code.");
            QRCodeGenerator generator = new QRCodeGenerator();

            //Set the Error Correction Capability (ECC) Level to H (High): 30% of data bytes can be restored. 
            //Higher ECC level is needed to combat potentially harsh conditions or visual obstructions such as weather and dirt.
            ECCLevel level = ECCLevel.H;
            QRCodeData qr = generator.CreateQrCode(url, level);

            SKImageInfo info = new SKImageInfo(512, 512);
            SKSurface surface = SKSurface.Create(info);

            SKCanvas canvas = surface.Canvas;
            //public static void Render(this SKCanvas canvas, QRCodeData data, int width, int hight, SKColor clearColor, SKColor codeColor);
            //Creates a 512px by 512px qr code with white background and black code color.
            canvas.Render(qr, 512, 512, SKColors.White, SKColors.Black);

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(@$"qr-{level}.png");
            data.SaveTo(stream);
            Console.WriteLine("QR Code Generated");
        }

        //parse subdomain from url
        public String parseURL()
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

