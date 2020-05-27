namespace SMRenderer.Base.Scene
{
    public class Scene : SMItemCollection
    {
        public static Scene Current { get; private set; } = new Scene();
        public static SMItemCollection HUD = new SMItemCollection();
        public static Camera CurrentCam => Current.Camera;
        public static LightOptions CurrentLight => Current.Light;

        public SMItemCollection Background = new SMItemCollection();

        public Camera Camera = new Camera();

        public LightOptions Light = new LightOptions();

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