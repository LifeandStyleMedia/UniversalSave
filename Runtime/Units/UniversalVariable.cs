using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// A single variable that is the data type for UniversalSaves to store in its internal dictionary.
    /// </summary>
    [Inspectable]
    [RenamedFrom("Lasm.BoltExtensions.IO.BinaryVariable")]
    [RenamedFrom("Lasm.BoltExtensions.BinaryVariable")]
    [RenamedFrom("Lasm.UAlive.BinaryVariable")]
    public sealed class UniversalVariable
    {
        /// <summary>
        /// Assign a name and value upon creating a new variable.
        /// </summary>
        public UniversalVariable(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        /// <summary>
        /// The name of the variable.
        /// </summary>
        [Inspectable]
        public string name;

        /// <summary>
        /// The variables value.
        /// </summary>
        [Inspectable]
        public object value;
    }
}