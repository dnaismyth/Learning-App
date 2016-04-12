using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDescriptions : MonoBehaviour {

    public ItemObject[] games;      // created item objects to store description, title, level
    private string description;
    private string title;
    private string level;
    public GameChoice choice;
    private string sceneNum;
    public Font font;
    public GUIStyle style = new GUIStyle();
    public int gameIndex;

    void Awake()
    {
        
    }
    // Use this for initialization
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
    }

   public void getInfo(string num)
    {
        switch (num)
        {
            case "1":
                gameIndex = 0;
                break;
            case "3":
                gameIndex = 6;
                break;
            case "4":
                gameIndex = 4;
                break;
            case "5":
                gameIndex = 2;
                break;
            case "9":
                gameIndex = 1;
                break;
            case "10":
                gameIndex = 3;
                break;
            case "12":
                gameIndex = 5;
                break;       
      
        }
    }


    void OnGUI()
    {
        if (GetComponentInParent<Canvas>().enabled == true)
        {
            
            description = games[gameIndex].title;
            title = games[gameIndex].itemName;
            level = games[gameIndex].difficulty;

             GUI.skin.font = font;
             style.fontSize = 60;
             style.normal.textColor = Color.white;

             style.fontSize = 40;

             Color myColor = new Color();
             //Debug.Log(games[gameIndex].color.ToString());
             ColorUtility.TryParseHtmlString(games[gameIndex].color.ToString(), out myColor);
             style.normal.textColor = myColor;
             //GUI.Label(new Rect(Screen.width / 2 - level.Length*22, Screen.height / 2.5f, 100, 50), "Level: " +  level, style);
            // GetComponentInChildren<Text>().text = description; // handle the spacing with inspector text
            this.transform.GetChild(0).GetComponent<Text>().text = title;
            this.transform.GetChild(1).GetComponent<Text>().text = description;
            this.transform.GetChild(2).GetComponent<Text>().color = Color.white;
            this.transform.GetChild(2).GetComponent<Text>().text = "Level: " + level;

        }

    }

    public void playPressed()
    {
        
        SceneManager.LoadScene(games[gameIndex].sceneNum);
        PlayerPrefs.SetInt("currentGame", games[gameIndex].sceneNum);       // set the current game chosen
        
    }

}
