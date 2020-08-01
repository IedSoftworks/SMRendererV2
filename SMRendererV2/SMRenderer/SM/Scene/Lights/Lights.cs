using System.Collections.Generic;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Lights
{
    public class Lights : List<Light>
    {
        public Color Ambient = Color.From255Basis(10,10,10);
    }
}