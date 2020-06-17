using System;
using System.IO;
using System.Reflection;
using SMRenderer.Base.Models.Import;
using SMRenderer.Base.Models.Import.Enums;
using SMRenderer.Core;
using SMRenderer.Core.Models;

namespace SMRenderer.Base.Models
{
    public class Meshes
    {
        public static Model DefaultModel = Cube1;

        public static ImportedMesh Cube1;
        public static ImportedMesh Cube6;
        public static ImportedMesh Plane;
        public static ImportedMesh Cone;
        public static ImportedMesh Cylinder;
        public static ImportedMesh Sphere;
        public static ImportedMesh Torus;

        public static void Load()
        {
            ImportedMesh[] meshes = Importer.ImportMeshes("SMDefaultObjects.glb", true, ModelImportOptions.None);

            foreach (ImportedMesh mesh in meshes)
            {
                if (mesh.Name.StartsWith("SMCube-1")) Cube1 = mesh;
                else if (mesh.Name.StartsWith("SMCube-6")) Cube6 = mesh;
                else if (mesh.Name.StartsWith("SMPlane")) Plane = mesh;
                else if (mesh.Name.StartsWith("SMCone")) Cone = mesh;
                else if (mesh.Name.StartsWith("SMCylinder")) Cylinder = mesh;
                else if (mesh.Name.StartsWith("SMSphere")) Sphere = mesh;
                else if (mesh.Name.StartsWith("SMTorus")) Torus = mesh;
            }
        }
    }
}