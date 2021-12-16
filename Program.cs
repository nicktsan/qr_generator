using MyQRCodeGenerator;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Generator test = new Generator("/home/nicholas/Documents/qr_generator/links.txt");
test.Generate();