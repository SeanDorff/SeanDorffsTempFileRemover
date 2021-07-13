using System;

namespace SeanDorffsTempFileRemover
{
    internal static class ConsoleWriter
    {
        internal static void WriteLine(string text, bool withTimeStamp = true)
        {
            Write(text + "\n", withTimeStamp);
        }

        internal static void Write(string text, bool withTimeStamp = true)
        {
            text = text.Replace("\t", "   ");
            if (withTimeStamp)
                Console.Write(GetTimeStamp() + "   " + text);
            else
                Console.Write("   " + text);
        }

        internal static void EmptyLine(bool withTimeStamp = false)
        {
            WriteLine("", withTimeStamp);
        }

        private static string GetTimeStamp()
        {
            return DateTime.Now.ToString("HH:mm:ss.fff");
        }
    }
}
