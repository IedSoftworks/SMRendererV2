using System;
using SMRenderer.Core;
using SMRenderer2D.Types.ValueStructs;

namespace SMRenderer.Types.Animations
{
    public class Animation : Timer
    {
        public AnimationVector StartValue { get; }
        public AnimationVector EndValue { get; }
        public AnimationVector Direction { get; }
        public bool IsDirectional { get; }

        public AnimatableType Object;

        public Animation(AnimatableType obj, TimeSpan time, AnimationVector start, AnimationVector end) : this(obj, time, start, end, false) { }

        public Animation(AnimatableType obj, TimeSpan time, AnimationVector start, AnimationVector end, bool repeat) : base(time, repeat)
        {
            StartValue = start;
            EndValue = end;

            Direction = AnimationVector.Sub(EndValue, StartValue);

            Object = obj;
        }

        public Animation(AnimatableType obj, TimeSpan time, AnimationVector direction) : this(obj, time, direction, false) { }

        public Animation(AnimatableType obj, TimeSpan time, AnimationVector direction, bool repeat) : base(time, repeat)
        {
            Direction = direction;
            IsDirectional = true;

            Object = obj;
        }

        public override void Start(bool repeater = false)
        {
            if (!IsDirectional)
            {
                Object.X = StartValue.X;
                Object.Y = StartValue.Y;
                Object.Z = StartValue.Z;
                Object.W = StartValue.W;
            }

            base.Start(repeater);
        }

        public override void Stop(bool userCalled = true)
        {
            if (!userCalled && !IsDirectional)
            {
                Object.X = EndValue.X;
                Object.Y = EndValue.Y;
                Object.Z = EndValue.Z;
                Object.W = EndValue.W;
            }
            base.Stop(userCalled);
        }

        public override void PerformTick(double delta)
        {
            double per = delta / TargetTime;

            Object.X += Direction.X * (float)(IsDirectional ? delta : per);
            Object.Y += Direction.Y * (float)(IsDirectional ? delta : per);
            Object.Z += Direction.Z * (float)(IsDirectional ? delta : per);
            Object.W += Direction.W * (float)(IsDirectional ? delta : per);

            base.PerformTick(delta);
        }
    }
}