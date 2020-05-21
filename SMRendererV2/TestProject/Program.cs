using System;
using OpenTK.Graphics;
using SMRenderer.Base;
using SMRenderer.Base.Types.Animations;
using SMRenderer.Core;
using SMRenderer.Core.Enums;
using SMRenderer.Core.Window;
using SMRenderer.Base.Types.VectorTypes;
using SMRenderer2D;
using SMRenderer2D.Visual.Draw;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            new Log("logs/last.log", "logs").Enable();

            Timer timer = new Timer(1, true);
            timer.End += timer2 => Log.Write(LogWriteType.Info, "Timer 1 repeats.");
            timer.Start();

            GLWindow window = new GLWindow(new WindowSettings(500, 500), new GLInformation()).Use(typeof(Window2D), WindowUsage.All);
            window.Load += (sender, eventArgs) => Test1();
            window.Run();
        }

        static void Test1()
        {
            DrawObject start = new DrawObject()
            {
                Position = new Position(50, 50),
                Size = Size.Uniform2D(50)
            };
            DrawObject end = new DrawObject()
            {
                Position = new Position(50, 450),
                Size = Size.Uniform2D(50)
            };

            DrawObject obj = new DrawObject()
            {
                Position = new Position(50, 50),
                Size = Size.Uniform2D(25),
                Material = { BaseColor = Color4.Red }
            };
            obj.Position.Animate(TimeSpan.FromSeconds(5), start.Position, new AnimationVector(450,450), true);

            Scene.Current.AddRange(start, end, obj);
        }
    }
}
