using System;
using System.Collections.Generic;

namespace SeanDorffsTempFileRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            new DirectoryCleaner();
            ConsoleWriter.WriteLine("Press any key to close this window.");
            Console.ReadKey();
        }
    }
}
