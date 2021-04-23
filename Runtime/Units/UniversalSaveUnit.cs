using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// The root of all Universal Save Units. Does nothing on its own. Used for consistancy for units and the editors.
    /// </summary>
    [RenamedFrom("Lasm.BoltExtensions.IO.BinarySaveUnit")]
    [RenamedFrom("Lasm.BoltExtensions.BinarySaveUnit")]
    [RenamedFrom("Lasm.UAlive.BinarySaveUnit")]
    public abstract class UniversalSaveUnit : Unit
    {
        protected override void Definition()
        {
            
        }
    }
}