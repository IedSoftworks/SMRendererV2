using System;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SM.DataManager
{
    [Serializable]
    public class Data
    {
        private object _storedData;
        public string Name;
        public Assembly Assembly { get; private set; }
        public Type Type { get; private set; }

        public object StoredData
        {
            get => _storedData;
            set
            {
                _storedData = value;
                UpdateData();
            }
        }

        private void UpdateData()
        {
            Type = _storedData.GetType();
            Assembly = Type.Assembly;
        }


        public Data(string name, object data)
        {
            Name = name;
            StoredData = data;

            UpdateData();
        }

        public T GetStoredData<T>()
        {
            return (T)_storedData;
        }
    }
}