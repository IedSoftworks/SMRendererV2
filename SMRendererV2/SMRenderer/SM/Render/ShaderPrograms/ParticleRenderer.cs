using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Core.Renderer;
using SM.Core.Renderer.Shaders;
using SM.Render.ShaderFiles;
using SM.Scene.Cameras;
using SM.Scene.Draw;
using SM.Scene.Draw.Particles;
using SM.Utility;

namespace SM.Render.ShaderPrograms
{
    public class ParticleRenderer : GenericRenderer
    {
        public static int DrawCallAmount = 32;

        public override ShaderFile VertexFiles { get; } =
            new ShaderFile(AssemblyUtility.ReadAssemblyFile("Render.ShaderFiles.Particles.particle.vert"));

        public override ShaderFile FragmentFiles { get; } = ShaderCatalog.MainFragmentFile;

        public ParticleRenderer()
        {
            RenderProgramCollection.Particle = this;

            VertexFiles.StringExtention["particleCount"] = DrawCallAmount.ToString();
        }

        public void Draw(Camera cam, DrawParticles particleObject)
        {
            GL.UseProgram(ID);

            Matrix4 world = cam.World;
            Matrix4 view = cam.ViewMatrix;
            Matrix4 model = particleObject.ModelMatrix;

            U["projection"]?.SetMatrix4(ref world);
            U["view"]?.SetMatrix4(ref view);
            U["model"]?.SetMatrix4(ref model);

            U["HasColors"]?.SetUniform1(particleObject.Mesh.VertexColors.HadContent);
            U["Fade"]?.SetUniform1(particleObject.Fade);
            ShaderCatalog.SetMainFragmentUniforms(U, particleObject.Material);

            GL.BindVertexArray(particleObject.Mesh.VAO); 
            int modelLocation = U["Matrices"].Value;
            int i = 0;
            foreach (Particle para in particleObject.ParticleObject.Particles)
            {
                var currentLocationAdd = i % DrawCallAmount;
                if (currentLocationAdd == 0 && i != 0)
                    GL.DrawArraysInstanced(particleObject.Mesh.PrimitiveType, 0, particleObject.Mesh.Vertices.Count, DrawCallAmount);


                Matrix4 matrix = particleObject.MoveAction(particleObject.ParticleObject, para, particleObject);
                GL.UniformMatrix4(modelLocation + currentLocationAdd, false, ref matrix);


                i++;
            }

            GL.DrawArraysInstanced(particleObject.Mesh.PrimitiveType, 0, particleObject.Mesh.Vertices.Count, i);

            particleObject.Material.Modifiers.ForEach(a => a.ClearUniforms(U));
            CleanUp();
            GL.UseProgram(0);
        }
    }
}