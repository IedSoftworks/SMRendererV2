using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Enums;
using SM.Core.Exceptions;
using SM.Core.Renderer.Framebuffers;
using SM.Core.Renderer.Shaders;

namespace SM.Core.Renderer
{
    /// <summary>
    /// Represents a OpenGL renderer
    /// </summary>
    public abstract class GenericRenderer : IGLObject
    {
        /// <inheritdoc />
        public int ID { get; set; } = -1;

        /// <inheritdoc />
        public ObjectLabelIdentifier Identifier { get; set; } = ObjectLabelIdentifier.Program;

        /// <summary>
        /// This contains the vertex file for the shader
        /// </summary>
        public abstract ShaderFile VertexFiles { get; }
        /// <summary>
        /// This contains the fragment file for the shader
        /// </summary>
        public abstract ShaderFile FragmentFiles { get; }

        /// <summary>
        /// This contains all uniforms.
        /// </summary>
        public UniformCollection Uniforms { get; private set; } = new UniformCollection();
        /// <summary>
        /// Shortcut to 'Uniforms'.
        /// </summary>
        public UniformCollection U => Uniforms;

        public virtual Dictionary<string, int> FragData { get; }

        /// <summary>
        /// This compiles the shader together.
        /// </summary>
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

            if (FragData != null)
                foreach (KeyValuePair<string, int> keyValuePair in FragData)
                {
                    GL.BindFragDataLocation(ID, keyValuePair.Value, keyValuePair.Key);
                }

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

        /// <summary>
        /// This cleans up the data after the shader program has been run.
        /// </summary>
        public void CleanUp()
        {
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            U.NextTexture = 0;
        }

        public static implicit operator int(GenericRenderer renderer) => renderer.ID;
    }
}