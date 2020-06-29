using System.Collections.Generic;
using SMRenderer.Core.Models;

namespace SMRenderer.Models.Import
{
    public class ImportedMesh : Mesh
    {
        public Material ImportedMaterial = new Material();

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