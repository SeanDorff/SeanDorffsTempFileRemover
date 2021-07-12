using System;
using System.Collections.Generic;

namespace SeanDorffsTempFileRemover
{
    internal class TempFileRemover
    {
        private readonly OsTools osTools = new OsTools();
        private readonly OsPlatforms osPlatform;
        private readonly List<string> directoriesToClean = new List<string>();

        public TempFileRemover()
        {
            ConsoleWriter.WriteLine("Detecting operating system platform...");
            osPlatform = osTools.GetOSPlatform();
            ConsoleWriter.WriteLine("\t" + osPlatform);

            ConsoleWriter.WriteLine("Detecting directories to clean...");
            switch (osPlatform)
            {
                case OsPlatforms.Windows:
                    directoriesToClean = GetDirectoriesWindows();
                    break;
                default:
                    ConsoleWriter.WriteLine("\tPlatform not supported.");
                    break;
            }

            ConsoleWriter.EmptyLine();
            ConsoleWriter.WriteLine(directoriesToClean.Count + " directories to clean:");
            foreach (string directory in directoriesToClean)
                ConsoleWriter.WriteLine("\t" + directory);

            ConsoleWriter.EmptyLine();

            ConsoleWriter.WriteLine("Cleaning directories...");
            new DirectoryCleaner(directoriesToClean);

            ConsoleWriter.EmptyLine();
            ConsoleWriter.WriteLine("Quitting application.");
        }

        private List<string> GetDirectoriesWindows()
        {
            List<string> directoriesToClean = new List<string>
            {
                Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\Prefetch",
                Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\Temp",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp"
            };
            return directoriesToClean;
        }
    }
}
