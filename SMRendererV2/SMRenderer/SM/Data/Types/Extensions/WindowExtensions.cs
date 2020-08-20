using SM.Core.Exceptions;
using SM.Core.Window;
using SM.PostProcessing;
using SM.Render.ShaderPrograms;

namespace SM.Data.Types.Extensions
{
    public static class WindowExtensions
    {
        public static void AddPostProcessing(this GLWindow window, PostProcessing.PostProcess postProcess)
        {
            PostProcessManager.PostProcesses.Add(postProcess);
            PostProcessingRenderer.MergeFragment.Extensions.Add(postProcess.File);

            window.Reload();
        }

        public static void RemovePostProcessing(this GLWindow window, PostProcess postProcess)
        {
            if (!PostProcessManager.PostProcesses.Contains(postProcess)) 
                throw new LogException("You tried to remove a not existed post processing effect.");

            PostProcessManager.PostProcesses.Remove(postProcess);
            PostProcessingRenderer.MergeFragment.Extensions.Remove(postProcess.File);

            window.Reload();
        }
    }
}