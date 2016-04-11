using UnityEngine;
using System.Collections;

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

            GUI.Label(new Rect(Screen.width / 2 - title.Length*11, Screen.height / 6, 100, 50), title, style);

            style.fontSize = 40;

            Color myColor = new Color();
            //Debug.Log(games[gameIndex].color.ToString());
            ColorUtility.TryParseHtmlString(games[gameIndex].color.ToString(), out myColor);
            style.normal.textColor = myColor;
            GUI.Label(new Rect(Screen.width / 2 - level.Length*11, Screen.height / 3, 100, 50), level, style);

            style.fontSize = 30;
            style.normal.textColor = Color.white;
            GUI.Label(new Rect(0, Screen.height / 1.5f, 100, 50), description, style);


        }

    }

}
