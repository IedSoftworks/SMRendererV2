using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Models;

namespace SMRenderer.Core.Renderer
{
    /// <include file='renderer.docu' path='Documentation/ShaderFile/Class'/>
    [Serializable]
    public class ShaderFile : IGLObject
    {
        public string Source;
        public ShaderType Type;
        public ShaderSourceExt SourceExt = new ShaderSourceExt();

        public int ID { get; set; } = -1;
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Shader;

        public bool Individual;
        public Action<Dictionary<string, Uniform>, object[]> SetUniforms = (uniforms, objects) => { };

        [NonSerialized] public List<string> InDictionary = new List<string>();
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
                foreach (KeyValuePair<string, List<string>> keyValuePair in SourceExt)
                {
                    string check = "//#"+keyValuePair.Key;

                    Source = Source.Replace(check, String.Join("\n", keyValuePair.Value));
                }
            
                ID = GL.CreateShader(Type);
                GL.ShaderSource(ID, Source);
                GL.CompileShader(ID);

                Source = null;
            }
            GL.AttachShader(programID, ID);
        }
    }
}