using System;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM.Core.Window;
using SM.Helpers;
using SM.Scene;
using SM.Scene.Cameras;
using SM.Scene.Draw;
using SM.Scene.Draw.Base;

namespace SM.Controls
{
    public class Mouse
    {
        public static Vector2 MouseInScreen { get; private set; }
        public static Vector2 MouseInScreenNormalized { get; private set; }

        public static Vector2 GetMouseIn2DWorld(Camera cam)
        {
            Aspect aspect = GLWindow.Window.Aspect;
            Vector2 res = aspect.ScaledResolution;

            return MouseInScreenNormalized * (res * 2) - res + cam.Position;
        }
        public static Vector3 GetMouseIn3DWorld(SMItemCollection objects, out IShowObject selectedObject)
        {
            selectedObject = null;
            Vector3 distanceVector = Vector3.Zero;

            Vector4 rayOrigin = new Vector4(MouseInScreenNormalized) {Z = -1, W = 1};
            Vector4 rayEnd = new Vector4(MouseInScreenNormalized) {W = 1, Z = 0};

            Matrix4 M = objects.Camera.WorldViewProjection.Inverted();
            Vector4 rayOriginWorld = M * rayOrigin; rayOriginWorld /= rayOriginWorld.W;
            Vector4 rayEndWorld = M * rayEnd; rayEndWorld /= rayEndWorld.W;

            Vector3 dir = new Vector3(rayEndWorld - rayOriginWorld);
            dir.Normalize();

            Vector3 origin = new Vector3(rayOriginWorld);

            foreach (IShowObject obj in objects.GetAllObjects())
            {
                if (!obj.GetType().IsSubclassOf(typeof(MatrixObject))) continue;

                MatrixObject matrixObj = (MatrixObject) obj;
                if (!matrixObj.Clickable) continue;

                if (RayHelper.TestOBBRayIntersection(origin, dir, matrixObj.Mesh,
                    matrixObj.ModelMatrix, out float distance))
                {
                    selectedObject = obj;
                    distanceVector = dir * distance;
                    break;
                }
            }

            return distanceVector;
        }

        

        internal static void Window_MouseMove(object obj, MouseMoveEventArgs mmea)
        {
            GLWindow window = (GLWindow) obj;

            MouseInScreen = new Vector2(mmea.X, mmea.Y);
            MouseInScreenNormalized = new Vector2((mmea.X / (float)window.Width - 0.5f) * 2, (mmea.Y / (float)window.Height - 0.5f) * 2);
        }
    }
}