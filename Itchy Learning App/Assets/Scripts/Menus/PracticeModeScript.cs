using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PracticeModeScript : MonoBehaviour {
    public GameDisplay gd = new GameDisplay();
    public Color32 color = new Color32(186, 85, 211, 0);
    // arrays to store a few images of each game that can be displayed
    public Sprite Spelling;
    public Sprite Final;
    public Sprite Vowel;
    public Sprite Initial;
    public Sprite Matching;
    // Use this for initialization

    void Awake()
    {

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
        PlayerPrefs.SetInt("currentGame", 9); // set the current game chosen to 4 (initial sounds)

        this.gameObject.GetComponentInChildren<Button>().image.color = Color.grey;
        gd.setActive("Matching", Matching);
    }

    public void VowelsChosen()
    {
        PlayerPrefs.SetInt("currentGame", 12); // set the current game chosen to 4 (initial sounds)

        this.gameObject.GetComponentInChildren<Button>().image.color = Color.grey;
        gd.setActive("Vowels", Vowel);
    }

    public void InitialChosen()
    {
        this.gameObject.GetComponentInChildren<Button>().image.color = Color.grey;
        PlayerPrefs.SetInt("currentGame", 5); // set the current game chosen to 4 (initial sounds)

        gd.setActive("Initial", Initial);
    }

    public void FinalChosen()
    {
        PlayerPrefs.SetInt("currentGame", 4); // set the current game chosen to 4 (initial sounds)

        this.gameObject.GetComponentInChildren<Button>().image.color = Color.grey;
        gd.setActive("Final", Final);
    }

    public void SpellingChosen()
    {
        PlayerPrefs.SetInt("currentGame", 11); // set the current game chosen to 4 (initial sounds)

        this.gameObject.GetComponentInChildren<Button>().image.color = Color.grey;
        gd.setActive("Spelling", Spelling);
    }

    public void playButton()
    {
        Application.LoadLevel(PlayerPrefs.GetInt("currentGame"));

    }
}
