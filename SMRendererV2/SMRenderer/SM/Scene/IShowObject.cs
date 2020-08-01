using OpenTK.Graphics.OpenGL4;
using SM.Scene.Cameras;
using SM.Scene.Draw;

namespace SM.Scene
{
    public interface IShowObject
    {
        bool Collection { get; set; }
        SMItemCollection Parent { get; set; }
        float RenderPosition { get; set; }
        DepthFunction DepthFunc { get; set; }

        void Update(double delta);
        void Prepare(double delta);
        void Draw(Camera camera);
        bool AddTrigger(SMItemCollection collection);
        bool RemoveTrigger(SMItemCollection collection);
    }
}