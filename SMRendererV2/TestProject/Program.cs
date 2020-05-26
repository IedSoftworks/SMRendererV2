using System;
using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Base;
using SMRenderer.Base.Types.Animations;
using SMRenderer.Base.Types.Extensions;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Window;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.PostProcessing.Bloom;
using SMRenderer3D.Draw;
using SMRenderer3D;
using SMRenderer3D.Objects;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Log("logs/latest.log").Enable();

            GLWindow window = new GLWindow(new WindowSettings(500, 500) {VSync = VSyncMode.Off}, new GLInformation() {ClearColor = Color4.FromXyz(new Vector4(0.2f, 0.3f, 0.3f, 1.0f)) })
                .Use(WindowUsage.All, new Window3D());
            window.Load += (sender, eventArgs) => Test1();
            window.Run();
        }

        static void Test1()
        {
            DrawObject groundPlate = new DrawObject
            {
                Position = new Position(5,0,5),
                Size = new Size(50,1,50),
                Rotation = new Rotation(y: 90),
                Mesh = SMPlate.Object,
                Material =
                {
                    BaseColor = new Color(1,1,1),
                }
            };

            DrawObject obj = new DrawObject
            {
                Position = new Position(0, 1, 0),
                Size = new Size(2),
                Material =
                {
                    BaseColor = new Color(1,1,1)
                }
            };

            obj.Position.Animate(TimeSpan.FromSeconds(5), obj.Position, new AnimationVector(20,2,0), true);

            Animation color1 = new Animation(obj.Material.BaseColor, TimeSpan.FromSeconds(2.5), obj.Material.BaseColor, new AnimationVector(Color4.Red.R, Color4.Red.G, Color4.Red.B));
            Animation color2 = new Animation(obj.Material.BaseColor, TimeSpan.FromSeconds(2.5), new AnimationVector(Color4.Red.R, Color4.Red.G, Color4.Red.B, 1), new AnimationVector(1, 1, 1));

            color1.End += timer => color2.Start();
            color2.End += timer => color1.Start();

            color1.Start();

            Scene.CurCam.Target = obj.Position;
            Scene.CurCam.Position = new Position(0,2, 5);

            Scene.Current.AddRange(groundPlate, obj);
        
        }
    }
}