using Ludiq;
using Bolt;

namespace Lasm.Bolt.UniversalSave.Editor
{
    /// <summary>
    /// The visuals and visual behaviour of a RemoveBinaryVariable Unit.
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