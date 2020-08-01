using System;
using OpenTK;
using SM.Core.Renderer;
using SM.Data.Types.VectorTypes;

namespace SM.Scene.Lights
{
    public class Spotlight : Light
    {
        public override LightType Type { get; set; } = LightType.Spotlight;

        public Position Position;
        public Direction Direction;

        public float OuterCutoff = 15;
        public float InnerCutoff = 12.5f;

        public Spotlight(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        public override void SetUniforms(UniformCollection u, int index)
        {
            base.SetUniforms(u, index);

            u[$"Lights[{index}].Spotlight.Position"]?.SetUniform3(Position);
            u[$"Lights[{index}].Spotlight.Direction"]?.SetUniform3(Direction);
            u[$"Lights[{index}].Spotlight.OuterCutoff"]?.SetUniform1((float)Math.Cos(MathHelper.DegreesToRadians(OuterCutoff)));
            u[$"Lights[{index}].Spotlight.InnerCutoff"]?.SetUniform1((float)Math.Cos(MathHelper.DegreesToRadians(InnerCutoff)));
        }
    }
}