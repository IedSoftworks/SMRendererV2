using System;
using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Core.Window;

namespace SMRenderer.Core.Window
{
    /// <include file='window.docu' path='Documentation/GLWindow/Class'/>
    public class GLWindow : GameWindow
    {
        private WindowSettings settings;

        /// <include file='window.docu' path='Documentation/GLWindow/Constructor'/>
        public GLWindow(WindowSettings settings)
        {
            Setup();
        }

        /// <include file='window.docu' path='Documentation/GLWindow/Method[@name="Setup"]'/>
        public void Setup()
        { }

        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }
    }
}