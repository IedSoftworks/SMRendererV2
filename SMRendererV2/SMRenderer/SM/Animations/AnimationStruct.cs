using System;

namespace SM.Animations
{
    public struct AnimationStruct
    {
        public static Func<Animation, float, float> DefaultCalculationFunction = AnimationCalculations.Linear;
        public static bool DefaultRelative = false;

        public Keyframe[] Keyframes { get; }
        public int KeyframeAmount { get; }
        public float Time;
        public Func<Animation, float, float> CalculationFunction;
        public bool RelativeValues;

        public AnimationStruct(TimeSpan time, params AnimationVector[] values) : this((float)time.TotalSeconds, DefaultRelative, DefaultCalculationFunction, values) { }
        public AnimationStruct(TimeSpan time, params Keyframe[] keyframes) : this((float)time.TotalSeconds, DefaultRelative, DefaultCalculationFunction, keyframes) { }
        public AnimationStruct(TimeSpan time, Func<Animation, float, float> calculationFunction, params AnimationVector[] values) : this((float)time.TotalSeconds, DefaultRelative, calculationFunction, values) { }
        public AnimationStruct(TimeSpan time, Func<Animation, float, float> calculationFunction, params Keyframe[] values) : this((float)time.TotalSeconds, DefaultRelative, calculationFunction, values) { }
        
        public AnimationStruct(TimeSpan time, bool relativeValues, params AnimationVector[] values) : this((float)time.TotalSeconds, relativeValues, DefaultCalculationFunction, values) { }
        public AnimationStruct(TimeSpan time, bool relativeValues, params Keyframe[] keyframes) : this((float)time.TotalSeconds, relativeValues, DefaultCalculationFunction, keyframes) { }
        public AnimationStruct(TimeSpan time, bool relativeValues, Func<Animation, float, float> calculationFunction, params AnimationVector[] values) : this((float)time.TotalSeconds, relativeValues, calculationFunction, values) { }
        public AnimationStruct(TimeSpan time, bool relativeValues, Func<Animation, float, float> calculationFunction, params Keyframe[] values) : this((float)time.TotalSeconds, relativeValues, calculationFunction, values) { }

        public AnimationStruct(float time, params AnimationVector[] values) : this(time, DefaultRelative, DefaultCalculationFunction, values) {}
        public AnimationStruct(float time, params Keyframe[] keyframes) : this(time, DefaultRelative, DefaultCalculationFunction, keyframes) {}
        public AnimationStruct(float time, bool relativeValues, params AnimationVector[] values) : this(time, relativeValues, DefaultCalculationFunction, values) {}
        public AnimationStruct(float time, bool relativeValues, params Keyframe[] keyframes) : this(time, relativeValues, DefaultCalculationFunction, keyframes) {}

        public AnimationStruct(float time, bool relativeValues, Func<Animation, float, float> calculationFunction, params AnimationVector[] keyframes)
        {
            Time = time;
            CalculationFunction = calculationFunction;
            RelativeValues = relativeValues;
            KeyframeAmount = keyframes.Length;
            Keyframes = new Keyframe[keyframes.Length];

            float timeBetween = time / (keyframes.Length - 1);
            float currentTime = 0;
            for(int i = 0; i < keyframes.Length; i++, currentTime += timeBetween)
                Keyframes[i] = new Keyframe(keyframes[i], currentTime);
        }
        public AnimationStruct(float time, bool relativeValues, Func<Animation, float, float> calculationFunction, params Keyframe[] keyframes)
        {
            Time = time;
            CalculationFunction = calculationFunction;
            RelativeValues = relativeValues;
            Keyframes = keyframes;
            KeyframeAmount = keyframes.Length;
        }
    }
}