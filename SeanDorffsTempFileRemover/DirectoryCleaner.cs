using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SeanDorffsTempFileRemover
{
    internal class DirectoryCleaner
    {
        internal DirectoryCleaner(List<string> directories)
        {
            Task[] taskArray = new Task[directories.Count];
            int taskNo = 0;
            foreach (string directory in directories)
            {
                ConsoleWriter.WriteLine("\t" + directory + " ...");
                taskArray[taskNo++] = Task.Factory.StartNew(() => { DeleteDirectoryContents(directory); });
            }
            Task.WaitAll(taskArray);
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
