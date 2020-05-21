namespace SMRenderer.Base
{
    public class Scene : SMItemCollection
    {
        public static Scene Current { get; private set; } = new Scene();
        public static SMItemCollection HUD = new SMItemCollection();
        public static Camera CurCam => Current.Camera;

        public SMItemCollection Background = new SMItemCollection();

        public Camera Camera = new Camera();

        public static void ChangeScene(Scene nextScene)
        {
            Current = nextScene;
        }

        public new void Sort()
        {
            this.Order();
            HUD.Order();
            Background.Order();
        }

        public void DrawAll()
        {
            Background.Prepare();
            Background.Draw();

            this.Prepare();
            this.Draw();

            HUD.Prepare();
            HUD.Draw();
        }
    }
}