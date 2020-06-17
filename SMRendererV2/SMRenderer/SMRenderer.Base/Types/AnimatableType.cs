using System;
using SMRenderer.Base.Types.Animations;

namespace SMRenderer.Base.Types
{
    public abstract class AnimatableType : VectorType
    {
        public override float X { get; set; }
        public override float Y { get; set; }
        public override float Z { get; set; }
        public override float W { get; set; }

        public Animation Animate(TimeSpan time, AnimationVector end, bool repeat = false) => Animate(time, this, end, repeat);
        public Animation Animate(TimeSpan time, AnimationVector end, Action<Animation, double> tick, bool repeat = false) => Animate(time, this, end, tick, repeat);

        public Animation Animate(TimeSpan time, AnimationVector start, AnimationVector end, bool repeat = false) =>
            Animate(time, start, end, AnimationCalculations.LinearCalculation(), repeat);
        public Animation Animate(TimeSpan time, AnimationVector start, AnimationVector end, Action<Animation, double> tick, bool repeat = false)
        {
            Animation ani = Animation.CreateValueAnimation(this, time, start, end, tick, repeat);
            ani.Start();
            return ani;
        }
        public Animation AnimateDirectional(TimeSpan time, AnimationVector direction, bool repeat = false)
        {
            Animation ani = new Animation(this, time, direction, repeat);
            ani.Start();
            return ani;
        }

        public void Set(AnimationVector vec) => Set(vec.X, vec.Y, vec.Z, vec.W);
        public void Set(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}