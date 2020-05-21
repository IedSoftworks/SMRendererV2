using System;
using System.IO;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Core
{
    /// <summary>
    /// Contains some useful functions
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// Read a file that is saved in a assembly
        /// </summary>
        /// <param name="ass">The assembly that contains the file</param>
        /// <param name="path">The path to the file inside the assembly</param>
        /// <returns></returns>
        public static string ReadAssemblyFile(Assembly ass, string path)
        {
            return new StreamReader(ass.GetManifestResourceStream(ass.GetName().Name + "."+path) ?? throw new InvalidOperationException("Assembly couldn't find resource at path '"+path+"'.")).ReadToEnd();
        }
    }
}