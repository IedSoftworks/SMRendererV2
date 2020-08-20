using System.Collections.Generic;

namespace SM.PostProcessing
{
    public class PostProcessManager
    {
        public static int EnabledPostProcesses => PostProcesses.Count;
        public static List<PostProcess> PostProcesses = new List<PostProcess>();
    }
}