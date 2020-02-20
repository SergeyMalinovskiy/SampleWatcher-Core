using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SW_Core.Utils
{
    static class CoreLogger
    {
        public static void ShowLogo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("####################################################################################");
            Console.ResetColor();
            nl();
            G(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); printVersion();     p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); p(); W(); p(); p(); W(); p(); p(); p(); W(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); W(); p(); W(); p(); W(); p(); p(); p(); W(); p(); p(); p(); p(); T(); W(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); W(); p(); p(); W(); p(); W(); p(); p(); p(); W(); p(); p(); p(); W(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); W(); p(); p(); p(); p(); W(); p(); p(); p(); W(); p(); p(); p(); W(); p(); p(); p(); p(); W(); p(); p(); W(); p(); W(); p(); T(); T(); W(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); T(); W(); p(); p(); W(); p(); p(); p(); W(); p(); p(); p(); W(); p(); p(); p(); W(); p(); W(); p(); T(); W(); p(); p(); W(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); p(); p(); W(); p(); W(); p(); W(); p(); W(); p(); p(); p(); W(); p(); p(); p(); W(); p(); W(); p(); W(); p(); p(); p(); T(); W(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); W(); p(); p(); W(); p(); T(); W(); p(); T(); W(); p(); p(); p(); W(); p(); p(); p(); W(); p(); W(); p(); W(); p(); p(); p(); W(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); T(); W(); p(); p(); W(); p(); p(); p(); W(); p(); p(); p(); p(); T(); W(); p(); p(); W(); p(); p(); W(); p(); p(); p(); T(); T(); W(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            G(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); p(); G(); nl();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("####################################################################################");
            Console.ResetColor();
            nl();
        }

        private static void p()
        {
            Console.Write("  ");
        }

        private static void G()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("#");
            Console.ResetColor();
        }

        private static void W()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(">");
            Console.ResetColor();
        }

        private static void printVersion()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("v. 0.0.1");
            Console.ResetColor();
        }

        private static void T()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("  ");
            Console.ResetColor();
        }

        private static void nl()
        {
            Console.WriteLine(" ");
        }
    }
}
