using System;
using System.Collections.Generic;
using System.Linq;

namespace SMRenderer.Types.Animations
{
    public struct AnimationStruct
    {
        public static Dictionary<AnimationStruct, float> currentAnimations = new Dictionary<AnimationStruct, float>(); 

        public float Time;
        public event Action<AnimationStruct, bool> Ending;

        private AnimationVector Start;
        private AnimationVector End;
        private AnimationVector Direction;
        private AnimatableType Object;

        private bool isDirectional;
        public bool Repeat;

        public AnimationStruct(TimeSpan time, AnimationVector start, AnimationVector end, bool repeat = false)
        {
            Time = (float)time.TotalSeconds;
            Start = start;
            End = end;
            Object = null;
            Repeat = repeat;
            Ending = null;

            Direction = AnimationVector.Sub(end, start);

            isDirectional = false;
        }

        public AnimationStruct(TimeSpan time, AnimationVector direction, bool repeat = false)
        {
            Time = (float)time.TotalSeconds;
            Object = null;
            End = default;
            Start = default;
            Direction = direction;
            Repeat = repeat;
            Ending = null;

            isDirectional = true;
        }
        private void SetValue(AnimationVector av)
        {
            Object.X = av.X;
            Object.Y = av.Y;
            Object.Z = av.Z;
            Object.W = av.W;
        }

        internal void SetObject(AnimatableType obj) => Object = obj;

        public void StartAnimation(bool repeating = false)
        {
            if (!repeating) currentAnimations.Add(this, 0);
            else currentAnimations[this] = 0;
            if (!isDirectional) SetValue(Start);
        }

        public void TickAnimation(float deltaTime, float currentTime)
        {
            float per = currentTime / Time;
            float deltaPer = deltaTime / Time;

            if (isDirectional)
            {
                Object.X += Direction.X / deltaTime;
                Object.Y += Direction.Y / deltaTime;
                Object.Z += Direction.Z / deltaTime;
                Object.W += Direction.W / deltaTime;
            }
            else
            {
                Object.X += Direction.X * deltaPer;
                Object.Y += Direction.Y * deltaPer;
                Object.Z += Direction.Z * deltaPer;
                Object.W += Direction.W * deltaPer;
            }

            if (per >= 1) Stop(false);
        }

        internal void Stop(bool userCalled)
        {
            if (!isDirectional) SetValue(End);

            if (Repeat && !userCalled) StartAnimation(true);
            else currentAnimations.Remove(this);

            Ending?.Invoke(this, Repeat);
        }
        public void StopAnimation()
        {
            Stop(true);
        }

        public static void Tick(float delta)
        {
            foreach (KeyValuePair<AnimationStruct, float> value in currentAnimations.ToList())
            {
                currentAnimations[value.Key] += delta;
                value.Key.TickAnimation(delta, currentAnimations[value.Key]);
            }
        }
    }
}