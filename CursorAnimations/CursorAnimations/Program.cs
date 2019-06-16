using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CursorAnimations
{
    class Program
    {
        static void Main(string[] args)
        {
            // Spinning
            //SpinningCursor();

            StarWarsIntro();

            //DrawGif();
            
        }

        private static void DrawGif()
        {
            Image image = Image.FromFile(@"C:\projects\some_animated_gif.gif");
            FrameDimension dimension = new FrameDimension(
                               image.FrameDimensionsList[0]);
            int frameCount = image.GetFrameCount(dimension);
            StringBuilder sb;

            // Remember cursor position
            int left = Console.WindowLeft, top = Console.WindowTop;

            char[] chars = { '#', '#', '@', '%', '=', '+',
                         '*', ':', '-', '.', ' ' };
            for (int i = 0; ; i = (i + 1) % frameCount)
            {
                sb = new StringBuilder();
                image.SelectActiveFrame(dimension, i);

                for (int h = 0; h < image.Height; h++)
                {
                    for (int w = 0; w < image.Width; w++)
                    {
                        Color cl = ((Bitmap)image).GetPixel(w, h);
                        int gray = (cl.R + cl.G + cl.B) / 3;
                        int index = (gray * (chars.Length - 1)) / 255;

                        sb.Append(chars[index]);
                    }
                    sb.Append('\n');
                }

                Console.SetCursorPosition(left, top);
                Console.Write(sb.ToString());

                System.Threading.Thread.Sleep(100);
            }
        }

        private static void StarWarsIntro()
        {
            Console.CursorVisible = false;

            var arr = new[]
            {
            @"        ________________.  ___     .______  ",
            @"       /                | /   \    |   _  \",
            @"      |   (-----|  |----`/  ^  \   |  |_)  |",
            @"       \   \    |  |    /  /_\  \  |      /",
            @"  .-----)   |   |  |   /  _____  \ |  |\  \-------.",
            @"  |________/    |__|  /__/     \__\| _| `.________|",
            @"   ____    __    ____  ___     .______    ________.",
            @"   \   \  /  \  /   / /   \    |   _  \  /        |",
            @"    \   \/    \/   / /  ^  \   |  |_)  ||   (-----`",
            @"     \            / /  /_\  \  |      /  \   \",
            @"      \    /\    / /  _____  \ |  |\  \---)   |",
            @"       \__/  \__/ /__/     \__\|__| `._______/",
        };

            var maxLength = arr.Aggregate(0, (max, line) => Math.Max(max, line.Length));
            var x = Console.BufferWidth / 2 - maxLength / 2;
            for (int y = -arr.Length; y < Console.WindowHeight + arr.Length; y++)
            {
                ConsoleDraw(arr, x, y);
                Thread.Sleep(100);
            }
        }

        static void ConsoleDraw(IEnumerable<string> lines, int x, int y)
        {
            if (x > Console.WindowWidth) return;
            if (y > Console.WindowHeight) return;

            var trimLeft = x < 0 ? -x : 0;
            int index = y;

            x = x < 0 ? 0 : x;
            y = y < 0 ? 0 : y;

            var linesToPrint =
                from line in lines
                let currentIndex = index++
                where currentIndex > 0 && currentIndex < Console.WindowHeight
                select new
                {
                    Text = new String(line.Skip(trimLeft).Take(Math.Min(Console.WindowWidth - x, line.Length - trimLeft)).ToArray()),
                    X = x,
                    Y = y++
                };

            Console.Clear();
            foreach (var line in linesToPrint)
            {
                Console.SetCursorPosition(line.X, line.Y);
                Console.Write(line.Text);
            }
        }

        private static void SpinningCursor()
        {
            Console.CursorVisible = false;
            Console.WriteLine("Press CTRL-C to exit.");

            var s = new ConsoleSpinner();

            while (true)
            {
                Thread.Sleep(100); // simulate some work being done
                s.UpdateProgress();
            }
        }

        public void UpdateProgress()
        {
            // Hide cursor to improve the visuals (note: we should re-enable at some point)
            Console.CursorVisible = false;

            // Store the current position of the cursor
            var originalX = Console.CursorLeft;
            var originalY = Console.CursorTop;

            // etc.
        }


    }
}
