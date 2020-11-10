using Ludiq;
using Bolt;

namespace Lasm.Bolt.UniversalSave
{
    /// <summary>
    /// A Unit that removes a variabke from a BinarySave.
    /// </summary>
    [UnitCategory("IO")]
    [RenamedFrom("Lasm.BoltExtensions.IO.RemoveBinaryVariable")]
    [RenamedFrom("Lasm.BoltExtensions.RemoveBinaryVariable")]
    [RenamedFrom("Lasm.UAlive.RemoveBinaryVariable")]
    public sealed class RemoveUniversalVariable : UniversalSaveUnit
    {
        /// <summary>
        /// The Control Input port we enter when we want to remove a variable.
        /// </summary>
        [DoNotSerialize][PortLabelHidden]
        public ControlInput enter;

        /// <summary>
        /// The Control Output port invoked when removing the variable is complete.
        /// </summary>
        [DoNotSerialize][PortLabelHidden]
        public ControlOutput exit;

        /// <summary>
        /// The Value Input port for the instance of the Binary Save we are removing the variable from.
        /// </summary>
        [DoNotSerialize]
        public ValueInput binary;

        /// <summary>
        /// The name of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput variableName;

        protected override void Definition()
        {
            enter = ControlInput("enter", RemoveVariable);
            binary = ValueInput<UniversalSaver>(nameof(binary));
            variableName = ValueInput<string>(nameof(variableName), string.Empty);
            exit = ControlOutput("exit");

            Succession(enter, exit);
            Requirement(binary, enter);
            Requirement(variableName, enter);
        }

        private ControlOutput RemoveVariable(Flow flow)
        {
            flow.GetValue<UniversalSaver>(binary).Remove(flow.GetValue<string>(variableName));
            return exit;
        }
    }
}