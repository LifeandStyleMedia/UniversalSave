using System;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// An AOTDictuonary replacement that can be serialized and saved.
    /// </summary>
    [Serializable][Inspectable]
    [IncludeInSettings(true)]
    public sealed class ObjectDictionary : Dictionary<object, object> { }
}

