using System;

namespace SM.Data.Models.Import
{
    [Flags]
    public enum ModelImportOptions
    {
        None,

        DefaultModelLoadPostProcessing,

        SaveAnimations,
        AbsoluteAnimations,

        SaveMaterial,
        Default = DefaultModelLoadPostProcessing | SaveMaterial,
    }
}