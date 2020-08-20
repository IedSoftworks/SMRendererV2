using SM.Core.Renderer;
using SM.Core.Renderer.Shaders;

namespace SM.PostProcessing
{
    public abstract class PostProcess
    {
        public abstract ShaderFile File { get; }

        public virtual void Prepare()
        { }
        public virtual void SetUniforms(UniformCollection uniforms) 
        { }
    }
}