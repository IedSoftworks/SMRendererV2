namespace SMRenderer.Animations
{
    public struct Keyframe
    {
        public AnimationVector Value;
        public float Time;

        public Keyframe(AnimationVector value, float time)
        {
            Value = value;
            Time = time;
        }
    }
}