using OpenTK.Graphics.OpenGL4;
using SM.Scene.Cameras;
using SM.Scene.Draw;

namespace SM.Scene
{
    public class Scene : SMItemCollection
    {
        public static Scene Current { get; private set; } = new Scene();
        public static SMItemCollection HUD = new SMItemCollection()
        {
            Camera = new OrthographicCamera(),
            DepthFunc = DepthFunction.Always
        };
        public static Camera CurrentCam => Current.Camera;
        public static DrawObject StaticAxisHelper = new DrawObject(true)
        {
            Mesh = Data.Models.Objects.AxisHelper.Object,
            Material = { AllowLight = false },
        };


        public IShowObject Background;
        public Lights.Lights Lights = new Lights.Lights();

        public float AxisHelperSize = 5;

        public bool AxisHelper = true;
        public bool ShowHUD = true;

        public Scene()
        {
            Camera = new PerspectiveCamera();
            DepthFunc = DepthFunction.Less;
        }

        public static void ChangeScene(Scene nextScene)
        {
            Current = nextScene;
            StaticAxisHelper.Size.X = StaticAxisHelper.Size.Y = StaticAxisHelper.Size.Z = nextScene.AxisHelperSize;
            StaticAxisHelper.Prepare(0);
        }

        public void DrawAll(double delta)
        {
            if (Background != null)
            {
                Background.Prepare(delta);
                Background.Draw(Camera);
            }

            this.Prepare(delta);
            this.Draw(Camera);

            if (ShowHUD && HUD != null)
            {
                HUD.Prepare(delta);
                HUD.Draw(Camera);
            }


            if (AxisHelper) StaticAxisHelper.Draw(Camera);
        }

        public void AddRange(params IShowObject[] objects)
        {
            foreach (IShowObject showObject in objects)
            {
                Add(showObject);
            }
        }

        public static Scene Create2DScene()
        {
            Scene scene = new Scene
            {
                Camera = new OrthographicCamera(),
                DepthFunc = DepthFunction.Always,
                AxisHelperSize = 50
            };
            return scene;
        }
    }
}