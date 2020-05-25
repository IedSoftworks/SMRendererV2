using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using SMRenderer.Core.Renderer.Framebuffers;

namespace SMRenderer.Core.Window
{
    /// <include file='window.docu' path='Documentation/GLInformation/Class'/>
    public class GLInformation
    {
        /// <include file='window.docu' path='Documentation/GLInformation/Fields/Field[@name="Renderers"]'/>
        public List<Type> Renderers = new List<Type>();
        public Color4 ClearColor = Color4.Black;
        public List<Framebuffer> Framebuffers = new List<Framebuffer>();
    }
}