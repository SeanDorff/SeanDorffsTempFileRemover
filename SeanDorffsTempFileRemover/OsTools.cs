using System;
using System.Runtime.InteropServices;

namespace SeanDorffsTempFileRemover
{
    internal class OsTools
    {
        private OsPlatforms osPlatform;

        internal OsTools()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
                osPlatform = OsPlatforms.FreeBSD;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                osPlatform = OsPlatforms.Linux;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                osPlatform = OsPlatforms.OSX;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                osPlatform = OsPlatforms.Windows;
            else
                throw new Exception("Unkown platform type");
        }

        internal OsPlatforms GetOSPlatform() => osPlatform;
    }

    internal enum OsPlatforms
    {
        FreeBSD,
        Linux,
        OSX,
        Windows
    }
}
