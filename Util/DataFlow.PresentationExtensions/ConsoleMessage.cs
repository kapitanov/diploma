using System;

namespace AISTek.DataFlow.PresentationExtensions
{
    public static class ConsoleMessage
    {
        public static IDisposable Forecolor(ConsoleColor color)
        {
            return new ConsoleForecolor(color);
        }

        public static IDisposable Error()
        {
            return new ConsoleForecolor(ConsoleColor.Red);
        }
        
        public static IDisposable Warning()
        {
            return new ConsoleForecolor(ConsoleColor.Yellow);
        }
        
        public static IDisposable Info()
        {
            return new ConsoleForecolor(ConsoleColor.Green);
        }
        
        public static IDisposable Highlight()
        {
            return new ConsoleForecolor(ConsoleColor.Cyan);
        }

        private class ConsoleForecolor : IDisposable
        {
            public ConsoleForecolor(ConsoleColor newColor)
            {
                oldColor = Console.ForegroundColor;
                Console.ForegroundColor = newColor;
            }

            private readonly ConsoleColor oldColor;

            public void Dispose()
            {
                Console.ForegroundColor = oldColor;
            }
        }
    }
}
