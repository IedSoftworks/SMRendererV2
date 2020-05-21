using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace SMRenderer.Core.Renderer
{
    /// <include file='renderer.docu' path='Documentation/ShaderFileCollection/Class'/>
    public class ShaderFileCollection : List<ShaderFile>
    {
        /// <include file='renderer.docu' path='Documentation/ShaderFileCollection/Fields/Field[@name="InDictionary"]'/>
        public List<string> InDictionary { get; private set; } = new List<string>();
        /// <include file='renderer.docu' path='Documentation/ShaderFileCollection/Fields/Field[@name="OutDictionary"]'/>
        public List<string> OutDictionary { get; private set; } = new List<string>();
        /// <include file='renderer.docu' path='Documentation/ShaderFileCollection/Fields/Field[@name="UniformDictionary"]'/>
        public List<string> UniformDictionary { get; private set; } = new List<string>();

        public ShaderType Type;

        public ShaderFileCollection(ShaderType type)
        {
            Type = type;
        }

        public void Add(string source)
        {
            base.Add(new ShaderFile(Type, source));
        }

        /// <include file='renderer.docu' path='Documentation/ShaderFileCollection/Methods/Method[@name="Load"]'/>
        public void Load(int programId)
        {
            foreach (ShaderFile file in this)
            {
                file.Load(programId);

                InDictionary = InDictionary.Concat(file.InDictionary).ToList();
                OutDictionary = OutDictionary.Concat(file.OutDictionary).ToList();
                UniformDictionary = UniformDictionary.Concat(file.UniformDictionary).ToList();
            }
        }
    }
}