using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SMRenderer.Base;
using SMRenderer.Base.Draw;
using SMRenderer.Base.Keybinds;
using SMRenderer.Base.Models;
using SMRenderer.Base.Models.Objects;
using SMRenderer.Base.Scene;
using SMRenderer.Base.Scene.Lights;
using SMRenderer.Base.Types.Extensions;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Window;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer2D;
using SMRenderer3D;
using Color = SMRenderer.Base.Types.VectorTypes.Color;
using Size = SMRenderer.Base.Types.VectorTypes.Size;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Log("logs/latest.log").Enable();

            GLWindow window = new GLWindow(new WindowSettings(500, 500) {VSync = VSyncMode.On}, new GLInformation() {ClearColor = Color4.FromXyz(new Vector4(0.2f, 0.3f, 0.3f, 1.0f)) })
                .Use(WindowUsage.All, new Window3D(45));
            window.Load += (sender, eventArgs) => Test1();
            window.Run();
        }

        static void Test1()
        {
            KeybindCollection.AutoCheckKeybindCollections.Add(new KeybindCollection()
            {
                new Keybind(a => Scene.CurrentCam.Position.X += .1f, Key.W),
                new Keybind(a => Scene.CurrentCam.Position.X -= .1f, Key.S),
                new Keybind(a => Scene.CurrentCam.Position.Z += .1f, Key.A),
                new Keybind(a => Scene.CurrentCam.Position.Z -= .1f, Key.D),
                new Keybind(a => Scene.CurrentCam.Position.Y += .1f, Key.Space),
                new Keybind(a => Scene.CurrentCam.Position.Y -= .1f, Key.ControlLeft),
            });

            Light light = new PhongLight()
            {
                Color = new Color(1, 0, 1),
                Direction = new Vector3(-2, -2, -2),
            };
            /*
            DrawObject groundPlate = new DrawObject
            {
                Position = new Position(5,0,5),
                Size = new Size(50,1,50),
                Rotation = new Rotation(y: 90),
                Mesh = Meshes.Plane
            };*/


            Scene.CurrentCam.Target = new Position(0,0,0);
            light.Position = Scene.CurrentCam.Position = new Position(-2,-2, -2);
            
            Scene.CurrentLight.Add(light);
        
        }
    }
}