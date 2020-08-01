using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Enums;
using SM.Core.Exceptions;
using SM.Core.Renderer.Shaders;

namespace SM.Core.Renderer
{
    public abstract class GenericRenderer : IGLObject
    {
        public static Dictionary<string, int> DefaultFragData;

        public int ID { get; set; } = -1;

        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Program;

        public abstract ShaderFile VertexFiles { get; }
        public abstract ShaderFile FragmentFiles { get; }

        public virtual Dictionary<string, int> CustomFragData { get; } = DefaultFragData;


        public static Dictionary<string, int> AttribIDs = new Dictionary<string, int>();

        public UniformCollection Uniforms { get; private set; } = new UniformCollection();
        public UniformCollection U => Uniforms;

        internal virtual void Compile()
        {
            Uniforms.renderer = this;
            Log.Write(LogWriteType.Info, "Loading render program '" + GetType().Name + "'");

            // Create ID
            ID = GL.CreateProgram();

            // Load all files in the ID
            if (VertexFiles == null || FragmentFiles == null)
                throw new ShaderLoadingException("[General] No Vertex or Fragment files found.");

            VertexFiles.Load(ID, ShaderType.VertexShader);
            FragmentFiles.Load(ID, ShaderType.FragmentShader);

            if (VertexFiles.ID < 0 && FragmentFiles.ID < 0)
                throw new ShaderLoadingException("[General] Not all of your shaders has been loaded correctly.\n\nRenderer: " + GetType().Name);

            GL.LinkProgram(ID);
            GLDebug.Name(this, GetType().Name);

            GL.DetachShader(ID, VertexFiles.ID);
            GL.DetachShader(ID, FragmentFiles.ID);

            GL.GetProgram(ID, GetProgramParameterName.ActiveUniforms, out var uniformAmount);

            for (int i = 0; i < uniformAmount; i++)
            {
                var key = GL.GetActiveUniform(ID, i, out _, out ActiveUniformType type);
                var location = GL.GetUniformLocation(ID, key);

                if (key.EndsWith("]")) key = key.Split('[')[0];
                Uniforms.Add(key, location, type);
            }

            Log.Write(LogWriteType.Info, "Used uniforms: " + uniformAmount);

            GLDebug.CheckGLErrors();

            RendererCollection.Add(this);
        }

        public void CleanUp()
        {
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            U.NextTexture = 0;

        }

        public static implicit operator int(GenericRenderer renderer) => renderer.ID;
    }
}