using Unity.VisualScripting;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    /// <summary>
    /// A descriptor for all UniversalSaveUnits. Provides the fetching and application of the icon for these units.
    /// </summary>
    [Descriptor(typeof(UniversalSaveUnit))]
    public class UniversalSaveUnitDescriptor : UnitDescriptor<UniversalSaveUnit>
    {
        private EditorTexture tex;

        public UniversalSaveUnitDescriptor(UniversalSaveUnit target) : base(target)
        {
        }

        protected override EditorTexture DefinedIcon()
        {
            if (tex == null) tex = EditorTexture.Single(IconUtilities.Load("UniversalSave", "universal_save", "universal_save_editor_root"));
            return tex;
        }

        protected override EditorTexture DefaultIcon()
        {
            if (tex == null) tex = EditorTexture.Single(IconUtilities.Load("UniversalSave", "universal_save", "universal_save_editor_root"));
            return tex;
        }
    }
}