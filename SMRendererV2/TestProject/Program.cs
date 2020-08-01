using System;
using System.Linq;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using SM;
using SM.Animations;
using SM.Core.Enums;
using SM.Core.Window;
using SM.Core;
using SM.Core.Models;
using SM.Scene.Draw;
using SM.Data.Fonts;
using SM.Keybinds;
using SM.Data.Models;
using SM.Data.Models.Objects;
using SM.PostProcessing.Bloom;
using SM.Scene;
using SM.Scene.Cameras;
using SM.Scene.Lights;
using SM.Data.Types;
using SM.Data.Types.Extensions;
using SM.Data.Types.VectorTypes;
using SM.Scene.Draw.Particles;
using Color = SM.Data.Types.VectorTypes.Color;
using Mouse = SM.Controls.Mouse;
using Size = SM.Data.Types.VectorTypes.Size;

namespace TestProject
{
    class Program
    {
        private static PerspectiveCamera Camera3D;
        private static OrthographicCamera Camera2D;

        private static Font Arial;
        private static DrawObject obj;
        private static DrawText mousePos;
        static void Main(string[] param)
        {
            if (param.Contains("-compileData"))
            {
                DataManage.Compile();
                Environment.Exit(0);
            }

            if (param.Contains("-advdebug")) GLDebug.AdvDebugging = true;

            Arial = new Font(@"C:\Windows\Fonts\Arial.ttf");

            new Log("latest.log").Enable();

            GLWindow window = new GLWindow(new WindowSettings(.9f) {VSync = VSyncMode.Off},
                    new GLInformation() {ClearColor = Color.From255Basis(128,128,128)})
                .Use(WindowUsage.All, new DefaultWindow());
            window.Load += (sender, eventArgs) =>
            {
                Test3D();
                HUD();
            };
            window.Run();
        }

        private static void HUD()
        {
            mousePos = new DrawText(Arial, "< >")
            {
                FontSize = 50,
                Material = {AllowLight = false, DiffuseColor = new Color(1, 0, 0, 1)}
            };
            mousePos.Size.Y = 75;
            Scene.HUD.Add(mousePos);
        }

        private static void Test3D()
        {
            Camera3D = (PerspectiveCamera) Scene.CurrentCam;

            int MoveSpeed = 10;
            int RotateSpeed = 20;
            KeybindCollection.AutoCheckKeybindCollections.Add(new KeybindCollection()
            {
                new Keybind(a => Scene.CurrentCam.Position.X += MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.W),
                new Keybind(a => Scene.CurrentCam.Position.X -= MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.S),
                new Keybind(a => Scene.CurrentCam.Position.Z += MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.A),
                new Keybind(a => Scene.CurrentCam.Position.Z -= MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.D),
                new Keybind(a => Scene.CurrentCam.Position.Y += MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.Space),
                new Keybind(a => Scene.CurrentCam.Position.Y -= MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.ControlLeft),

                new Keybind(a => Camera3D.Rotation.Y += RotateSpeed * (float)SMGlobals.UpdateDeltatime, Key.Up),
                new Keybind(a => Camera3D.Rotation.Y -= RotateSpeed * (float)SMGlobals.UpdateDeltatime, Key.Down),
                new Keybind(a => Camera3D.Rotation.X -= RotateSpeed * (float)SMGlobals.UpdateDeltatime, Key.Left),
                new Keybind(a => Camera3D.Rotation.X += RotateSpeed * (float)SMGlobals.UpdateDeltatime, Key.Right),

                new Keybind(new MouseButton[] {MouseButton.Left}, new Key[] {}, a =>
                {
                    Vector3 pos = Mouse.GetMouseIn3DWorld(Scene.Current, out _);
                    Console.Write("Pause");
                }),

                new Keybind(a => {
                    Console.Write("Pause");
                }, Key.P)
            });

            //Scene.Current.Lights.Ambient = Color.From255Basis(255, 255, 255);
            Scene.Current.AxisHelper = false;

            DrawParticles particles = new DrawParticles(new ConeParticles() {RotateTowardsOrigin = true})
            {
                
            };
            DrawObject obj = new DrawObject()
            {
                Mesh = Meshes.Cylinder,
                Clickable = true,
            };
            Spotlight spotlight = new Spotlight(new Position(20, 1), new Direction(x: -1f, 1f))
            {
                InnerCutoff = 50,
                OuterCutoff = 60
            };

            Scene.CurrentCam.Position = new Position(0,0, 10);
            Camera3D.Target = new Position(0);

            Scene.Current.Lights.Add(spotlight);
            Scene.Current.AddRange(obj);
        }

        static void Test2D()
        {
            Scene.ChangeScene(Scene.Create2DScene());
            Camera2D = (OrthographicCamera)Scene.CurrentCam;

            int MoveSpeed = 30;
            KeybindCollection.AutoCheckKeybindCollections.Add(new KeybindCollection()
            {
                new Keybind(a => Scene.CurrentCam.Position.X += MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.D),
                new Keybind(a => Scene.CurrentCam.Position.X -= MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.A),
                new Keybind(a => Scene.CurrentCam.Position.Y += MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.Space),
                new Keybind(a => Scene.CurrentCam.Position.Y -= MoveSpeed * (float)SMGlobals.UpdateDeltatime, Key.ControlLeft),
                new Keybind(a => {
                    Console.Write("Pause");
                }, Key.P)
            });
            OrthographicCamera.HeightScale = 1000;
            SMGlobals.DefaultModel = Meshes.Plane;

            Scene.Current.Lights.Ambient = Color4.White;

            obj = new DrawObject()
            {
                Size = Size.Uniform2D(50),
                Mesh = AxisHelper.Object
            };

            Scene.Current.AddRange(obj);
        }
    }
}