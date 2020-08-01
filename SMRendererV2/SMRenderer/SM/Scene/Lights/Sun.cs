using OpenTK;
using SM.Core.Renderer;

namespace SM.Scene.Lights
{
    public class Sun : Light
    {
        public override LightType Type { get; set; } = LightType.Sun;

        public Vector3 Direction;

        public Sun(Vector3 direction)
        {
            Direction = direction;
        }

        public override void SetUniforms(UniformCollection u, int index)
        {
            base.SetUniforms(u, index);

            u[$"Lights[{index}].Sun.Direction"]?.SetUniform3(Direction);
        }
    }
}