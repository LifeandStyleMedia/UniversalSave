using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// A Unit that sets a variable value of a UniversalSave instance.
    /// </summary>
    [UnitCategory("IO")]
    [RenamedFrom("Lasm.BoltExtensions.IO.SetBinaryVariable")]
    [RenamedFrom("Lasm.BoltExtensions.SetBinaryVariable")]
    [RenamedFrom("Lasm.UAlive.SetBinaryVariable")]
    public sealed class SetUniversalVariable : UniversalSaveUnit
    {
        /// <summary>
        /// The Control Input port we enter when we want to set a variable.
        /// </summary>
        [DoNotSerialize][PortLabelHidden]
        public ControlInput enter;

        /// <summary>
        /// The Control Output port invoked when setting the variable is complete.
        /// </summary>
        [DoNotSerialize][PortLabelHidden]
        public ControlOutput exit;

        /// <summary>
        /// The Value Input port for the instance of the Universal Save we are setting the variable on.
        /// </summary>
        [DoNotSerialize]
        public ValueInput binary;

        /// <summary>
        /// The name of this variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput variableName;

        /// <summary>
        /// The value for this variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput value;

        protected override void Definition()
        {
            enter = ControlInput("enter", SetVariable);
            binary = ValueInput<UniversalSave>(nameof(binary));
            variableName = ValueInput<string>(nameof(variableName), string.Empty);
            value = ValueInput<object>(nameof(value));
            exit = ControlOutput("exit");

            Requirement(binary, enter);
            Requirement(value, enter);
            Requirement(variableName, enter);

            Succession(enter, exit);
        }

        private ControlOutput SetVariable(Flow flow)
        {
            flow.GetValue<UniversalSave>(binary).Set(flow.GetValue<string>(variableName), flow.GetValue<object>(value));
            return exit;
        }
    }
}