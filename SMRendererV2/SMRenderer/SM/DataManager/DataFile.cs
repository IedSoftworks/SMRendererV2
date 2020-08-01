using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SM.DataManager
{
    [Serializable]
    public class DataFile : Dictionary<string, DataCollection>
    {
        public DataFile() {}
        public DataFile(SerializationInfo info, StreamingContext context) : base(info, context) {}

        public DataCollection Add(string categoryName)
        {
            DataCollection datC = new DataCollection();
            Add(categoryName, datC);
            return datC;
        }

        public void Save(Stream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, this);
        }

        public void Save(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write)) Save(stream);
        }

        public static DataFile Load(Stream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();
            return (DataFile) bf.Deserialize(stream);
        }

        public static DataFile Load(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                return Load(stream);
        }
    }
}