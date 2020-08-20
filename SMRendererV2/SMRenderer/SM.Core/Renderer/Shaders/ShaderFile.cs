using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using GL = OpenTK.Graphics.OpenGL4.GL;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;

namespace SM.Core.Renderer.Shaders
{

    /// <summary>
    /// Represents a shader file
    /// </summary>
    [Serializable]
    public class ShaderFile : IGLObject
    {
        /// <inheritdoc />
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Shader;

        /// <inheritdoc />
        public int ID { get; set; } = -1;

        /// <summary>
        /// Extensions, that is added to the file.
        /// </summary>
        public List<ShaderFile> Extensions = new List<ShaderFile>();
        /// <summary>
        /// String extensions, that only replace specific pre-defined keywords.
        /// </summary>
        public Dictionary<string, string> StringExtention = new Dictionary<string, string>();

        /// <summary>
        /// The original file content.
        /// </summary>
        public string FileContent;

        /// <summary>
        /// The resulting shader program, after adding all extensions.
        /// </summary>
        public string ShaderData { get; private set; } = "";

        /// <summary>
        /// Creates a shader file.
        /// </summary>
        /// <param name="content">The original file content</param>
        public ShaderFile(string content)
        {
            FileContent = content;
        }
        /// <summary>
        /// Create a shader file, together with some string extensions.
        /// </summary>
        /// <param name="content">The original file</param>
        /// <param name="overrides">The string extensions</param>
        public ShaderFile(string content, Dictionary<string, string> overrides)
        {
            FileContent = content;
            StringExtention = overrides;
        }

        /// <summary>
        /// Applies all extensions to the original file.
        /// </summary>
        /// <returns>The resulting file</returns>
        public string GetData()
        {
            if (ShaderData != "") return ShaderData;

            string storedContent = FileContent;

            // Insert extentions
            foreach (ShaderFile extention in Extensions)
            {
                storedContent = storedContent.Insert(storedContent.IndexOf("void main()"), extention.GetData()+"\r\n");
            }

            // go though the data, remove duplicates and save it. 
            bool structure = false;
            int force = 0;
            string[] ignoreLines = new[] {"}", "};"};
            string[] lines = storedContent.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in storedContent.Split(new [] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (structure && line.StartsWith("}"))
                {
                    structure = false;
                    continue;
                }

                if (line.StartsWith("void main()") || line.StartsWith("struct") && ShaderData.Contains(line))
                    structure = true;

                if (structure) 
                    continue;

                if (line.Contains("{")) force++;
                if (line.Contains("}")) force--;

                if (force <= 0 && !ignoreLines.Contains(line.Trim()) && ShaderData.Contains(line)) continue;

                ShaderData += line + "\r\n";
            }

            // merge multiple main-functions together
            int mainCount = lines.Count(a => a.StartsWith("void main()"));
            string endMain = "void main() {";
            for (int i = 0; i < mainCount; i++)
            {
                int start = storedContent.LastIndexOf("void main() {") + 13;
                int end = storedContent.LastIndexOf("}");

                endMain += storedContent.Substring(start, end - start);
                storedContent = storedContent.Remove(start - 13, storedContent.Length - (start - 13));
            }

            ShaderData += endMain + "}";

            // Insert StringExtentions
            foreach (KeyValuePair<string, string> pair in StringExtention)
            {
                ShaderData = ShaderData.Replace("//#" + pair.Key, pair.Value);
            }

            return ShaderData;
        }

        /// <summary>
        /// Loading shader into the GPU
        /// </summary>
        /// <param name="programID">The programID</param>
        /// <param name="type">The type</param>
        /// <param name="renderer">The renderer that asked. (If set a debug-file is created)</param>
        internal void Load(int programID, ShaderType type, GenericRenderer renderer)
        {
            if (ID < 0)
            {
                string data = GetData();
                File.WriteAllText($"SHADER_{renderer.GetType().Name}_{type}.txt", data);

                ID = GL.CreateShader(type);
                GL.ShaderSource(ID, data);
                GL.CompileShader(ID);
            }
            GL.AttachShader(programID, ID);
        }
        /// <summary>
        /// Loading shader into the GPU
        /// </summary>
        /// <param name="programID">The programID</param>
        /// <param name="type">The type</param>
        internal void Load(int programID, ShaderType type)
        {
            if (ID < 0)
            {
                string data = GetData();

                ID = GL.CreateShader(type);
                GL.ShaderSource(ID,  data);
                GL.CompileShader(ID);
            }
            GL.AttachShader(programID, ID);
        }
    }
}