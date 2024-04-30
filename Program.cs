using System.Drawing;

namespace CropPictures
{
    internal class Program
    {
        static int ReadCoordinate()
        {
            var number = Console.ReadLine();
            if (number == null) return 0;
            if (int.TryParse(number, out var x)) return x;
            else
            {
                Console.WriteLine("enter correct number");
                ReadCoordinate();
            }
            return 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter directory path with images to crop:");
            var dir = Console.ReadLine();
            string[] pictures;
            if (dir != null) pictures = Directory.GetFiles(dir);
            else return;
            Console.WriteLine("Enter X coordinate of upper left angle of rectangular area to crop");
            var x1 = ReadCoordinate();
            Console.WriteLine("Enter Y coordinate of upper left angle of rectangular area to crop");
            var y1 = ReadCoordinate();
            Console.WriteLine("Enter X coordinate of bottom right angle of rectangular area to crop");
            var x2 = ReadCoordinate();
            Console.WriteLine("Enter Y coordinate of bottom right angle of rectangular area to crop");
            var y2 = ReadCoordinate();
            foreach (var picture in pictures)
            {
                Bitmap img;
                try
                {
                    img = (Bitmap)Image.FromFile(picture);
                }
                catch
                {
                    continue;
                }
                var width = x2 - x1 + 1;
                var height = y2 - y1 + 1;
                var result = new Bitmap(width, height);
                for (var i = x1; i <= x2; i++)
                    for (var j = y1; j <= y2; j++)
                        result.SetPixel(i - x1, j - y1, img.GetPixel(i, j));
                var pathToSave = dir + "\\croped";
                Directory.CreateDirectory(pathToSave);
                result.Save(pathToSave + picture.Substring(dir.Length));
            }
        }
    }
}
