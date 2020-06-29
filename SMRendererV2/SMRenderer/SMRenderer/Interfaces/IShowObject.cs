using SMRenderer.Draw;

namespace SMRenderer.Interfaces
{
    public interface IShowObject
    {
        SMItemCollection Parent { get; set; }
        float RenderPosition { get; set; }

        void Prepare(double delta);
        void Draw(double delta);
        bool AddTrigger(SMItemCollection collection);
        bool RemoveTrigger(SMItemCollection collection);
    }
}