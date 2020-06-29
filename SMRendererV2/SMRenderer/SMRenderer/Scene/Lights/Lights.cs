using System.Collections.Generic;
using OpenTK.Graphics;
using SMRenderer.Types.VectorTypes;

namespace SMRenderer.Scene.Lights
{
    public class Lights : List<Light>
    {
        public Color Ambient = Color4.White;
    }
}