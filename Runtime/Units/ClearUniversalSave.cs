using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// Clears all variables of a Universal Save object.
    /// </summary>
    [RenamedFrom("Lasm.BoltExtensions.IO.ClearBinarySave")]
    [RenamedFrom("Lasm.BoltExtensions.ClearBinarySave")]
    [RenamedFrom("Lasm.UAlive.ClearBinarySave")]
    [UnitCategory("IO")]
    public sealed class ClearUniversalSave : UniversalSaveUnit
    {
        /// <summary>
        /// The Control Input port to enter when we want to clear the UniversalSaves variables.
        /// </summary>
        [DoNotSerialize][PortLabelHidden]
        public ControlInput enter;

        /// <summary>
        /// The Control Output port invoked when clearing is complete.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput exit;

        /// <summary>
        /// The Value Input port of the UniversalSave.
        /// </summary>
        [DoNotSerialize]
        public ValueInput save;

        protected override void Definition()
        {
            save = ValueInput<UniversalSave>("save");
            enter = ControlInput("enter", (flow) => { flow.GetValue<UniversalSave>(save).variables.Clear(); return exit; });
            exit = ControlOutput("exit");

            Succession(enter, exit);
        }
    }
}