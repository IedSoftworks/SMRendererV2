using SMRenderer.Base.Draw;
using SMRenderer.Base.Interfaces;
using SMRenderer.Base.Scene.Cameras;
using SMRenderer.Base.Types.VectorTypes;

namespace SMRenderer.Base.Scene
{
    public class Scene : SMItemCollection
    {
        public static Scene Current { get; private set; } = new Scene();
        public static IShowObject HUD;
        public static Camera CurrentCam => Current.Camera;
        static readonly DrawObject StaticAxisHelper = new DrawObject(true)
        {
            Size = new Size(5),
            Mesh = Models.Objects.AxisHelper.Object
        };

        public static LightOptions CurrentLight => Current.Light;

        public IShowObject Background;
        public Camera Camera = new Camera3D();
        public LightOptions Light = new LightOptions();

        public bool AxisHelper = true;
        public bool ShowHUD = true;

        public static void ChangeScene(Scene nextScene)
        {
            Current = nextScene;
        }

        public void DrawAll(double delta)
        {
            if (AxisHelper) StaticAxisHelper.Draw(delta);

            if (Background != null)
            {
                Background.Prepare(delta);
                Background.Draw(delta);
            }

            this.Prepare(delta);
            this.Draw(delta);

            if (!ShowHUD || HUD == null) return;
            HUD.Prepare(delta);
            HUD.Draw(delta);
        }

        public void AddRange(params IShowObject[] objects)
        {
            foreach (IShowObject showObject in objects)
            {
                Add(showObject);
            }
        }
    }
}