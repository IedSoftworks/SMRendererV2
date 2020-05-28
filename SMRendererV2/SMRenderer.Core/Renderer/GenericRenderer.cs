using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Renderer.Framebuffers;

namespace SMRenderer.Core.Renderer
{
    /// <include file='renderer.docu' path='Documentation/GenericRenderer/Class'/>
    public abstract class GenericRenderer
    {
        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="mProgramId"]'/>
        public int mProgramId { get; private set; } = -1;

        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="VertexFiles"]'/>
        public abstract ShaderFileCollection VertexFiles { get; }
        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="FragmentFiles"]'/>
        public abstract ShaderFileCollection FragmentFiles { get; }

        public virtual Dictionary<string, int> CustomFragData { get; }


        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="AttribIDs"]'/>
        public static Dictionary<string, int> AttribIDs = new Dictionary<string, int>();

        public Dictionary<string, Uniform> Uniforms { get; private set; } = new Dictionary<string, Uniform>();
        public Dictionary<string, Uniform> U => Uniforms;

        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Constructor'/>
        public GenericRenderer()
        {
            Log.Write(LogWriteType.Info, "Loading render program '"+GetType().Name+"'");

            // Create ID
            mProgramId = GL.CreateProgram();

            // Load all files in the ID
            if (VertexFiles == null || FragmentFiles == null)
                throw new ShaderLoadingException("[General] No Vertex or Fragment files found.");

            VertexFiles.Load(mProgramId);
            FragmentFiles.Load(mProgramId);

            if (VertexFiles.Any(a => a.ID == -1) && FragmentFiles.Any(a => a.ID == -1))
                throw new ShaderLoadingException("[General] Not all of your shaders has been loaded correctly.\n\nRenderer: "+GetType().Name);

            foreach (string inValue in VertexFiles.InDictionary)
            {
                if (!AttribIDs.ContainsKey(inValue))
                    throw new ShaderLoadingException("[General] There is no id found for attribute '" + inValue + "'. To use the attribute add it to GenericRenderer.AttribIDs.");

                int id = AttribIDs[inValue];
                GL.BindAttribLocation(mProgramId, id, inValue);
            }
            foreach (string outValue in FragmentFiles.OutDictionary)
            {
                if (CustomFragData != null)
                    if (CustomFragData?.ContainsKey(outValue) == true) GL.BindFragDataLocation(mProgramId, CustomFragData[outValue], outValue);
                
                else
                {
                    ColorAttachment id = Framebuffer.ActiveFramebuffer.ColorAttachments.FirstOrDefault(a =>
                        a.FragOutputVariable == outValue);
                    if (id != null)
                        GL.BindFragDataLocation(mProgramId, id, outValue);
                    else
                        Log.Write(LogWriteType.Warning,
                            "Fragment out variable '" + outValue +
                            "' doesn't exist in current framebuffer. Currently ignored.");
                }
            }

            GL.LinkProgram(mProgramId);

            VertexFiles.ForEach(a =>
            {
                GL.DetachShader(mProgramId, a.ID);
                if (a.Individual) GL.DeleteShader(a.ID);
            });
            FragmentFiles.ForEach(a =>
            {
                GL.DetachShader(mProgramId, a.ID);
                if (a.Individual) GL.DeleteShader(a.ID);
            });

            GL.GetProgram(mProgramId, GetProgramParameterName.ActiveUniforms, out var uniformAmount);

            for (int i = 0; i < uniformAmount; i++)
            {
                var key = GL.GetActiveUniform(mProgramId, i, out _, out _);
                var location = GL.GetUniformLocation(mProgramId, key);

                Uniforms.Add(key, new Uniform(location));
            }

            Utility.CheckGLErrors();
        }

        public void CleanUp()
        {
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public static implicit operator int(GenericRenderer renderer) => renderer.mProgramId;
    }
}