﻿using Lasm.OdinSerializer;
using System;
using Ludiq;
using System.IO;
using System.Collections.Generic;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// The underlying type of the Universal Save system. This is the type that will be saved and loaded with all the data you assigned.
    /// </summary>
    [Serializable]
    [RenamedFrom("Lasm.BoltExtensions.IO.UniversalSave")]
    [RenamedFrom("Lasm.BoltExtensions.UniversalSave")]
    [RenamedFrom("Lasm.UAlive.UniversalSave")]
    [IncludeInSettings(true)][Inspectable]
    public sealed class UniversalSave
    {
        /// <summary>
        /// All the save variables.
        /// </summary>
        [RenamedFrom("Lasm.BoltExtensions.IO.UniversalSave.saves")]
        [RenamedFrom("Lasm.UAlive.UniversalSave.saves")]
        [Inspectable][InspectorWide]
        public Dictionary<string, object> variables = new Dictionary<string, object>();

        /// <summary>
        /// The amount of saved variables in this save.
        /// </summary>
        public int Count => variables.Count;

        /// <summary>
        /// Load a binary save from a given path.
        /// </summary>
        /// <param name="path"></param>
        public static UniversalSave Load(string path)
        {
            if (File.Exists(path))
            {
                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    return SerializationUtility.DeserializeValue<UniversalSave>(SerializationUtility.CreateReader(fileStream, new DeserializationContext(), DataFormat.Binary));
                }
            }

            return null;
        }

        /// <summary>
        /// Save a binary save to a file path.
        /// </summary>
        public static void Save(string path, UniversalSave binary)
        {
            string filelessPath = string.Empty;

            if (path.Contains("/")) {
                filelessPath = path.Remove(path.LastIndexOf("/"));
            }
            else
            {
                filelessPath = path.Remove(path.LastIndexOf(@"\"));
            }

            if (!Directory.Exists(filelessPath)) Directory.CreateDirectory(filelessPath);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                SerializationUtility.SerializeValue<UniversalSave>(binary, SerializationUtility.CreateWriter(fileStream, new SerializationContext(), DataFormat.Binary));
            }
        }

        /// <summary>
        /// Deletes a file if it exists.
        /// </summary>
        public static void Delete(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }


        /// <summary>
        /// Get a variable from this Universal Save.
        /// </summary>
        public object Get(string name)
        {
            return variables[name];
        }

        /// <summary>
        /// Checks if this Universal Save has a variable.
        /// </summary>
        public bool Has(string name)
        {
            return variables.ContainsKey(name);
        }

        /// <summary>
        /// Removes a variable from the Universal Save.
        /// </summary>
        public void Remove(string name)
        {
            variables.Remove(name);
        }

        /// <summary>
        /// Sets a value of a Universal Save variable.
        /// </summary>
        public void Set(string name, object value)
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
