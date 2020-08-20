using OpenTK;

namespace SM.Core.Window
{
    /// <summary>
    /// This stores informations and methods for Aspect ratios.
    /// </summary>
    public class Aspect
    {
        /// <summary>
        /// The Aspect-Ratio
        /// </summary>
        public float Ratio = 0;
        /// <summary>
        /// The original resolution
        /// </summary>
        public Vector2 OriginalResolution = Vector2.Zero;
        /// <summary>
        /// The scaled resolution
        /// </summary>
        public Vector2 ScaledResolution = Vector2.Zero;

        /// <summary>
        /// Creates new aspect with a pre defined ratio.
        /// </summary>
        /// <param name="ratio">Pre-defined ratio</param>
        public Aspect(float ratio = 0)
        {
            Ratio = ratio;
        }
        /// <summary>
        /// Creates new aspect by using the original width and height
        /// </summary>
        /// <param name="width">Original width</param>
        /// <param name="height">Original height</param>
        public Aspect(int width, int height)
        {
            Recalculate(width, height);
        }

        /// <summary>
        /// Recalculates the aspect ratio.
        /// </summary>
        /// <param name="width">The original width</param>
        /// <param name="height">The original height</param>
        public void Recalculate(int width, int height)
        {
            OriginalResolution = new Vector2(width, height);
            Ratio = CalculateRatio(width, height);
        }

        /// <summary>
        /// Gets the width, with a specific height
        /// </summary>
        /// <param name="height">The height</param>
        /// <returns>The calculated width</returns>
        public int GetWidth(int height) => CalculateWidth(Ratio, height);
        /// <summary>
        /// Calculates the width by scaling the original width down.
        /// </summary>
        /// <param name="scale">Scale from 0 - 1</param>
        /// <returns>The rescaled width</returns>
        public int GetWidth(float scale) => (int)(OriginalResolution.X * scale);
        /// <summary>
        /// Gets the height, with a specific width
        /// </summary>
        /// <param name="width">The width</param>
        /// <returns>The calculated height</returns>
        public int GetHeight(int width) => CalculateHeight(Ratio, width);
        /// <summary>
        /// Calculates the height by scaling the original height down.
        /// </summary>
        /// <param name="scale">Scale from 0 - 1</param>
        /// <returns>The rescaled height</returns>
        public int GetHeight(float scale) => (int)(OriginalResolution.Y * scale);

        /// <summary>
        /// This calculates the ratio from a width and a height
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Aspect ratio</returns>
        public static float CalculateRatio(int width, int height) => (float) width / height;

        /// <summary>
        /// Calculates the width based on the ratio.
        /// </summary>
        /// <param name="ratio"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int CalculateWidth(float ratio, int height) => (int)(height * ratio);
        /// <summary>
        /// Calculates the height based on the ratio.
        /// </summary>
        /// <param name="ratio"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static int CalculateHeight(float ratio, int width) => (int)(ratio / width);
        
    }
}