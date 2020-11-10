using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

namespace Lasm.Bolt.UniversalSave
{
    /// <summary>
    /// A Unit to check if a Binary Save has a paticular variable.
    /// </summary>
    [UnitCategory("IO")]
    [RenamedFrom("Lasm.BoltExtensions.IO.HasBinaryVariable")]
    [RenamedFrom("Lasm.BoltExtensions.HasBinaryVariable")]
    [RenamedFrom("Lasm.UAlive.HasBinaryVariable")]
    public sealed class HasUniversalVariable : UniversalSaveUnit
    {
        /// <summary>
        /// The Value Input port for the instance of the Binary Save we are checking the variable exists in.
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
        /// Returns true if the variable exists in the Binary Save.
        /// </summary>
        [DoNotSerialize]
        [PortLabelHidden]
        public ValueOutput result;

        protected override void Definition()
        {
            binary = ValueInput<UniversalSaver>(nameof(binary));
            variableName = ValueInput<string>(nameof(variableName), string.Empty);
            result = ValueOutput<bool>(nameof(result), HasVariable);

            Requirement(binary, result);
            Requirement(variableName, result);
        }

        private bool HasVariable(Flow flow)
        {
            return flow.GetValue<UniversalSaver>(binary).Has(flow.GetValue<string>(variableName));
        }
    }
}