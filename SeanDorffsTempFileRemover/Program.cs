using System;

namespace SeanDorffsTempFileRemover
{
    class Program
    {
        static void Main(string[] args)
        {
            new TempFileRemover();
            ConsoleWriter.WriteLine("Press any key to quit the application.");
            Console.ReadKey();
        }
    }
}
