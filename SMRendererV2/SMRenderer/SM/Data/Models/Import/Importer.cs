using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Assimp;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;

namespace SM.Data.Models.Import
{
    public class Importer
    {
        static readonly Dictionary<Assimp.PrimitiveType, PrimitiveType> primitiveTypes = new Dictionary<Assimp.PrimitiveType, PrimitiveType>()
        {
            {Assimp.PrimitiveType.Line, PrimitiveType.Lines},
            {Assimp.PrimitiveType.Point, PrimitiveType.Points},
            {Assimp.PrimitiveType.Triangle, PrimitiveType.Triangles},
            {Assimp.PrimitiveType.Polygon, PrimitiveType.Triangles}
        };

        static readonly AssimpContext importer = new AssimpContext();

        public static ImportedMesh[] ImportMeshes(string path, bool relative = true, PostProcessSteps postProcess = PostProcessSteps.None)
        {
            if (relative) path = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), path);

            if (postProcess == PostProcessSteps.None)
            {
                postProcess = PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate |
                              PostProcessSteps.CalculateTangentSpace;
            }

            return ImportMeshes(importer.ImportFile(path, postProcess));
        }

        public static ImportedMesh[] ImportMeshes(Stream stream, PostProcessSteps postProcess = PostProcessSteps.None)
        {
            if (postProcess == PostProcessSteps.None)
            {
                postProcess = PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate | 
                              PostProcessSteps.CalculateTangentSpace;
            }

            return ImportMeshes(importer.ImportFileFromStream(stream, postProcess));
        }

        public static ImportedMesh[] ImportMeshes(Assimp.Scene root)
        {
            ImportedMesh[] meshes = new ImportedMesh[root.MeshCount];
            for (int i = 0; i < root.MeshCount; i++)
            {
                ImportedMesh currentMesh = meshes[i] = new ImportedMesh();
                Assimp.Mesh mesh = root.Meshes[i];

                currentMesh.Name = mesh.Name;
                currentMesh.PrimitiveType = primitiveTypes[mesh.PrimitiveType];
                if (mesh.HasVertices)
                {
                    mesh.Vertices.ForEach(vector => currentMesh.Vertices.Add(vector.X, vector.Y, vector.Z));
                }

                if (mesh.HasNormals)
                {
                    mesh.Normals.ForEach(vector => currentMesh.Normals.Add(vector.X, vector.Y, vector.Z));
                }

                if (mesh.HasTextureCoords(0))
                {
                    mesh.TextureCoordinateChannels[0].ForEach(vector => currentMesh.UVs.Add(vector.X, vector.Y));
                }

                if (mesh.HasVertexColors(0))
                {
                    mesh.VertexColorChannels[0].ForEach(vector =>
                        currentMesh.VertexColors.Add(vector.R, vector.G, vector.B, vector.A));
                }

                if (mesh.HasTangentBasis)
                {
                    mesh.Tangents.ForEach(vector => currentMesh.Tangents.Add(vector.X, vector.Y, vector.Z));
                }

                currentMesh.Indices = mesh.GetIndices();
            }

            return meshes;
        }
    }
}