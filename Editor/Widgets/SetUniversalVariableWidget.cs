using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    /// <summary>
    /// The visuals and visual behaviour of a SetUniversalVariable Unit.
    /// </summary>
    [Widget(typeof(SetUniversalVariable))]
    public sealed class SetUniversalVariableWidget : UnitWidget<SetUniversalVariable>
    {
        public SetUniversalVariableWidget(FlowCanvas canvas, SetUniversalVariable unit) : base(canvas, unit)
        {
        }

        /// <summary>
        /// Overrides the color of this unit to be Teal like other variable units.
        /// </summary>
        protected override NodeColorMix baseColor => NodeColorMix.TealReadable;
    }
}