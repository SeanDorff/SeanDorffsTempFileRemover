using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SeanDorffsTempFileRemover
{
    internal class DirectoryCleaner
    {
        private readonly OsTools osTools = new OsTools();
        private readonly OsPlatforms osPlatform;
        private readonly List<string> directoriesToClean = new List<string>();

        public DirectoryCleaner()
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
            foreach (string directory in directoriesToClean)
            {
                ConsoleWriter.WriteLine("\t" + directory + " ...");
                DeleteDirectoryContents(directory);
            }

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

        private void DeleteDirectoryContents(string directory)
        {
            List<string> subDirectories = new List<string>(Directory.GetDirectories(directory));
            foreach (string subDirectory in subDirectories)
            {
                DeleteDirectoryContents(subDirectory);
                ConsoleWriter.WriteLine("\t\tDirectory: " + subDirectory);
                try
                {
                    Directory.Delete(subDirectory);
                }
                catch (IOException e)
                {
                    ConsoleWriter.WriteLine("\t\t\t" + e.Message);
                }
            }
            List<string> files = new List<string>(Directory.GetFiles(directory));
            foreach (string file in files)
            {
                ConsoleWriter.WriteLine("\t\tFile:      " + file);
                try
                {
                    File.Delete(file);
                }
                catch (UnauthorizedAccessException e)
                {
                    ConsoleWriter.WriteLine("\t\t\t" + e.Message);
                }
                catch (IOException e)
                {
                    ConsoleWriter.WriteLine("\t\t\t" + e.Message);
                }
            }
        }
    }
}
