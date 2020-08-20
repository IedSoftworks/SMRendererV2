using Assimp;
using SM.Data.Models;
using SM.Data.Models.Import;
using SM.DataManager;
using SM.Utility;

namespace SM
{
    public class DataManage
    {
        public static DataFile DefaultDataFile { get; private set; }

        /// <summary>
        /// This compiles the default data and put in a file in the running folder for additional work.
        /// <para>NEVER NEVER RUN THIS IF YOU DON'T HAVE THE REQUIRED FILES.</para>
        /// </summary>
        public static void Compile()
        {
            DataFile dataFile = new DataFile();
            
            // Load and insert default meshes into the file
            DataCollection meshCollection = dataFile.Add("Meshes");
            ImportedMesh[] meshes = Importer.ImportMeshes("SMDefaultObjects.glb", true, PostProcessSteps.CalculateTangentSpace | PostProcessSteps.FlipUVs);
            foreach (ImportedMesh mesh in meshes) meshCollection.Add(mesh.Name, mesh);

            dataFile.Save(@"..\..\..\SMRenderer\SM\Default.smd");
        }

        /// <summary>
        /// This decompiles data from inside the "SM"-assembly.
        /// <para>Since it will be automaticly run at initilizing your default window, there is no need to call it menually</para>
        /// </summary>
        public static void Decompile()
        {
            DataFile dataFile = DefaultDataFile = DataFile.Load(AssemblyUtility.GetAssemblyStream("Default.smd"));

            DataCollection mesh = dataFile["Meshes"];
            Meshes.Plane = mesh.GetStoredData<ImportedMesh>("SMPlane");
            Meshes.Cube1 = mesh.GetStoredData<ImportedMesh>("SMCube1");
            Meshes.Cube6 = mesh.GetStoredData<ImportedMesh>("SMCube6");
            Meshes.Cylinder = mesh.GetStoredData<ImportedMesh>("SMCylinder");
            Meshes.Sphere = mesh.GetStoredData<ImportedMesh>("SMSphere");
            Meshes.Pyramid = mesh.GetStoredData<ImportedMesh>("SMPyramid");
        }
    }
}