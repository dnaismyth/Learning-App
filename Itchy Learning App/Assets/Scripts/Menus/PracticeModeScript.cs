using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PracticeModeScript : MonoBehaviour {
    public GameDisplay gd = new GameDisplay();
    public Color32 color = new Color32(186, 85, 211, 0);
    private int currChild = 0; // used to hold the current child that is selected from the options
    // arrays to store a few images of each game that can be displayed
    public Sprite Spelling;
    public Sprite Final;
    public Sprite Vowel;
    public Sprite Initial;
    public Sprite Matching;
    // Use this for initialization

    void Awake()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Button>().image.color = Color.grey;

    }

    void Update()
    {
        PlayerPrefs.Save();

    }
    
    void OnDestroy()
    {
        PlayerPrefs.Save(); // save the preferences on scene exit
    }

    public void MatchingChosen()
    {
        changeChildren(currChild);
        PlayerPrefs.SetInt("currentGame", 9); // set the current game chosen to 9 (Matching)

        this.gameObject.transform.GetChild(0).GetComponent<Button>().image.color = Color.grey;
        gd.setActive("Matching", Matching);
        currChild = 0;
    }

    public void VowelsChosen()
    {
        changeChildren(currChild);

        PlayerPrefs.SetInt("currentGame", 12); // set the current game chosen to 12 (Vowels)

        this.gameObject.transform.GetChild(4).GetComponent<Button>().image.color = Color.grey;

        gd.setActive("Vowels", Vowel);
        currChild = 4;
    }

    public void InitialChosen()
    {
        changeChildren(currChild);

        this.gameObject.transform.GetChild(3).GetComponent<Button>().image.color = Color.grey;
        PlayerPrefs.SetInt("currentGame", 5); // set the current game chosen to 5 (initial sounds)

        gd.setActive("Initial", Initial);
        currChild = 3;
    }

    public void FinalChosen()
    {
        changeChildren(currChild);

        PlayerPrefs.SetInt("currentGame", 4); // set the current game chosen to 4 (final sounds)

        this.gameObject.transform.GetChild(2).GetComponent<Button>().image.color = Color.grey;
        gd.setActive("Final", Final);
        currChild = 2;
    }

    public void SpellingChosen()
    {
        changeChildren(currChild);

        PlayerPrefs.SetInt("currentGame", 11); // set the current game chosen to 11 (spelling)

        this.gameObject.transform.GetChild(1).GetComponent<Button>().image.color = Color.grey;
        gd.setActive("Spelling", Spelling);
        currChild = 1;
    }

    public void playButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("currentGame"));

    }

    // method used to change the current active button back to white
    private void changeChildren(int currChild)
    {
        this.gameObject.transform.GetChild(currChild).GetComponent<Button>().image.color = Color.white;

    }
}
