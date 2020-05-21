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

        public Animation Animate(TimeSpan time, AnimationVector start, AnimationVector end, bool repeat)
        {
            Animation ani = new Animation(this, time, start, end, repeat);
            ani.Start();
            return ani;
        }
        public Animation Animate(TimeSpan time, AnimationVector direction, bool repeat)
        {
            Animation ani = new Animation(this, time, direction, repeat);
            ani.Start();
            return ani;
        }
    }
}