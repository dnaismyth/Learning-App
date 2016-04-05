using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeGameObject
{

    [MenuItem("Assets/Create/Item Object")]
    public static void createItem()
    {

        ItemObject asset = ScriptableObject.CreateInstance<ItemObject>();
        AssetDatabase.CreateAsset(asset, "Assets/NewItemObject.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
