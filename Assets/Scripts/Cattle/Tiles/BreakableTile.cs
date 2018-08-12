using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cattle.Tiles
{
    public class BreakableTile : Tile
    {
    #if UNITY_EDITOR
        [MenuItem("Assets/Create/Breakable Tile")]
        public static void CreateRoadTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Breakable Tile", "New breakable tile", "asset",
                "Save Breakable Tile", "Assets");
            if (path == "")
                return;
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BreakableTile>(), path);
        }
    #endif
    }
}