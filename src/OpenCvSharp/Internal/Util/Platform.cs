using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace OpenCvSharp.Internal.Util
{
    // ReSharper disable once InconsistentNaming
    internal enum OS
    {
        Windows,
        Unix
    }

    internal enum Runtime
    {
        DotNet,
        Mono
    }

    /// <summary>
    /// Provides information for the platform which the user is using 
    /// </summary>
    internal static class Platform
    {
        /// <summary>
        /// OS type
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static readonly OS OS;

        /// <summary>
        /// Runtime type
        /// </summary>
        public static readonly Runtime Runtime;

#pragma warning disable CA1810 
        static Platform()
#pragma warning restore CA1810 
        {
#if NET461
            var p = (int)Environment.OSVersion.Platform;
            OS = ((p == 4) || (p == 6) || (p == 128)) ? OS.Unix : OS.Windows;
#else
            OS = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                 RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? OS.Unix
                : OS.Windows;
#endif
            Runtime = (Type.GetType("Mono.Runtime") == null) ? Runtime.Mono : Runtime.DotNet;
        }
    }
}
