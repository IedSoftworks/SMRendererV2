using System;
using System.Collections.Generic;
using SM.Core.Models;

namespace SM.Data.Models.Import
{
    [Serializable]
    public class ImportedMesh : Mesh
    {
        public Material ImportedMaterial = new Material();
    }
}