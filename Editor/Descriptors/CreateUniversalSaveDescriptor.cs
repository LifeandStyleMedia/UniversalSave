using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    /// <summary>
    /// A descriptor for a CreateUniversalSave Unit.
    /// </summary>
    [Descriptor(typeof(CreateUniversalSave))]
    public sealed class CreateUniversalSaveDescriptor : UniversalSaveUnitDescriptor
    {
        public CreateUniversalSaveDescriptor(CreateUniversalSave target) : base(target)
        {
        }

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);

            for (int i = 0; i < ((CreateUniversalSave)target).count; i++)
            {
                if (port.key == "name_" + i.ToString()) description.showLabel = false;
                if (port.key == "value_" + i.ToString()) description.label = "Value";
            }
        }
    }
}