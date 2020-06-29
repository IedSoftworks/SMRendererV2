using System;
using SMRenderer.Utility;

namespace SMRenderer.Animations
{
    public static class AnimationCalculations
    {
        public static float Linear(Animation animation, float delta) => animation.KeyframeTimer / animation.KeyframeTargetTime;

        public static Func<Animation, float, float> Bezier(params float[] controlPoints)
        {
            float[] points = new float[controlPoints.Length + 2];
            points[0] = 0;
            points[controlPoints.Length + 1] = 1;

            for (int i = 0; i < controlPoints.Length; i++)
            {
                points[i + 1] = controlPoints[i];
            }

            return (animation, f) =>
            {
                return BezierCurve.Calculate((float)(animation.KeyframeTimer / animation.KeyframeTargetTime), points);
            };
        }
    }
}