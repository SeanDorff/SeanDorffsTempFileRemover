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

            switch (subDirectories.Count)
            {
                case 0:
                    break;
                case 1:
                    DeleteSubDirectory(subDirectories[0]);
                    break;
                default: // > 1
                    Task[] taskArray = new Task[subDirectories.Count];
                    int taskNo = 0;
                    foreach (string subDirectory in subDirectories)
                    {
                        taskArray[taskNo++] = Task.Factory.StartNew(() => { DeleteSubDirectory(subDirectory); });
                    }
                    Task.WaitAll(taskArray);
                    break;
            }

            List<string> files = new List<string>(Directory.GetFiles(directory));
            foreach (string file in files)
            {
                ConsoleWriter.WriteLine("\t\tFile:      " + file);
                try
                {
                    File.Delete(file);
                    ConsoleWriter.WriteLine("\t\t\t...deleted");
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

        private void DeleteSubDirectory(string subDirectory)
        {
            DeleteDirectoryContents(subDirectory);
            ConsoleWriter.WriteLine("\t\tDirectory: " + subDirectory);
            try
            {
                Directory.Delete(subDirectory);
                ConsoleWriter.WriteLine("\t\t\t...deleted");
            }
            catch (IOException e)
            {
                ConsoleWriter.WriteLine("\t\t\t" + e.Message);
            }
        }
    }
}
