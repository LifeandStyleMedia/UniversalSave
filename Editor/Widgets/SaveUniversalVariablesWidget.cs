using Ludiq;
using Bolt;
using UnityEngine;
using Lasm.Dependencies.Humility;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    [Widget(typeof(SaveUniversalVariables))]
    public sealed class SaveUniversalVariablesWidget : UnitWidget<SaveUniversalVariables>
    {
        public SaveUniversalVariablesWidget(FlowCanvas canvas, SaveUniversalVariables unit) : base(canvas, unit)
        {
        }

        protected override bool showHeaderAddon => true;
        protected override void DrawHeaderAddon()
        {
            if (!unit.promoteToInputPort)
            {
                LudiqGUI.Inspector(metadata["format"], position.Add().X(42).Add().Y(22).Set().Width(60).Set().Height(20), GUIContent.none);
                GUI.Label(position.Add().X(42).Add().Y(42).Set().Width(40).Set().Height(20), "Count");

                Inspector.BeginBlock(metadata, position, GUIContent.none);
                LudiqGUI.Inspector(metadata["count"], position.Add().X(84).Add().Y(42).Set().Width(40).Set().Height(20), GUIContent.none);
                if (Inspector.EndBlock(metadata))
                {
                    unit.Define();
                }
            }
        }

        protected override float GetHeaderAddonHeight(float width)
        {
            return unit.promoteToInputPort ? 0 : 38;
        }
    }
}