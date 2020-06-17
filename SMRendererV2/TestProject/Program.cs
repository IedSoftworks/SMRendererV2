using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SMRenderer.Base;
using SMRenderer.Base.Draw;
using SMRenderer.Base.Keybinds;
using SMRenderer.Base.Models;
using SMRenderer.Base.Models.Import;
using SMRenderer.Base.Scene;
using SMRenderer.Base.Scene.Lights;
using SMRenderer.Base.Types.Animations;
using SMRenderer.Base.Types.Extensions;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Window;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.Core;
using SMRenderer.PostProcessing.Bloom;
using Color = SMRenderer.Base.Types.VectorTypes.Color;

namespace TestProject
{
    class Program
    {
        static void Main()
        {

            new Log("latest.log").Enable();

            GLWindow window = new GLWindow(new WindowSettings(500, 500) {VSync = VSyncMode.On},
                new GLInformation() {ClearColor = Color4.FromXyz(new Vector4(0.2f, 0.3f, 0.3f, 1.0f))})
                .Use(WindowUsage.All, new DefaultWindow());
            window.Load += (sender, eventArgs) => Test1();
            window.Run();
        }

        static void Test1()
        {
            DrawObject collection = new DrawObject()
            {
                Mesh = Meshes.Torus,
                Size = new Size(1),
            };
            Animation ani = Animation.CreateValueAnimation(collection.Position, TimeSpan.FromSeconds(3), collection.Position, new AnimationVector(-2.5f), AnimationCalculations.BezierCalculation());

            DrawParticle particle = new DrawParticle()
            {
                Amount = 256, Mesh = Meshes.Sphere, Speed = 25, ObjectSize = new Size(.2f), PrettyRemove = true
            };

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
                }, Key.P),
                new Keybind(a => ani.Start(), Key.X)
            });

            Scene.CurrentCam.Position = new Position(0,1,10);
            Scene.Current.AddRange(collection, particle);
            Scene.Current.Remove(particle);
        }
    }
}