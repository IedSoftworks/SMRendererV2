using System;
using System.Collections.Generic;
using OpenTK.Graphics;
using SMRenderer.Core.Renderer;
using SMRenderer.Core.Renderer.Framebuffers;

namespace SMRenderer.Core.Window
{
    /// <include file='Window.docu' path='Documentation/GLInformation/Class'/>
    public class GLInformation
    {
        /// <include file='Window.docu' path='Documentation/GLInformation/Fields/Field[@name="Renderers"]'/>
        public List<GenericRenderer> Renderers = new List<GenericRenderer>();
        public Color4 ClearColor = Color4.Black;
        public List<Framebuffer> Framebuffers = new List<Framebuffer>();
    }
}