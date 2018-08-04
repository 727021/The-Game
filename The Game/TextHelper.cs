using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Game
{
    static class TextHelper
    {
        public enum BorderType
        {
            TOP,
            BOTTOM,
            SIDES
        }

        private struct Colors
        {
            public const ConsoleColor borderColor = ConsoleColor.Red;
            public const ConsoleColor titleColor = ConsoleColor.Red;
            public const ConsoleColor subtitleColor = ConsoleColor.White;
            public const ConsoleColor actionColor = ConsoleColor.Yellow;
        }

        static string Border(BorderType borderType)
        {
            int width = Console.WindowWidth - 2;
            string border = "";

            if (borderType != BorderType.SIDES)
            {
                for (int i = 0; i < width; i++)
                    border += "═";
                border = ((borderType == BorderType.TOP) ? "╔" : "╚") + border + ((borderType == BorderType.TOP) ? "╗" : "╝");
            }
            else
            {
                border += "║";
                for (int i = 0; i < width; i++)
                    border += " ";
                border += "║";
            }

            return border;
        }

        public static void BorderedText(string[] title)
        {
            int height = Console.WindowHeight - 4 - title.Length;

            // Top border
            Console.ForegroundColor = Colors.borderColor;
            Console.Write(Border(BorderType.TOP));
            Console.ResetColor();
            // Blank lines before the text
            for (int i = 0; i < height / 2; i++)
            {
                Console.ForegroundColor = Colors.borderColor;
                Console.Write(Border(BorderType.SIDES));
                Console.ResetColor();
            }
            // Text lines
            foreach (string line in title)
            {
                int width = ((Console.WindowWidth - line.Length) / 2) - 1;
                string spaces = string.Empty;
                if (width > 0)
                    spaces = new string(' ', width);

                Console.ForegroundColor = Colors.borderColor;
                Console.Write(((line.Length % 2 == 0) ? "║" : "║ ") + spaces);
                Console.ResetColor();
                Console.Write(line);
                Console.ForegroundColor = Colors.borderColor;
                Console.Write(spaces + "║");
                Console.ResetColor();
            }
            // Blank lines after text
            for (int i = 0; i < height / 2; i++)
            {
                Console.ForegroundColor = Colors.borderColor;
                Console.Write(Border(BorderType.SIDES));
                Console.ResetColor();
            }
            // Bottom border
            Console.ForegroundColor = Colors.borderColor;
            Console.Write(Border(BorderType.BOTTOM));
            Console.ResetColor();
        }
    }
}
