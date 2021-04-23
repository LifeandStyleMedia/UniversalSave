using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    /// <summary>
    /// The visuals and visual behaviour of a GetUniversalVariable Unit.
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