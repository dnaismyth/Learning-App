using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemObject : ScriptableObject
{

    public string itemName = "Description";
    public string difficulty = "level of difficulty";
    public string title = "title";
    public int sceneNum = 0;
    public Color color;
    public AudioClip voiceDesc; // Audio of the description of the game
}