using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    /// <summary>
    /// The visuals and visual behaviour of a RemoveUniversalVariable Unit.
    /// </summary>
    [Widget(typeof(RemoveUniversalVariable))]
    public sealed class RemoveUniversalVariableWidget : UnitWidget<RemoveUniversalVariable>
    {
        public RemoveUniversalVariableWidget(FlowCanvas canvas, RemoveUniversalVariable unit) : base(canvas, unit)
        {
        }

        /// <summary>
        /// Overrides the color of this unit to be Teal like other variable units.
        /// </summary>
        protected override NodeColorMix baseColor => NodeColorMix.TealReadable;
    }
}