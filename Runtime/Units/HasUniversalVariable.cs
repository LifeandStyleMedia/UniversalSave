using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// A Unit to check if a Universal Save has a paticular variable.
    /// </summary>
    [UnitCategory("IO")]
    [RenamedFrom("Lasm.BoltExtensions.IO.HasBinaryVariable")]
    [RenamedFrom("Lasm.BoltExtensions.HasBinaryVariable")]
    [RenamedFrom("Lasm.UAlive.HasBinaryVariable")]
    public sealed class HasUniversalVariable : UniversalSaveUnit
    {
        /// <summary>
        /// The Value Input port for the instance of the Universal Save we are checking the variable exists in.
        /// </summary>
        [DoNotSerialize]
        public ValueInput binary;

        /// <summary>
        /// The name of the variable.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueInput variableName;

        /// <summary>
        /// Returns true if the variable exists in the Universal Save.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput result;

        protected override void Definition()
        {
            binary = ValueInput<UniversalSave>(nameof(binary));
            variableName = ValueInput<string>(nameof(variableName), string.Empty);
            result = ValueOutput<bool>(nameof(result), HasVariable);

            Requirement(binary, result);
            Requirement(variableName, result);
        }

        private bool HasVariable(Flow flow)
        {
            return flow.GetValue<UniversalSave>(binary).Has(flow.GetValue<string>(variableName));
        }
    }
}