using System;

namespace FTetris.Console
{
    static class ConsoleWriter
    {
        public static void WriteLine(string text)
        {
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine(text);
        }

        public static void WriteLine()
        { System.Console.WriteLine(); }

        public static void Write(ConsoleColor color, char character)
        {
            System.Console.ForegroundColor = color;
            System.Console.Write(character);
        }
    }
}
