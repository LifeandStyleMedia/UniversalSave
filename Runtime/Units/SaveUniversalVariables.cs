using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;
using Lasm.OdinSerializer;

namespace Lasm.Bolt.UniversalSave
{
    /// <summary>
    /// A Unit that saves the Binary Variables to the path of your choosing.
    /// </summary>
    [UnitTitle("Save Binary")]
    [UnitCategory("IO")]
    [RenamedFrom("Lasm.BoltExtensions.IO.SaveBinaryVariables")]
    [RenamedFrom("Lasm.BoltExtensions.SaveBinaryVariables")]
    [RenamedFrom("Lasm.UAlive.SaveBinaryVariables")]
    public sealed class SaveUniversalVariables : UniversalSaveUnit
    {
        /// <summary>
        /// Uses the OS application path (Persistant Data Path) if true.
        /// </summary>
        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool usePersistantDataPath = true;

        /// <summary>
        /// Adds variables of a Binary Save or newly created variables on this Unit, 
        /// to the current Binary Save, if they don't exist. If they do exist, the value will be overwritten.
        /// </summary>
        [Serialize]
        [Inspectable]
        [InspectorToggleLeft]
        public bool append = false;

        /// <summary>
        /// Makes the saved data come from an external source, by providing an input port for a Binary Save on the unit.
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
        [UnitHeaderInspectable("Count")]
        public int count { get { return _count; } set { _count = Mathf.Clamp(value, 0, 100); } }

        /// <summary>
        /// The path to the Binary Save.
        /// </summary>
        [DoNotSerialize]
        public ValueInput path;

        /// <summary>
        /// The filename and extension for the path of the Binary Save.
        /// </summary>
        [DoNotSerialize]
        public ValueInput fileName;

        /// <summary>
        /// The Binary Save input port show when promoteToInputPort is true.
        /// </summary>
        [DoNotSerialize]
        public ValueInput binarySave;

        /// <summary>
        /// The output result of the Binary Save we just saved.
        /// </summary>
        [DoNotSerialize]
        public ValueOutput binarySaveOut;

        private UniversalSaver lastSave;

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

            if (promoteToInputPort) binarySave = ValueInput<UniversalSaver>("binary");

            if (!usePersistantDataPath) path = ValueInput<string>("path", string.Empty);
            fileName = ValueInput<string>("fileName", string.Empty);

            complete = ControlOutput("complete");
            DefineSaveControlPort();
            if (!promoteToInputPort) DefineVariablePorts();

            binarySaveOut = ValueOutput<UniversalSaver>("_binary", GetBinaryOutput);

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
            save = ControlInput("save", SaveBinary);
        }

        private void Save(Flow flow, UniversalSaver binary)
        {
            UniversalSaver.Save(GetPath(flow), binary);
        }

        private ControlOutput SaveBinary(Flow flow)
        {
            var binary = promoteToInputPort == true ? flow.GetValue<UniversalSaver>(binarySave) : new UniversalSaver();
            var loadedSave = append ? UniversalSaver.Load(GetPath(flow)) : null;

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
            return (usePersistantDataPath) ? Application.persistentDataPath + "/data/" + flow.GetValue<string>(fileName) : flow.GetValue<string>(path) + "/" + flow.GetValue<string>(fileName);
        }

        private UniversalSaver GetBinaryOutput(Flow flow)
        {
            return lastSave;
        }
    }

}