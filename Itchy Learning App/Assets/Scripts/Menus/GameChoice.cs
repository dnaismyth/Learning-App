using UnityEngine;
using System.Collections;

public class GameChoice : MonoBehaviour {
    public ButtonScroll bs;
    public Canvas currentGame;
    private bool gameActive = false;
    private string gameChoice;
	// Use this for initialization

    void Awake()
    {
        currentGame.GetComponent<Canvas>().enabled = false;
    }

	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnMouseDown()
    {
        gameActive = true;
        currentGame.GetComponent<Canvas>().enabled = true;
        Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.name);
        gameChoice = this.transform.GetComponent<SpriteRenderer>().sprite.name;
        bs.setIsScrolling(false);
    }

    public void getChosen()
    {

    }
}
