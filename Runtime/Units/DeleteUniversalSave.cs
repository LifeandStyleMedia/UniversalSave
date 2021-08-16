using UnityEngine;
using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// Deletes a Universal Save file from a path.
    /// </summary>
    [UnitCategory("IO")]
    [UnitTitle("Delete Save")]
    [RenamedFrom("Lasm.BoltExtensions.IO.DeleteBinarySave")]
    [RenamedFrom("Lasm.BoltExtensions.DeleteBinarySave")]
    [RenamedFrom("Lasm.UAlive.IO.DeleteBinarySave")]
    public class DeleteUniversalSave : UniversalSaveUnit
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
        /// The Control Input port to enter when you want to delete the save.
        /// </summary>
        [DoNotSerialize]
        public ControlInput delete;

        /// <summary>
        /// The Control Output port invoked when deleting is complete.
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

            complete = ControlOutput("complete");
            delete = ControlInput("delete", (flow) => {
                UniversalSave.Delete((usePersistantDataPath) ? Application.persistentDataPath + "/" + flow.GetValue<string>(fileName) : flow.GetValue<string>(path) + "/" + flow.GetValue<string>(fileName));
                return complete;
            });

            Requirement(fileName, delete);
            Succession(delete, complete);
        }

    }
}