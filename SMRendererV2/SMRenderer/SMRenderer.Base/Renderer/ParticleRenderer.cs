using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Base.Draw;
using SMRenderer.Base.Shaders;
using SMRenderer.Core;
using SMRenderer.Core.Renderer;

namespace SMRenderer.Base.Renderer
{
    public class ParticleRenderer : GenericRenderer
    {
        public static ParticleRenderer program;

        public override ShaderFileCollection VertexFiles { get; } = new ShaderFileCollection(ShaderType.VertexShader)
        {
            { Utility.ReadAssemblyFile("Shaders.Particles.main.vert") }
        };

        public override ShaderFileCollection FragmentFiles { get; } = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            ShaderCatalog.MainFragment,
            ShaderCatalog.Lights
        };

        public ParticleRenderer()
        {
            program = this;
        }

        public void Draw(Matrix4 model, DrawParticle particleObject)
        {
            GL.UseProgram(mProgramId);

            ShaderCatalog.MainVertex.SetUniforms(U, new[] { (object)model });
            ShaderCatalog.MainFragment.SetUniforms(U, new[] { (object)particleObject.Mesh, particleObject.Material });

            U["Motions"].SetUniform3(particleObject.Amount, particleObject.ShaderFloats);
            U["Time"].SetUniform1(particleObject.CurrentTime);
            U["Fade"].SetUniform1(particleObject.CurrentFade);

            GL.BindVertexArray(particleObject.Mesh.VAO);
            GL.DrawArraysInstanced(particleObject.Mesh.PrimitiveType, 0, particleObject.Mesh.VertexCount, particleObject.Amount);

            CleanUp();
            GL.UseProgram(0);
        }
    }
}