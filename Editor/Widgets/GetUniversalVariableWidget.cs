using Ludiq;
using Bolt;

namespace Lasm.Bolt.UniversalSave.Editor
{
    /// <summary>
    /// The visuals and visual behaviour of a GetBinaryVariable Unit.
    /// </summary>
    [Widget(typeof(GetUniversalVariable))]
    public sealed class GetUniversalVariableWidget : UnitWidget<GetUniversalVariable>
    {
        public GetUniversalVariableWidget(FlowCanvas canvas, GetUniversalVariable unit) : base(canvas, unit)
        {
        }

        /// <summary>
        /// Overrides the color of this unit to be Teal like other variable units.
        /// </summary>
        protected override NodeColorMix baseColor => NodeColorMix.TealReadable;
    }
}