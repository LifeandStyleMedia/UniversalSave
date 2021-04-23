using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    /// <summary>
    /// A descriptor for a Save Universal Variable Unit.
    /// </summary>
    [Descriptor(typeof(SaveUniversalVariables))]
    public sealed class SaveUniversalVariablesDescriptor : UniversalSaveUnitDescriptor
    {
        public SaveUniversalVariablesDescriptor(SaveUniversalVariables target) : base(target)
        {
        }

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);

            for (int i = 0; i < ((SaveUniversalVariables)target).count; i++)
            {
                if (port.key == "name_" + i.ToString()) description.showLabel = false;
                if (port.key == "value_" + i.ToString()) description.label = "Value";
            }
        }
    }
}