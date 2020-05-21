namespace SMRenderer.Base.Interfaces
{
    public interface IShowObject
    {
        float RenderPosition { get; set; }

        void Prepare();
        void Draw();
        void Add(SMItemCollection collection);
        void Remove(SMItemCollection collection);
    }
}