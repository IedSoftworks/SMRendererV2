using System;

namespace SMRenderer.Models.Import.Enums
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