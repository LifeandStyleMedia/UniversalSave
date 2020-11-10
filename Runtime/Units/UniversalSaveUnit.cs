using Bolt;
using Ludiq;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// The root of all Universal Save Units. Does nothing on its own. Used for consistancy for units and the editors.
    /// </summary>
    [RenamedFrom("Lasm.BoltExtensions.IO.UniversalSaveUnit")]
    [RenamedFrom("Lasm.BoltExtensions.UniversalSaveUnit")]
    [RenamedFrom("Lasm.UAlive.UniversalSaveUnit")]
    public abstract class UniversalSaveUnit : Unit
    {
        protected override void Definition()
        {
            
        }
    }
}