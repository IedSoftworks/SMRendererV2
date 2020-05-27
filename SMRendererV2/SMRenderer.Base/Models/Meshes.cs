using System;
using System.IO;
using System.Reflection;
using Assimp;
using SMRenderer.Core;

namespace SMRenderer.Base.Models
{
    public class Meshes
    { 
        public static Mesh Cube;
        public static Mesh Plane;

        public static void Load()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Base.obj");

            Mesh[] meshes = Importer.ImportFile(path);

            foreach (Mesh mesh in meshes)
            {
                if (mesh.Name.StartsWith("SMCube")) Cube = mesh;
                else if (mesh.Name.StartsWith("SMPlane")) Plane = mesh;
            }
        }
    }
}