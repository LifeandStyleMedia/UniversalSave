using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// A Unit that sets a variable value of a UniversalSave instance.
    /// </summary>
    [UnitCategory("IO")]
    [RenamedFrom("Lasm.BoltExtensions.IO.GetBinaryVariable")]
    [RenamedFrom("Lasm.BoltExtensions.GetBinaryVariable")]
    [RenamedFrom("Lasm.UAlive.GetBinaryVariable")]
    public sealed class GetUniversalVariable : UniversalSaveUnit
    {
        /// <summary>
        /// The Value Input port for the instance of the Universal Save we are getting the variable of.
        /// </summary>
        [DoNotSerialize]
        public ValueInput binary;

        /// <summary>
        /// The name of the variable.
        /// </summary>
        [DoNotSerialize][PortLabelHidden]
        public ValueInput variableName;

        /// <summary>
        /// The returned value of this variable.
        /// </summary>
        [DoNotSerialize][PortLabelHidden]
        public ValueOutput value;

        protected override void Definition()
        {
            binary = ValueInput<UniversalSave>(nameof(binary));
            variableName = ValueInput<string>(nameof(variableName), string.Empty);
            value = ValueOutput<object>(nameof(value), GetVariable);

            Requirement(binary, value);
            Requirement(variableName, value);
        }

        private object GetVariable(Flow flow)
        {
            return flow.GetValue<UniversalSave>(binary).Get(flow.GetValue<string>(variableName));
        }
    }
}