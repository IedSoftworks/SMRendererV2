using System;
using OpenTK;
using SMRenderer.Core;
using SMRenderer.Core.Enums;

namespace SMRenderer.Base.Types.Animations
{
    public class AnimationCalculations
    {
        public static Action<Animation, double> LinearCalculation()
        {
            return (a, d) =>
            {
                a.Object.X += a.Direction.X * (float)d;
                a.Object.Y += a.Direction.Y * (float) d;
                a.Object.Z += a.Direction.Z * (float) d;
                a.Object.W += a.Direction.W * (float) d;
            };
        }

        public static Action<Animation, double> BezierCalculation(float multiplier = 1f) => BezierCalculation(0f, multiplier);
        public static Action<Animation, double> BezierCalculation(float point1, float? point2 = null)
        {
            return (a, d) =>
            {
                double per = a.CurrentTime / a.TargetTime;
                double t = per;

                double y;
                if (point2.HasValue)
                {
                    y = Math.Pow(1 - t, 3) * 0 +
                               3 * Math.Pow(1 - t, 2) * t * point1 +
                               3 * (1 - t) * Math.Pow(t, 2) * point2.GetValueOrDefault() +
                               Math.Pow(t, 3) * 1;
                }
                else
                {
                    y = 2 * (1 - t) * t * point1 +
                        Math.Pow(t, 2);
                }

                Log.Write("Testing", per+" - " +y, LogWriteTarget.LogFile);

                AnimationVector vec = a.StartValue + a.CompleteDirection * (float) y;
                a.Object.Set(vec);
            }; 
        }
    }
}