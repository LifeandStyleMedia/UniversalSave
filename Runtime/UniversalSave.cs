using Lasm.Bolt.UniversalSaver.OdinSerializer;
using System;
using Unity.VisualScripting;
using System.IO;
using System.Collections.Generic;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// The underlying type of the Universal Save system. This is the type that will be saved and loaded with all the data you assigned.
    /// </summary>
    [Serializable]
    [RenamedFrom("Lasm.BoltExtensions.IO.BinarySave")]
    [RenamedFrom("Lasm.BoltExtensions.BinarySave")]
    [RenamedFrom("Lasm.UAlive.BinarySave")]
    [IncludeInSettings(true)][Inspectable]
    public sealed class UniversalSave
    {
        [Inspectable]
        public DataFormat dataFormat = DataFormat.Binary;

        /// <summary>
        /// All the save variables.
        /// </summary>
        [RenamedFrom("Lasm.BoltExtensions.IO.BinarySave.saves")]
        [RenamedFrom("Lasm.UAlive.BinarySave.saves")]
        [Inspectable][InspectorWide]
        [Serialize]
        public Dictionary<string, object> variables = new Dictionary<string, object>();

        /// <summary>
        /// The amount of saved variables in this save.
        /// </summary>
        public int Count => variables.Count;

        /// <summary>
        /// Load a binary save from a given path.
        /// </summary>
        /// <param name="path"></param>
        internal static UniversalSave Load(string path, DataFormat dataFormat)
        {
            if (File.Exists(path))
            {
                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    return SerializationUtility.DeserializeValue<UniversalSave>(SerializationUtility.CreateReader(fileStream, new DeserializationContext(), dataFormat));
                }
            }

            return null;
        }

        /// <summary>
        /// Save a binary save to a file path.
        /// </summary>
        internal static void Save(string path, UniversalSave universalSave)
        {
            string filelessPath = string.Empty;

            if (path.Contains("/"))
            {
                filelessPath = path.Remove(path.LastIndexOf("/"));
            }
            else
            {
                filelessPath = path.Remove(path.LastIndexOf(@"\"));
            }

            if (!Directory.Exists(filelessPath)) Directory.CreateDirectory(filelessPath);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                SerializationUtility.SerializeValue<UniversalSave>(universalSave, SerializationUtility.CreateWriter(fileStream, new SerializationContext(), universalSave.dataFormat));
            }
        }

        public static byte[] GetBytes(UniversalSave universalSave)
        {
            return SerializationUtility.SerializeValue<UniversalSave>(universalSave, universalSave.dataFormat);
        }

        public static UniversalSave FromBytes(byte[] bytes, DataFormat format)
        {
            return SerializationUtility.DeserializeValue<UniversalSave>(bytes, format);
        }

        /// <summary>
        /// Deletes a file if it exists.
        /// </summary>
        internal static void Delete(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }


        /// <summary>
        /// Get a variable from this Universal Save.
        /// </summary>
        internal object Get(string name)
        {
            return variables[name];
        }

        /// <summary>
        /// Checks if this Universal Save has a variable.
        /// </summary>
        internal bool Has(string name)
        {
            return variables.ContainsKey(name);
        }

        /// <summary>
        /// Removes a variable from the Universal Save.
        /// </summary>
        internal void Remove(string name)
        {
            variables.Remove(name);
        }

        /// <summary>
        /// Sets a value of a Universal Save variable.
        /// </summary>
        internal void Set(string name, object value)
        {
            if (variables.ContainsKey(name))
            {
                variables[name] = value;
            }
            else
            {
                variables.Add(name, value);
            }
        }
    }
}

