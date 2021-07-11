using System;
using System.Collections.Generic;
using System.Text;

namespace SeanDorffsTempFileRemover
{
    internal class DirectoryCleaner
    {
        private static OsTools osTools = new OsTools();
        private static OsPlatforms osPlatform;
        private static List<string> directoriesToClean = new List<string>();

        public DirectoryCleaner()
        {
            ConsoleWriter.WriteLine("Detecting operating system platform...");
            osPlatform = osTools.GetOSPlatform();
            ConsoleWriter.WriteLine("\t" + osPlatform);

            ConsoleWriter.WriteLine("Detecting directories to clean...");
            switch (osPlatform)
            {
                case OsPlatforms.Windows:
                    directoriesToClean = getDirectoriesWindows();
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
            ConsoleWriter.WriteLine("Go ahead and do it yourself.");
            ConsoleWriter.WriteLine("Quitting application.");
        }

        private List<string> getDirectoriesWindows()
        {
            List<string> directoriesToClean = new List<string>();
            directoriesToClean.Add(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\Prefetch");
            directoriesToClean.Add(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "\\Temp");
            directoriesToClean.Add(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp");
            return directoriesToClean;
        }
    }
}
