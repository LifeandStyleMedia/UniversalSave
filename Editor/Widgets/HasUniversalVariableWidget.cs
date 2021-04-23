using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    /// <summary>
    /// The visuals and visual behaviour of a HasUniversalVariable Unit.
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