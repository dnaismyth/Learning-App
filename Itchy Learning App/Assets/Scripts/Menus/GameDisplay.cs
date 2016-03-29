using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameDisplay : MonoBehaviour
{
 
    private Sprite current;
    private GameObject gameO;
    private string gameActive = "Matching";
	// Use this for initialization
	void Start () {
        gameO = this.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        getActive();
        setCurrentDisplay();
	}

    // use this method to set the current display depending on what button the user has selected
    private void setCurrentDisplay()
    {
        gameO.GetComponent<Image>().sprite = getCurrentSpriteShown();
    }

    // getters and setters to change which game is active via the practice menu script
    public string getActive()
    {
        return gameActive;
    }

    public Sprite getCurrentSpriteShown()
    {
        return current;
    }

    public void setActive(string s, Sprite sprite)
    {
        gameActive = s;
        current = sprite;
    }
}
