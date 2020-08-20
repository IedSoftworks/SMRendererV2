using System;

namespace SM.Utility
{
    public class Randomize
    {
        public static Random Randomizer = new Random();

        public static void SetSeed(int seed) => Randomizer = new Random(seed);

        public static int GetInt() => Randomizer.Next();
        public static int GetInt(int max) => Randomizer.Next(max);
        public static int GetInt(int min, int max) => Randomizer.Next(min, max);

        public static float GetFloat() => (float)Randomizer.NextDouble();
        public static float GetFloat(float max) => (float)Randomizer.NextDouble() * max;
        public static float GetFloat(float min, float max) => (float)Randomizer.NextDouble() * max + min;

    }
}