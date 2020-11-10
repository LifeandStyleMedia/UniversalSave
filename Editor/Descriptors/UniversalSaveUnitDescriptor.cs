using Ludiq;
using Bolt;
using Lasm.Utilities;
using UnityEngine;
using System.Collections.Generic;

namespace Lasm.Bolt.UniversalSave.Editor
{
    /// <summary>
    /// A descriptor for all BinarySaveUnits. Provides the fetching and application of the icon for these units.
    /// </summary>
    [Descriptor(typeof(UniversalSaveUnit))]
    public class UniversalSaveUnitDescriptor : UnitDescriptor<UniversalSaveUnit>
    {
        private Texture2D tex;

        public UniversalSaveUnitDescriptor(UniversalSaveUnit target) : base(target)
        {
            Debug.Log("Made");
        }

        protected override EditorTexture DefinedIcon()
        {
            if (tex == null) tex = Images.Load("UniversalSave", "universal_save", "universal_save_root");
            return EditorTexture.Single(tex);
        }

        protected override EditorTexture DefaultIcon()
        {
            if (tex == null) tex = Images.Load("UniversalSave", "universal_save", "universal_save_root");
            return EditorTexture.Single(tex);
        }
    }
}