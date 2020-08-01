using System;
using SM.Animations;

namespace SM.Data.Types
{
    [Serializable]
    public abstract class AnimatableType : VectorType
    {
        public AnimationStruct Animate(TimeSpan time, bool repeat, params AnimationVector[] values)
        {
            AnimationStruct ani = new AnimationStruct(time, values);
            Animation animation = new Animation(this, ani, repeat);
            animation.Start();
            return ani;
        }

        public void Animate(AnimationStruct animationStruct, bool repeat = false)
        {
            new Animation(this, animationStruct, repeat).Start();
        }

        public void Set(AnimationVector vec) => Set(vec.X, vec.Y, vec.Z, vec.W);
        public void Set(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public void Set(VectorType vec) => Set(vec.X, vec.Y, vec.Z, vec.W);
    }
}