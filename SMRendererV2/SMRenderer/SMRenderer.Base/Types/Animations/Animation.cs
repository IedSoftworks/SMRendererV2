using System;
using SMRenderer.Core;

namespace SMRenderer.Base.Types.Animations
{
    public class Animation : Timer
    {
        public Action<Animation, double> TickCalculation;
        public Action<Animation, bool> StartAction;
        public Action<Animation, bool> EndAction;

        public AnimationVector StartValue;
        public AnimationVector Direction { get; }
        public AnimationVector CompleteDirection;

        public AnimatableType Object;

        public Animation(AnimatableType obj, TimeSpan time, AnimationVector movePerSecond, bool repeat = false) : this(
            obj, time, movePerSecond, repeat, AnimationCalculations.LinearCalculation())
        { }

        public Animation(AnimatableType obj, TimeSpan time, AnimationVector movePerSecond, bool repeat,
            Action<Animation, double> tick) : base(time, repeat)
        {
            Object = obj;
            Direction = movePerSecond;
            TickCalculation = tick;

            CompleteDirection = Direction * (float)time.TotalSeconds;
        }

        #region Overrides
        public override void Start(bool repeater = false)
        {
            if (Active) return;

            StartAction?.Invoke(this, repeater);
            base.Start(repeater);
            StartValue = Object;
        }

        public override void Stop(bool userCalled = true)
        {
            if (!Active) return;

            EndAction?.Invoke(this, userCalled);
            base.Stop(userCalled);
        }

        public override void PerformTick(double delta)
        {
            base.PerformTick(delta);

            TickCalculation?.Invoke(this, delta);
        }
        #endregion

        #region Static Methods

        public static Animation CreateValueAnimation(AnimatableType obj, TimeSpan time, AnimationVector start, AnimationVector end,
            Action<Animation, double> tick, bool repeat = false)
        {
            return new Animation(obj, time, (end - start) / (float)time.TotalSeconds, repeat, tick)
            {
                StartAction = (a, b) =>
                {
                    a.Object.X = start.X;
                    a.Object.Y = start.Y;
                    a.Object.Z = start.Z;
                    a.Object.W = start.W;
                },
                EndAction = (a, b) =>
                {
                    a.Object.X = end.X;
                    a.Object.Y = end.Y;
                    a.Object.Z = end.Z;
                    a.Object.W = end.W;
                }
            };
        }

        #endregion
    }
}