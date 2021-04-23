using System;
using Unity.VisualScripting;
using System.Collections.Generic;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// An AOTDictuonary replacement that can be serialized and saved.
    /// </summary>
    [Serializable][Inspectable][IncludeInSettings(true)]
    [RenamedFrom("Lasm.BoltExtensions.IO.ObjectList")]
    [RenamedFrom("Lasm.BoltExtensions.ObjectList")]
    [RenamedFrom("Lasm.UAlive.ObjectList")]
    public sealed class ObjectList : List<object> { }
}

