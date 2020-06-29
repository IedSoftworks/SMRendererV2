using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Renderer.Framebuffers;

namespace SMRenderer.Core.Renderer
{
    /// <include file='renderer.docu' path='Documentation/GenericRenderer/Class'/>
    public abstract class GenericRenderer : IGLObject
    {
        public static Dictionary<string, int> DefaultFragData;

        public int ID { get; set; } = -1;

        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Program;

        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="VertexFiles"]'/>
        public abstract ShaderFileCollection VertexFiles { get; }
        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="FragmentFiles"]'/>
        public abstract ShaderFileCollection FragmentFiles { get; }

        public virtual Dictionary<string, int> CustomFragData { get; } = DefaultFragData;


        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="AttribIDs"]'/>
        public static Dictionary<string, int> AttribIDs = new Dictionary<string, int>();

        public UniformCollection Uniforms { get; private set; } = new UniformCollection();
        public UniformCollection U => Uniforms;

        internal virtual void Compile()
        {
            Log.Write(LogWriteType.Info, "Loading render program '" + GetType().Name + "'");

            // Create ID
            ID = GL.CreateProgram();
            GLDebug.Name(this, GetType().Name);

            // Load all files in the ID
            if (VertexFiles == null || FragmentFiles == null)
                throw new ShaderLoadingException("[General] No Vertex or Fragment files found.");

            VertexFiles.Load(ID);
            FragmentFiles.Load(ID);

            if (VertexFiles.Any(a => a.ID == -1) && FragmentFiles.Any(a => a.ID == -1))
                throw new ShaderLoadingException("[General] Not all of your shaders has been loaded correctly.\n\nRenderer: " + GetType().Name);

            GL.LinkProgram(ID);

            VertexFiles.ForEach(a =>
            {
                GL.DetachShader(ID, a.ID);
                if (a.Individual) GL.DeleteShader(a.ID);
            });
            FragmentFiles.ForEach(a =>
            {
                GL.DetachShader(ID, a.ID);
                if (a.Individual) GL.DeleteShader(a.ID);
            });

            GL.GetProgram(ID, GetProgramParameterName.ActiveUniforms, out var uniformAmount);

            for (int i = 0; i < uniformAmount; i++)
            {
                var key = GL.GetActiveUniform(ID, i, out _, out _);
                var location = GL.GetUniformLocation(ID, key);

                key = key.Split('[')[0];
                Uniforms.Add(key, new Uniform(location));
            }

            GLDebug.CheckGLErrors();
        }

        public void CleanUp()
        {
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public static implicit operator int(GenericRenderer renderer) => renderer.ID;
    }
}