using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Core.Renderer;
using SMRenderer.Draw;
using SMRenderer.Shaders;
using SMRenderer.Utility;

namespace SMRenderer.Renderer
{
    public class ParticleRenderer : GenericRenderer
    {
        public static ParticleRenderer program;

        public override ShaderFileCollection VertexFiles { get; } = new ShaderFileCollection(ShaderType.VertexShader)
        {
            { AssemblyUtility.ReadAssemblyFile("Shaders.Particles.particle.vert") }
        };

        public override ShaderFileCollection FragmentFiles { get; } = new ShaderFileCollection(ShaderType.FragmentShader)
        {
            ShaderCatalog.MainFragment
        };

        public ParticleRenderer()
        {
            VertexFiles[0].SourceExt["particleCount"].Add(SMRenderer.MAX_PARTICLES.ToString());
            program = this;
        }

        public void Draw(Matrix4 model, DrawParticle particleObject)
        {
            GL.UseProgram(ID);

            ShaderCatalog.SetMainVertexUniforms(U, ref model, particleObject.Mesh);
            ShaderCatalog.SetMainFragmentUniforms(U, particleObject.Material);

            U["Motions"]?.SetUniform3(particleObject.Amount, particleObject.ShaderFloats);
            U["Time"]?.SetUniform1(particleObject.CurrentTime);
            U["Fade"]?.SetUniform1(particleObject.CurrentFade);

            GL.BindVertexArray(particleObject.Mesh.VAO);
            GL.DrawArraysInstanced(particleObject.Mesh.PrimitiveType, 0, particleObject.Mesh.VertexCount, particleObject.Amount);

            CleanUp();
            GL.UseProgram(0);
        }
    }
}