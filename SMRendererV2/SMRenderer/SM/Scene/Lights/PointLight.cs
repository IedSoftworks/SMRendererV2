using SM.Core.Renderer;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Lights
{
    public class PointLight : Light
    {
        public override LightType Type { get; set; } = LightType.Point;

        public Position Position;

        public float Constant = 1f;
        public float Linear = 0.7f;
        public float Quadratic = 1.8f;

        public PointLight(Position position)
        {
            Position = position;
        }

        public override void SetUniforms(UniformCollection u, int index)
        {
            base.SetUniforms(u, index);

            u[$"Lights[{index}].Point.Position"]?.SetUniform3(Position);
            u[$"Lights[{index}].Point.Constant"]?.SetUniform1(Constant);
            u[$"Lights[{index}].Point.Linear"]?.SetUniform1(Linear);
            u[$"Lights[{index}].Point.Quadratic"]?.SetUniform1(Quadratic);
        }
    }
}