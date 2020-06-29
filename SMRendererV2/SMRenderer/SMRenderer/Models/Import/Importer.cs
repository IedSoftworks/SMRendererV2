using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Assimp;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using SMRenderer.Models.Import.Enums;
using Material = SMRenderer.Core.Models.Material;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;
using TextureWrapMode = OpenTK.Graphics.OpenGL4.TextureWrapMode;

namespace SMRenderer.Models.Import
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

        public static ImportedMesh[] ImportMeshes(string path, bool relative = true,
            ModelImportOptions options = ModelImportOptions.Default, PostProcessSteps postProcess = PostProcessSteps.None)
        {
            if (relative) path = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), path);

            if ((options & ModelImportOptions.DefaultModelLoadPostProcessing) != 0 && postProcess == PostProcessSteps.None)
            {
                postProcess = PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate |
                              PostProcessSteps.JoinIdenticalVertices;
            }

            return ImportMeshes(importer.ImportFile(path, postProcess), options);
        }

        public static ImportedMesh[] ImportMeshes(Stream stream,
            ModelImportOptions options = ModelImportOptions.Default, PostProcessSteps postProcess = PostProcessSteps.None)
        {
            if ((options & ModelImportOptions.DefaultModelLoadPostProcessing) != 0 && postProcess == PostProcessSteps.None)
            {
                postProcess = PostProcessSteps.GenerateUVCoords | PostProcessSteps.Triangulate |
                              PostProcessSteps.JoinIdenticalVertices;
            }

            return ImportMeshes(importer.ImportFileFromStream(stream, postProcess), options);
        }

        public static ImportedMesh[] ImportMeshes(Assimp.Scene root, ModelImportOptions options)
        {
            Dictionary<int, Material> materials = new Dictionary<int, Material>();
            if ((options & ModelImportOptions.SaveMaterial) != 0)
            {
                for (int i = 0; i < root.MaterialCount; i++)
                {
                    Material material = new Material();
                    materials.Add(i, material);

                    Assimp.Material mat = root.Materials[i];

                    if (mat.HasColorDiffuse) material.DiffuseColor = new Color4(mat.ColorDiffuse.R, mat.ColorDiffuse.G, mat.ColorDiffuse.B, mat.ColorDiffuse.A);
                    if (mat.HasTextureDiffuse) material.DiffuseTexture = new Texture(new Bitmap(mat.TextureDiffuse.FilePath), TextureMinFilter.Linear, TextureWrapMode.ClampToEdge);
                }
            }

            ImportedMesh[] meshes = new ImportedMesh[root.MeshCount];
            for (int i = 0; i < root.MeshCount; i++)
            {
                ImportedMesh currentMesh = meshes[i] = new ImportedMesh();
                Assimp.Mesh mesh = root.Meshes[i];

                currentMesh.Name = mesh.Name;
                currentMesh.PrimitiveType = primitiveTypes[mesh.PrimitiveType];

                foreach (int faceIndex in mesh.Faces.Where(face => face.HasIndices).SelectMany(face => face.Indices))
                {
                    if (mesh.HasVertices)
                    {
                        Vector3D vector = mesh.Vertices[faceIndex];
                        currentMesh.Vertices.Add(vector.X, vector.Y, vector.Z);
                    }

                    if (mesh.HasNormals)
                    {
                        Vector3D normal = mesh.Normals[faceIndex];
                        currentMesh.Normals.Add(normal.X, normal.Y, normal.Z);
                    }

                    if (mesh.HasTextureCoords(0))
                    {
                        Vector3D texCoord = mesh.TextureCoordinateChannels[0][faceIndex];
                        currentMesh.UVs.Add(texCoord.X, texCoord.Y);
                    }

                    if (mesh.HasVertexColors(0))
                    {
                        Color4D vertexColor = mesh.VertexColorChannels[0][faceIndex];
                        currentMesh.VertexColors.Add(vertexColor.R, vertexColor.G, vertexColor.B, vertexColor.A);
                    }
                }

                if ((options & ModelImportOptions.SaveMaterial) != 0)
                {
                    currentMesh.ImportedMaterial = materials[mesh.MaterialIndex];
                }
            }

            return meshes;
        }
    }
}