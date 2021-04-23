using Unity.VisualScripting;
using UnityEngine;

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
                var formatRect = position;
                formatRect.x += 42;
                formatRect.y += 22;
                formatRect.width = 60;
                formatRect.height = 20;

                var countLabelRect = position;
                countLabelRect.x += 42;
                countLabelRect.y += 42;
                countLabelRect.width = 40;
                countLabelRect.height = 20;

                var countRect = position;
                countRect.x += 84;
                countRect.y += 42;
                countRect.width = 40;
                countRect.height = 20;

                LudiqGUI.Inspector(metadata["format"], formatRect, GUIContent.none);
                GUI.Label(countLabelRect, "Count");

                Inspector.BeginBlock(metadata, position);
                LudiqGUI.Inspector(metadata["count"], countRect, GUIContent.none);
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