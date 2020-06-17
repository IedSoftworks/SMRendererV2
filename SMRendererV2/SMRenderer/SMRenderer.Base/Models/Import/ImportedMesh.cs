using System.Collections.Generic;
using SMRenderer.Base.Types;
using SMRenderer.Core.Models;

namespace SMRenderer.Base.Models.Import
{
    public class ImportedMesh : Mesh
    {
        public Material ImportedMaterial = new Material();
        public ModelPostioning ImportedPositioning;

        public Dictionary<string, ModelData> ImportKeyDataDictionary;

        public ImportedMesh()
        {
            ImportKeyDataDictionary = new Dictionary<string, ModelData>()
            {
                { "POSITION", Vertices },
                { "TEXCOORD_0", UVs },
                { "NORMAL", Normals },
                { "COLOR_0", VertexColors }
            };
        }
    }
}