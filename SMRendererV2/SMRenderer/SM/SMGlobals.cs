using System;
using OpenTK.Input;
using SM.Core.Models;
using SM.Data.Models;
using SM.Utility;

namespace SM
{
    public class SMGlobals
    {
        public const int MAX_PARTICLES = 512;
        public const int MAX_DRAW_PARAMETER = 32;
        public const int MAX_LIGHTS = 4;

        public static Model DefaultModel { get; set; } = Meshes.Cube1;
        public static int DefaultFontSize { get; set; } = 1;

        public static Deltatime MasterDeltatime = new Deltatime();

        public static ulong CurrentFrame { get; internal set; } = 0;
        public static ulong CurrentFrameStack { get; internal set; } = 0;


        public static KeyboardState CurrentKeyState { get; internal set; }
        public static MouseState CurrentMouseState { get; internal set; }

        internal static void Setup() {}
    }
}