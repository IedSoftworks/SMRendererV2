using System.Collections.Generic;
using OpenTK.Graphics;
using SM.Core.Renderer;
using SM.Core.Renderer.Framebuffers;

namespace SM.Core.Window
{
    public class GLInformation
    {
        public List<GenericRenderer> Renderers = new List<GenericRenderer>();
        public Color4 ClearColor = Color4.Black;
        public List<Framebuffer> Framebuffers = new List<Framebuffer>();
    }
}