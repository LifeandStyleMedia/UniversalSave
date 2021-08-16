using Unity.VisualScripting;
using UnityEngine;
using System.IO;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// A Unit that checks if a Universal Save exists in the chosen path.
    /// </summary>
    [UnitCategory("IO")]
    [UnitTitle("Universal Exists")]
    [RenamedFrom("Lasm.BoltExtensions.IO.BinarySaveExists")]
    [RenamedFrom("Lasm.BoltExtensions.BinarySaveExists")]
    [RenamedFrom("Lasm.UAlive.BinarySaveExists")]
    public sealed class UniversalSaveExists : UniversalSaveUnit
    {
        /// <summary>
        /// Uses the OS application path (Persistant Data Path) if true.
        /// </summary>
        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool usePersistantDataPath = true;

        [Serialize]
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
        /// The input to enter when we want to check if this save exists.
        /// </summary>
        [DoNotSerialize]
        public ControlInput check;

        /// <summary>
        /// The output control that is invoked when the save does exist.
        /// </summary>
        [DoNotSerialize]
        public ControlOutput @true;

        /// <summary>
        /// The output control that is invoked when the save does not exist.
        /// </summary>
        [DoNotSerialize]
        public ControlOutput @false;

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
            check = ControlInput("check", SaveExists);
            if (!usePersistantDataPath) path = ValueInput<string>("path", string.Empty);
            fileName = ValueInput<string>(nameof(fileName), string.Empty);

            @true = ControlOutput("true");
            @false = ControlOutput("false");

            if (!usePersistantDataPath) Requirement(path, check);
            Requirement(fileName, check);
            Succession(check, @true);
            Succession(check, @false);
        }

        /// <summary>
        /// The method executed upon the Check port being entered. Checks if the save exists at path and filename.
        /// </summary>>
        public ControlOutput SaveExists(Flow flow)
        {
            if (File.Exists((usePersistantDataPath) ? Application.persistentDataPath + "/" + flow.GetValue<string>(fileName) : flow.GetValue<string>(path) + "/" + flow.GetValue<string>(fileName)))
            {
                return @true;
            }

            return @false;
        }
    }
}