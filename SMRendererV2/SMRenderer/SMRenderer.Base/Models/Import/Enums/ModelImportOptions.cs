using System;

namespace SMRenderer.Base.Models.Import.Enums
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