using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SM.Core.Exceptions;

namespace SM.DataManager
{
    [Serializable]
    public class DataCollection : List<Data>
    {
        public object this[string name]
        {
            get => GetStoredData(name);
            set => Get(name).StoredData = value;
        }

        public Data Add(string name, object data)
        {
            Data dat = new Data(name, data);
            Add(dat);
            return dat;
        }

        public Data Get(string name)
        {
            Data data = this.FirstOrDefault(a => a.Name == name);
            return data;
        }

        public object GetStoredData(string name)
        {
            return Get(name)?.StoredData;
        }

        public T GetStoredData<T>(string name)
        {
            Data data = Get(name);
            return data != null ? data.GetStoredData<T>() : default;
        }
    }
}