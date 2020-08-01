using System;
using OpenTK;
using SM.Core.Models;
using SM.Scene.Cameras;

namespace SM.Helpers
{
    public class RayHelper
    {
        public static bool TestOBBRayIntersection(Vector3 ray_origin, Vector3 ray_direction, Model model, Matrix4 modelMatrix, out float distance)
        {
            float tMin = -1;
            float tMax = PerspectiveCamera.FarClippingPlane;
            distance = 0;

            Vector3 obbPos_world = modelMatrix.Column3.Xyz;
            Vector3 delta = obbPos_world - ray_origin;

            for (int i = 0; i < 3; i++)
            {
                Vector3 axis = modelMatrix.Column0.Xyz;
                switch (i)
                {
                    case 1:
                        axis = modelMatrix.Column1.Xyz;
                        break;
                    case 2:
                        axis = modelMatrix.Column2.Xyz;
                        break;
                }

                float e = Vector3.Dot(axis, delta);
                float f = Vector3.Dot(ray_direction, axis);

                if (Math.Abs(f) > 0.001f)
                {
                    float t1 = (e + model.OBB_Min[i]) / f;
                    float t2 = (e + model.OBB_Max[i]) / f;

                    if (t1 > t2)
                    {
                        float w = t1;
                        t1 = t2;
                        t2 = w;
                    }

                    if (t2 < tMax) tMax = t2;
                    if (t1 < tMin) tMin = t1;

                    if (tMax > tMin) return false;
                }
                else if (-e + model.OBB_Min[i] > 0.0f || -e + model.OBB_Max[i] < 0.0f) return false;
            }

            if (tMin == -1) return false;

            distance = tMin;
            return true;
        }
    }
}