using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Lasm.Bolt.UniversalSaver.OdinSerializer;

namespace Lasm.Bolt.UniversalSaver
{
    /// <summary>
    /// A Unit that saves the Universal Variables to the path of your choosing.
    /// </summary>
    [UnitTitle("Save Universal Variables")]
    [UnitCategory("IO")]
    [RenamedFrom("Lasm.BoltExtensions.IO.SaveBinaryVariables")]
    [RenamedFrom("Lasm.BoltExtensions.SaveBinaryVariables")]
    [RenamedFrom("Lasm.UAlive.SaveBinaryVariables")]
    public sealed class SaveUniversalVariables : UniversalSaveUnit
    {
        [Inspectable]
        [Serialize]
        public DataFormat format = DataFormat.Binary;

        /// <summary>
        /// Uses the OS application path (Persistant Data Path) if true.
        /// </summary>
        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool usePersistantDataPath = true;

        /// <summary>
        /// Adds variables of a Universal Save or newly created variables on this Unit, 
        /// to the current Universal Save, if they don't exist. If they do exist, the value will be overwritten.
        /// </summary>
        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool append = false;

        /// <summary>
        /// Makes the saved data come from an external source, by providing an input port for a Universal Save on the unit.
        /// </summary>
        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool promoteToInputPort = true;

        [Serialize]
        private int _count;

        /// <summary>
        /// The amount of variables to create when saving.
        /// </summary>
        [Inspectable]
        public int count { get { return _count; } set { _count = Mathf.Clamp(value, 0, 100); } }

        /// <summary>
        /// The path to the Universal Save.
        /// </summary>
        [DoNotSerialize]
        public ValueInput path;

        /// <summary>
        /// The filename and extension for the path of the Universal Save.
        /// </summary>
        [DoNotSerialize]
        public ValueInput fileName;

        /// <summary>
        /// The Universal Save input port show when promoteToInputPort is true.
        /// </summary>
        [DoNotSerialize]
        public ValueInput binarySave;

        /// <summary>
        /// The output result of the Universal Save we just saved.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput binarySaveOut;

        private UniversalSave lastSave;

        /// <summary>
        /// The name ports for the Units save variables.
        /// </summary>
        [DoNotSerialize]
        public List<ValueInput> names = new List<ValueInput>();

        /// <summary>
        /// The value ports for the Units save variables.
        /// </summary>
        [DoNotSerialize]
        public List<ValueInput> values = new List<ValueInput>();

        /// <summary>
        /// The Control Input port to enter when saving.
        /// </summary>
        [DoNotSerialize]
        public ControlInput save;

        /// <summary>
        /// The Control Output port invoked when saving is complete.
        /// </summary>
        [DoNotSerialize]
        public ControlOutput complete;

        protected override void Definition()
        {
            values.Clear();

            if (promoteToInputPort) binarySave = ValueInput<UniversalSave>("binary");

            if (!usePersistantDataPath) path = ValueInput<string>("path", string.Empty);
            fileName = ValueInput<string>("fileName", string.Empty);

            complete = ControlOutput("complete");
            DefineSaveControlPort();
            if (!promoteToInputPort) DefineVariablePorts();

            binarySaveOut = ValueOutput<UniversalSave>("_binary", GetUniversalOutput);

            Requirement(fileName, save);
            Succession(save, complete);
        }

        private void DefineVariablePorts()
        {
            for (int i = 0; i < count; i++)
            {
                var namePort = ValueInput<string>("name_" + i.ToString(), string.Empty);
                var valuePort = ValueInput<object>("value_" + i.ToString());
                names.Add(namePort);
                values.Add(valuePort);
                Requirement(namePort, save);
                Requirement(valuePort, save);
            }
        }

        private void DefineSaveControlPort()
        {
            save = ControlInput("save", SaveUniversal);
        }

        private void Save(Flow flow, UniversalSave binary)
        {
            UniversalSave.Save(GetPath(flow), binary);
        }

        private ControlOutput SaveUniversal(Flow flow)
        {
            var binary = promoteToInputPort == true ? flow.GetValue<UniversalSave>(binarySave) : new UniversalSave() { dataFormat = format };
            var loadedSave = append ? UniversalSave.Load(GetPath(flow), format) : null;

            if (!promoteToInputPort)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (!string.IsNullOrEmpty(values[i].key))
                    {
                        binary.Set(flow.GetValue<string>(names[i]), flow.GetValue<object>(values[i]));
                    }
                }
            }

            if (loadedSave != null)
            {
                if (!promoteToInputPort)
                {
                    for (int i = 0; i < binary.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(values[i].key))
                        {
                            loadedSave.Set(flow.GetValue<string>(names[i]), flow.GetValue<object>(values[i]));
                        }
                    }
                }
                else
                {
                    var saveKeys = binary.variables.Keys.ToArrayPooled();
                    var saveValues = binary.variables.Values.ToArrayPooled();

                    for (int i = 0; i < binary.Count; i++)
                    {
                        loadedSave.Set(saveKeys[i], saveValues[i]);
                    }

                }

                Save(flow, loadedSave);

                lastSave = loadedSave;
            }
            else
            {
                Save(flow, binary);
                lastSave = binary;
            }


            return complete;
        }

        private string GetPath(Flow flow)
        {
            return (usePersistantDataPath) ? Application.persistentDataPath + "/" + flow.GetValue<string>(fileName) : flow.GetValue<string>(path) + "/" + flow.GetValue<string>(fileName);
        }

        private UniversalSave GetUniversalOutput(Flow flow)
        {
            return lastSave;
        }
    }

}