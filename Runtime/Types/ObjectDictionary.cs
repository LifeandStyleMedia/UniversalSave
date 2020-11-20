using System;
using System.Collections.Generic;
using Ludiq;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// An AOTDictuonary replacement that can be serialized and saved.
    /// </summary>
    [Serializable][Inspectable]
    [IncludeInSettings(true)]
    [RenamedFrom("Lasm.BoltExtensions.IO.ObjectDictionary")]
    [RenamedFrom("Lasm.BoltExtensions.ObjectDictionary")]
    [RenamedFrom("Lasm.UAlive.ObjectDictionary")]
    public sealed class ObjectDictionary : Dictionary<object, object> { }
}

