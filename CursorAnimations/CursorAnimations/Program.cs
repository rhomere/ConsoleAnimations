using System;
using System.Collections.Generic;
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
            SpinningCursor();
            
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
