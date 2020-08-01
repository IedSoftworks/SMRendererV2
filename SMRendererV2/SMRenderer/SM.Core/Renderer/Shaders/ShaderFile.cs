using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using GL = OpenTK.Graphics.OpenGL4.GL;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;

namespace SM.Core.Renderer.Shaders
{

    [Serializable]
    public class ShaderFile : IGLObject
    {
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Shader;
        public int ID { get; set; } = -1;

        public List<ShaderFile> Extentions = new List<ShaderFile>();
        public Dictionary<string, string> StringExtention = new Dictionary<string, string>();

        public string FileContent;

        public string ShaderData { get; private set; } = "";

        public ShaderFile(string content)
        {
            FileContent = content;
        }
        public ShaderFile(string content, Dictionary<string, string> overrides)
        {
            FileContent = content;
            StringExtention = overrides;
        }

        public string GetData(bool askedFromShader = false)
        {
            if (ShaderData != "") return ShaderData;

            string storedContent = FileContent;

            // Insert extentions
            foreach (ShaderFile extention in Extentions)
            {
                storedContent = storedContent.Insert(storedContent.IndexOf("void main()"), extention.GetData()+"\r\n");
            }

            // go though the data, remove duplicates and save it. 
            bool structure = false;
            bool hasEndFunction = false;
            int force = 0;
            string[] ignoreLines = new[] {"}", "};"};
            string[] lines = storedContent.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in storedContent.Split(new [] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.StartsWith("void end()")) hasEndFunction = true;

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

            ShaderData += endMain + (askedFromShader && hasEndFunction ? "end();" : "")+ "}";

            // Insert StringExtentions
            foreach (KeyValuePair<string, string> pair in StringExtention)
            {
                ShaderData = ShaderData.Replace("//#" + pair.Key, pair.Value);
            }

            return ShaderData;
        }

        internal void Load(int programID, ShaderType type, GenericRenderer renderer)
        {
            if (ID < 0)
            {
                string data = GetData(true);
                File.WriteAllText($"SHADER_{renderer.GetType().Name}_{type}.txt", data);

                ID = GL.CreateShader(type);
                GL.ShaderSource(ID, data);
                GL.CompileShader(ID);
            }
            GL.AttachShader(programID, ID);
        }

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