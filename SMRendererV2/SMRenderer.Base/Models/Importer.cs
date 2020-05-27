using System.IO;
using Assimp;
using SMRenderer.Base.Types.Extensions;
using SMRenderer.Core.Exceptions;

namespace SMRenderer.Base.Models
{
    public class Importer
    {
        static AssimpContext importer = new AssimpContext();

        public static Mesh[] ImportFile(string fullPath, PostProcessSteps postProcessSteps = PostProcessSteps.None) =>
            Process(importer.ImportFile(fullPath, postProcessSteps));
        public static Mesh[] ImportStream(Stream stream, PostProcessSteps postProcessSteps = PostProcessSteps.None, string formatHint = null) =>
            Process(importer.ImportFileFromStream(stream, postProcessSteps, formatHint));
        
        static Mesh[] Process(Assimp.Scene importedModel)
        {
            if (!importedModel.HasMeshes)
                throw new LogException("You tried to import a file that doesn't contain a mesh.");

            Mesh[] meshes = new Mesh[importedModel.MeshCount];
            int c = 0;
            foreach (Assimp.Mesh mesh in importedModel.Meshes)
            {
                Mesh model = new Mesh();
                model.Name = mesh.Name;

                switch (mesh.PrimitiveType)
                {
                    case PrimitiveType.Line:
                        model.PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType.Lines;
                        break;
                    case PrimitiveType.Point:
                        model.PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType.Points;
                        break;
                    case PrimitiveType.Polygon:
                        model.PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType.LineStrip;
                        break;
                    case PrimitiveType.Triangle:
                        model.PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType.Triangles;
                        break;
                }

                mesh.Vertices.ForEach(a => model.Vertices.AddRange(a.X, a.Y, a.Z));
                mesh.Normals.ForEach(a => model.Normals.AddRange(a.X, a.Y, a.Z));
                mesh.TextureCoordinateChannels[0].ForEach(a => model.UVs.AddRange(a.X, a.Y));

                model.Compile();
                meshes[c++] = model;
            }

            return meshes;
        }
    }
}