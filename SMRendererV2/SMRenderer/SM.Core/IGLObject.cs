using OpenTK.Graphics.OpenGL4;

namespace SM.Core
{
    /// <summary>
    /// This represens a OpenGL object.
    /// </summary>
    public interface IGLObject
    {
        /// <summary>
        /// The ID of the OpenGL object
        /// </summary>
        int ID { get; set; }
        /// <summary>
        /// The specification what exactly the object is.
        /// </summary>
        ObjectLabelIdentifier Identifier { get; set; }
    }
}