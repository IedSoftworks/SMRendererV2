using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Exceptions;
using SMRenderer.Core.Object;
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


        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="AttribIDs"]'/>
        public static Dictionary<string, int> AttribIDs = new Dictionary<string, int>();
        /// <include file='renderer.docu' path='Documentation/GenericRenderer/Fields/Field[@name="FragDataIDs"]'/>
        public static Dictionary<string, int> FragDataIDs = new Dictionary<string, int>();

        public Dictionary<string, int> Uniforms { get; private set; } = new Dictionary<string, int>();
        public Dictionary<string, int> U => Uniforms;

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

            if (VertexFiles.All(a => a.ID != -1) && FragmentFiles.All(a => a.ID != -1))
            {
                foreach (string inValue in VertexFiles.InDictionary)
                {
                    if (!AttribIDs.ContainsKey(inValue))
                        throw new ShaderLoadingException("[General] There is no id found for attribute '"+inValue+"'. Do use the attribute add it to GenericRenderer.AttribIDs.");

                    int id = AttribIDs[inValue];
                    GL.BindAttribLocation(mProgramId, id, inValue);
                }
                foreach (string outValue in FragmentFiles.OutDictionary)
                {
                    ColorAttachment id = Framebuffer.ActiveFramebuffer.ColorAttachments.FirstOrDefault(a =>
                        a.FragOutputVariable == outValue);
                    if (id != null)
                    {
                        GL.BindFragDataLocation(mProgramId, id, outValue);
                    } else
                    {
                        Log.Write(LogWriteType.Warning, "Fragment out variable '"+outValue+"' doesn't exist in current framebuffer '"+Framebuffer.ActiveFramebuffer.GetType().Name+"'. Currently ignored.");
                    }
                }

                GL.LinkProgram(mProgramId);
            } else
            {
                throw new ShaderLoadingException("[General] Not all of your shaders has been loaded correctly.\n\nRenderer: "+GetType().Name);
            }

            foreach (string key in VertexFiles.UniformDictionary)
            {
                if (Uniforms.ContainsKey(key))
                    throw new ShaderLoadingException("[Uniforms] There are multiple uniforms with the name '"+key+"'.");

                Uniforms.Add(key, GL.GetUniformLocation(mProgramId, key));
            }
            foreach (string key in FragmentFiles.UniformDictionary)
            {
                if (Uniforms.ContainsKey(key))
                    throw new ShaderLoadingException("[Uniforms] There are multiple uniforms with the name '"+key+ "'.\n\nRenderer: " + GetType().Name);

                Uniforms.Add(key, GL.GetUniformLocation(mProgramId, key));
            }
        }

        public abstract void Draw(Matrix4 MVP, Model model, Material material);

        public void CleanUp()
        {
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}