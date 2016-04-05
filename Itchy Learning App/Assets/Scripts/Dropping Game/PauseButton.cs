using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour {

    public Color highlight;
    public CatchController cc;
    //private bool touched = false; // hold whether or not the button has already been touched
    SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        if (cc.getIsPlaying() && cc.pause == false) // check if the game is currently being played
        {
            cc.pauseGame(); // if so, the button will pause the game
            cc.pause = true;
            sprite.color = Color.black;
        }
        else if (!cc.getIsPlaying() && cc.pause == true)
        {
            cc.gamePlaying(); // if the game is paused, reclicking this button will set the game to playing
            cc.pause = false;
            sprite.color = Color.white;
        }
    }
}
