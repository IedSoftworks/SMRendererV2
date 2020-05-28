using SMRenderer.Base.Draw;
using SMRenderer.Base.Types.VectorTypes;

namespace SMRenderer.Base.Scene
{
    public class Scene : SMItemCollection
    {
        public static Scene Current { get; private set; } = new Scene();
        public static SMItemCollection HUD = new SMItemCollection();
        public static Camera CurrentCam => Current.Camera;
        public static DrawObject AxisHelper = new DrawObject()
        {
            Size = new Size(5),
            Mesh = Models.Objects.AxisHelper.Object
        };

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
            if (AxisHelper != null)
            {
                AxisHelper.Prepare();
                AxisHelper.Draw();
            }

            Background.Prepare();
            Background.Draw();

            this.Prepare();
            this.Draw();

            HUD.Prepare();
            HUD.Draw();
        }
    }
}