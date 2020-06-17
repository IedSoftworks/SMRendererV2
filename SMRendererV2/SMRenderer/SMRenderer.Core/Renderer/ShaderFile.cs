using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Models;

namespace SMRenderer.Core.Renderer
{
    /// <include file='renderer.docu' path='Documentation/ShaderFile/Class'/>
    [Serializable]
    public class ShaderFile
    {
        /// <include file='renderer.docu' path='Documentation/ShaderFile/Fields/Field[@name="Source"]'/>
        public string Source;

        /// <include file='renderer.docu' path='Documentation/ShaderFile/Fields/Field[@name="Type"]'/>
        public ShaderType Type;

        /// <include file='renderer.docu' path='Documentation/ShaderFile/Fields/Field[@name="ID"]'/>
        public int ID { get; private set; } = -1;

        public bool Individual;
        public Action<Dictionary<string, Uniform>, object[]> SetUniforms = (uniforms, objects) => { };

        /// <include file='renderer.docu' path='Documentation/ShaderFile/Fields/Field[@name="InDictionary"]'/>
        [NonSerialized] public List<string> InDictionary = new List<string>();
        /// <include file='renderer.docu' path='Documentation/ShaderFile/Fields/Field[@name="OutDictionary"]'/>
        [NonSerialized] public List<string> OutDictionary = new List<string>();

        /// <include file='renderer.docu' path='Documentation/ShaderFile/Constructor'/>
        public ShaderFile(ShaderType type, string source, bool individual = true)
        {
            Type = type;
            Source = source;
            Individual = individual;
        }

        /// <include file='renderer.docu' path='Documentation/ShaderFile/Methods/Method[@name="Load"]'/>
        public void Load(int programID)
        {
            if (ID == -1)
            {
                ID = GL.CreateShader(Type);
                GL.ShaderSource(ID, Source);
                GL.CompileShader(ID);
            }
            GL.AttachShader(programID, ID);

            if (Source == null) return;

            foreach (string line in Source.Split('\n'))
            {
                string[] splits = line.Split(';')[0].Split(' ');

                switch (splits[0])
                {
                    case "in":
                        InDictionary.Add(splits[2]);
                        break;

                    case "out":
                        OutDictionary.Add(splits[2]);
                        break;
                }
            }

            Source = null;
        }
    }
}