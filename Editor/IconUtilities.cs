using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Lasm.Bolt.UniversalSaver.Editor
{
    public static class IconUtilities
    {
        private static Dictionary<string, Dictionary<string, MultiTexture>> collections = new Dictionary<string, Dictionary<string, MultiTexture>>();

        public static Texture2D Load(string collection, string name, string rootFileName)
        {
            if (!collections.ContainsKey(collection))
            {
                collections.Add(collection, new Dictionary<string, MultiTexture>());
            }

            if (collections[collection].ContainsKey(name))
            {
                return GetStateTexture(collection, name);
            }

            var path = PathOf(rootFileName) + "Resources/Icons/";
            var multiTex = new MultiTexture();

            multiTex.personal = AssetDatabase.LoadAssetAtPath<Texture2D>(path + name + ".png");
            multiTex.pro = AssetDatabase.LoadAssetAtPath<Texture2D>(path + name + "@Pro.png");

            collections[collection].Add(name, multiTex);

            return GetStateTexture(collection, name);
        }

        private static Texture2D GetStateTexture(string collection, string name)
        {
            if (EditorGUIUtility.isProSkin)
            {
                if (collections[collection][name].pro == null)
                {
                    return collections[collection][name].personal;
                }

                return collections[collection][name].pro;
            }

            return collections[collection][name].personal;
        }

        private static string PathOf(string fileName)
        {
            var files = UnityEditor.AssetDatabase.FindAssets(fileName);
            if (files.Length == 0) return string.Empty;
            var assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(files[0]).Replace(fileName, string.Empty);
            return assetPath;
        }

        public class MultiTexture
        {
            public Texture2D personal;
            public Texture2D pro;
        }
    }
}