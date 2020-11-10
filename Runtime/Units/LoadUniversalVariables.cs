using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;
using Lasm.OdinSerializer;

namespace Lasm.Bolt.UniversalSave
{
    /// <summary>
    /// Loads a Binary Save and returns the variables and values of it.
    /// </summary>
    [UnitCategory("IO")]
    [UnitTitle("Load Binary")]
    [RenamedFrom("Lasm.BoltExtensions.IO.LoadBinaryVariables")]
    [RenamedFrom("Lasm.BoltExtensions.LoadBinaryVariables")]
    [RenamedFrom("Lasm.UAlive.LoadBinaryVariables")]
    public sealed class LoadUniversalVariables : UniversalSaveUnit
    {
        /// <summary>
        /// Uses the OS application path (Persistant Data Path) if true.
        /// </summary>
        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool usePersistantDataPath = true;

        [OdinSerialize]
        private bool isInit;

        /// <summary>
        /// The Value Input port for a custom path. Shown only when usePersistantDataPath is false.
        /// </summary>
        [DoNotSerialize]
        public ValueInput path;

        /// <summary>
        /// The filename and file extension of this save.
        /// </summary>
        [DoNotSerialize]
        public ValueInput fileName;

        /// <summary>
        /// The Value Output port of the 
        /// </summary>
        [DoNotSerialize]
        public ValueOutput binary;

        /// <summary>
        /// 
        /// </summary>
        [DoNotSerialize]
        public ControlInput load;

        /// <summary>
        /// 
        /// </summary>
        [DoNotSerialize]
        public ControlOutput complete;

        public override void AfterAdd()
        {
            base.AfterAdd();

            if (!isInit)
            {
                usePersistantDataPath = true;
                Define();
                isInit = true;
            }
        }

        protected override void Definition()
        {
            if (!usePersistantDataPath) path = ValueInput<string>("path", string.Empty);
            fileName = ValueInput<string>(nameof(fileName), string.Empty);
            binary = ValueOutput<UniversalSaver>(nameof(binary));

            complete = ControlOutput("complete");
            load = ControlInput("load", (flow) => {
                flow.SetValue(binary, UniversalSaver.Load((usePersistantDataPath) ? Application.persistentDataPath + "/data/" + flow.GetValue<string>(fileName) : flow.GetValue<string>(path) + "/" + flow.GetValue<string>(fileName)));
                return complete;
            });

            Requirement(fileName, binary);
            Requirement(fileName, load);
            Succession(load, complete);
        }

    }
}