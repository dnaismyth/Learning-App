using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MakeGameObject
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Item Object")]
    public static void createItem()
    {

        ItemObject asset = ScriptableObject.CreateInstance<ItemObject>();
        AssetDatabase.CreateAsset(asset, "Assets/NewItemObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif
}
