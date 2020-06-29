using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SMRenderer.Animations;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Window;
using SMRenderer.Core;
using SMRenderer.Core.Models;
using SMRenderer.Draw;
using SMRenderer.Keybinds;
using SMRenderer.Models;
using SMRenderer.PostProcessing.Bloom;
using SMRenderer.Scene;
using SMRenderer.Scene.Cameras;
using SMRenderer.Scene.Lights;
using SMRenderer.Types.Extensions;
using SMRenderer.Types.VectorTypes;
using SMRenderer.Window;
using Color = SMRenderer.Types.VectorTypes.Color;

namespace TestProject
{
    class Program
    {
        static void Main()
        {
            KeybindCollection.AutoCheckKeybindCollections.Add(new KeybindCollection()
            {
                new Keybind(a => Scene.CurrentCam.Position.X += .1f, Key.W),
                new Keybind(a => Scene.CurrentCam.Position.X -= .1f, Key.S),
                new Keybind(a => Scene.CurrentCam.Position.Z += .1f, Key.A),
                new Keybind(a => Scene.CurrentCam.Position.Z -= .1f, Key.D),
                new Keybind(a => Scene.CurrentCam.Position.Y += .1f, Key.Space),
                new Keybind(a => Scene.CurrentCam.Position.Y -= .1f, Key.ControlLeft),
                new Keybind(a => {
                    Console.Write("Pause");
                }, Key.P)
            });

            //new Log("latest.log").Enable();

            GLWindow window = new GLWindow(new WindowSettings(500, 500) {VSync = VSyncMode.On},
                new GLInformation() {ClearColor = Color4.FromXyz(new Vector4(0.2f, 0.3f, 0.3f, 1.0f))})
                .Use(WindowUsage.All, new DefaultWindow());
            window.Load += (sender, eventArgs) => Test1();
            window.Run();
        }

        static void Test1()
        {
            float i;
            int row = 32;
            int column = 32;
            Mesh mesh = new Mesh()
            {
                PrimitiveType = PrimitiveType.Lines
            };
            for (i = 1; i < row; i++)
            {
                mesh.Vertices.Add(0, i / row, 0);
                mesh.Vertices.Add(1, i / row, 0);
                mesh.VertexColors.AddRange(new ModelValue(1,0,0,1), new ModelValue(1, 0, 0, 1));
            }

            for (i = 1; i < column; i++)
            {
                mesh.Vertices.Add(i / column, 0, 0);
                mesh.Vertices.Add(i / column, 1, 0);

                mesh.VertexColors.AddRange(new ModelValue(0, 0, 1, 1), new ModelValue(0, 0, 1, 1));
            }

            CallParameter parameter = new CallParameter()
            {
                Size = new Size(5)
            };

            DrawCall obj = new DrawCall()
            {
                Mesh = mesh,
                
            };
            obj.DrawCallParameters.Add(parameter);

            DrawObject obj2 = new DrawObject()
            {
                Position = new Position(2.5f, 2.5f, 2.5f),
                Size = new Size(2)
            };
            obj2.Material.DiffuseColor = Color4.Black;

            Light light = new Light(LightType.Point, obj2.Position);
            Scene.Current.Lights.Add(light);
            Scene.Current.Lights.Ambient = Color4.Brown;

            Scene.CurrentCam.Position = new Position(0,3,5);
            ((Camera3D)Scene.CurrentCam).Target = new Position(2.5f, 2.5f, 0);
            ((Camera3D) Scene.CurrentCam).UseTarget = true;
            Scene.Current.AddRange(obj, obj2);
        }
    }
}