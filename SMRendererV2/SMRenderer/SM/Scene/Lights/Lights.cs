using System.Collections.Generic;
using SM.Core.Renderer;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Lights
{
    public class Lights : List<Light>
    {
        public float Ambient = .03f;

        public void SetUniforms(UniformCollection u)
        {
            u["AmbientLight"]?.SetUniform1(Ambient);
            u["UsedLights"]?.SetUniform1(Count);
            u["ViewPosition"]?.SetUniform3(Scene.CurrentCam.Position);

            for (int i = 0; i < Count; i++)
            {
                this[i].SetUniforms(u, i);
            }
        }
    }
}