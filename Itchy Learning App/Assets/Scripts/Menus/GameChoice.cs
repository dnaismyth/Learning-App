using UnityEngine;
using System.Collections;

public class GameChoice : MonoBehaviour {
    public ButtonScroll bs;
    public Canvas currentGame;
    private bool gameActive;
    private string gameChoice;
    public GameDescriptions gd;
    AudioSource playAudio;
    // Use this for initialization

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("itchyMode", 1); // by default, itchy mode enabled
        PlayerPrefs.Save();
        
        //gameActive = false;
        gameChoice = "";
        currentGame.GetComponent<Canvas>().enabled = false;
        playAudio = GetComponent<AudioSource>();
       
    }

	void Start () {
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        gameChoice = this.transform.GetComponent<SpriteRenderer>().sprite.name.ToString();
        gd.getInfo(gameChoice);         // pass through this variable to display the info of the game
        StartCoroutine(playSoundAfter()); // play the voice description of the game after 0.5 seconds
        setGameActive(true);
        currentGame.GetComponent<Canvas>().enabled = true;
        bs.setIsScrolling(false);
    }

    public void OnExit()
    {
        currentGame.GetComponent<Canvas>().enabled = false;
        bs.setIsScrolling(true);
            //TODO pause audio, check out later
    }

    IEnumerator playSoundAfter()
    {
        yield return new WaitForSeconds(0.5f);
        playAudio.PlayOneShot(gd.games[gd.gameIndex].voiceDesc); // "have fun finding the letters + pic cues that match"
    }


    public string getChosen()
    {
        return gameChoice;
    }

    public void setGameActive(bool b)
    {
        gameActive = b;
    }

    public bool getGameActive()
    {
        return gameActive;
    }
}
