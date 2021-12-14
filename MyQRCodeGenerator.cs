using System;
using System.Drawing;
using System.Linq;
using SkiaSharp;
using SkiaSharp.QrCode;

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

            ECCLevel level = ECCLevel.H;
            QRCodeData qr = generator.CreateQrCode(url, level);

            SKImageInfo info = new SKImageInfo(512, 512);
            SKSurface surface = SKSurface.Create(info);

            SKCanvas canvas = surface.Canvas;
            canvas.Render(qr, 512, 512/*, SKColors.White*/);

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(@$"qr-{level}.png");
            data.SaveTo(stream);
            Console.WriteLine("QR Code Generated");
        }
    }
}

