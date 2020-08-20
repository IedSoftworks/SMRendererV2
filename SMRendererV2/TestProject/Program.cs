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
using SM.Scene;
using SM.Scene.Cameras;
using SM.Scene.Lights;
using SM.Data.Types;
using SM.Data.Types.Extensions;
using SM.Data.Types.VectorTypes;
using SM.Scene.Draw.Particles;
using Color = SM.Data.Types.VectorTypes.Color;
using Mouse = SM.Controls.Mouse;

namespace TestProject
{
    class Program
    {
        private static PerspectiveCamera Camera3D;
        private static OrthographicCamera Camera2D;

        private static Font Arial;
        private static DrawObject obj;
        private static DrawText mousePos;
        [STAThread]
        static void Main(string[] param)
        {
            if (param.Contains("-compileData"))
            {
                DataManage.Compile();
                Environment.Exit(0);
            }

            if (param.Contains("-advdebug")) GLDebug.AdvDebugging = true;

            Log.UseOwnCrashEvent = true;

            Arial = new Font(@"C:\Windows\Fonts\Arial.ttf");

            new Log("latest.log").Enable();

            GLWindow window = new GLWindow(new WindowSettings(.9f) {VSync = VSyncMode.Off})
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
                Material = {AllowLight = false, Color = new Color(1, 0, 0, 1)}
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
                new Keybind(a => Scene.CurrentCam.Position.X += MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.W),
                new Keybind(a => Scene.CurrentCam.Position.X -= MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.S),
                new Keybind(a => Scene.CurrentCam.Position.Z += MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.A),
                new Keybind(a => Scene.CurrentCam.Position.Z -= MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.D),
                new Keybind(a => Scene.CurrentCam.Position.Y += MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.Space),
                new Keybind(a => Scene.CurrentCam.Position.Y -= MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.ControlLeft),

                new Keybind(a => Camera3D.Rotation.Y += RotateSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.Up),
                new Keybind(a => Camera3D.Rotation.Y -= RotateSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.Down),
                new Keybind(a => Camera3D.Rotation.X -= RotateSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.Left),
                new Keybind(a => Camera3D.Rotation.X += RotateSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.Right),

                new Keybind(a => {
                    Console.Write("Pause");
                }, Key.P)
            });

            Scene.Current.Lights.Ambient = 1;

            DrawObject obj = new DrawObject()
            {
                Mesh = Meshes.Cylinder,
                Material = {AllowLight = false}
            };
            Spotlight spotlight = new Spotlight(new Vector(20, 1), new Vector(x: -1f, 1f))
            {
                InnerCutoff = 50,
                OuterCutoff = 60
            };

            Scene.CurrentCam.Position = new Vector(0,0, 5);
            Camera3D.Target = new Vector(0);

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
                new Keybind(a => Scene.CurrentCam.Position.X += MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.D),
                new Keybind(a => Scene.CurrentCam.Position.X -= MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.A),
                new Keybind(a => Scene.CurrentCam.Position.Y += MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.Space),
                new Keybind(a => Scene.CurrentCam.Position.Y -= MoveSpeed * SMGlobals.MasterDeltatime.DeltaTime, Key.ControlLeft),
                new Keybind(a => {
                    Console.Write("Pause");
                }, Key.P)
            });
            OrthographicCamera.HeightScale = 1000;
            SMGlobals.DefaultModel = Meshes.Plane;

            Scene.Current.Lights.Ambient = 1;

            obj = new DrawObject()
            {
                Size = new Vector(50, 50),
                Mesh = Meshes.Plane,
                Rotation = new Vector(90,0,0)
            };

            Scene.Current.AddRange(obj);
        }
    }
}