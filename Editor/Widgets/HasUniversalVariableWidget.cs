using Ludiq;
using Bolt;

namespace Lasm.Bolt.UniversalSave.Editor
{
    /// <summary>
    /// The visuals and visual behaviour of a HasBinaryVariable Unit.
    /// </summary>
    [Widget(typeof(HasUniversalVariable))]
    public sealed class HasUniversalVariableWidget : UnitWidget<HasUniversalVariable>
    {
        public HasUniversalVariableWidget(FlowCanvas canvas, HasUniversalVariable unit) : base(canvas, unit)
        {
        }

        /// <summary>
        /// Overrides the color of this unit to be Teal like other variable units.
        /// </summary>
        protected override NodeColorMix baseColor => NodeColorMix.TealReadable;
    }
}