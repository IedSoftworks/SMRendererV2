using System;
using OpenTK;
using OpenTK.Graphics;
using SMRenderer.Base;
using SMRenderer.Base.Types.Animations;
using SMRenderer.Core;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Window;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer.PostProcessing;
using SMRenderer.PostProcessing.Bloom;
using SMRenderer2D;
using SMRenderer2D.Draw;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            new Log("logs/latest.log").Enable();

            GLWindow window = new GLWindow(new WindowSettings(500, 500), new GLInformation())
                .Use(WindowUsage.All, new Window2D(), new BloomFeature());
            window.Load += (sender, eventArgs) => Test1();
            window.Run();
        }

        static void Test1()
        {
            DrawObject obj = new DrawObject
            {
                Position = new Position(2, 2),
                Size = Size.Uniform2D(20),
                Material =
                {
                    BaseColor = new Color(1,1,1, 1)
                }
            };
            obj.Position.Animate(TimeSpan.FromSeconds(5), obj.Position, AnimationVector.Add(obj.Position, new AnimationVector(500,500)), true);
            
            Animation color1 = new Animation(obj.Material.BaseColor, TimeSpan.FromSeconds(2.5), obj.Material.BaseColor, new AnimationVector(Color4.Red.R, Color4.Red.G, Color4.Red.B));
            Animation color2 = new Animation(obj.Material.BaseColor, TimeSpan.FromSeconds(2.5), new AnimationVector(Color4.Red.R, Color4.Red.G, Color4.Red.B, 1), new AnimationVector(1, 1, 1));

            color1.End += timer => color2.Start();
            color2.End += timer => color1.Start();

            color1.Start();

            Scene.Current.Add(obj);
        
        }
    }
}