using MyQRCodeGenerator;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
String testURL = "https://www.images.google.com/search?channel=fs&client=ubuntu&q=skiasharp+qrcode+white+canvas";
Console.WriteLine("original url: " + testURL);
Generator test = new Generator(testURL);
//test.Generate();
Console.WriteLine("parsed url: " + test.parseURL());