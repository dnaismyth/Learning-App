using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class initialsound_settings : MonoBehaviour {

    [SerializeField]
    public Sprite[] letters; // array for the sprites assets to be added
    [SerializeField] public
    Sprite[] finalLetters;      // array for the final sounds letters
    [SerializeField]
    public Sprite[] vowelLetters;
    [SerializeField]
    private initialSounds_settingsMouse originalCard; // reference for the card in the scene
    static bool isEmpty = false; // boolean to check if the user selections list is empty
    static bool alreadyChosen = false;
    public int gridRows = 3; // value for how many grid spaces to make + how far apart to place them
    public int gridCols = 4;
    public float offsetX = 1.5f;
    public float offsetY = 4f;
    private string playerPrefName;
    private SpriteRenderer sprite;
    private int currentGame;        // store the current game
    public initialSounds_settingsMouse[] allChoices;
    public List<initialSounds_settingsMouse> allLetterChoices = new List<initialSounds_settingsMouse>();
    public initialSounds_settingsMouse letter;
    static List<int> userChoices = new List<int>(); // a list to store the id's of the letters in which they have chosen, will somehow need to pass this into the game scene. 
    // Use this for initialization
    
    void Awake()
    {
        currentGame = PlayerPrefs.GetInt("currentGame");        // store the current game being played into a variable to determine letters
        //Debug.Log("This is the current game: " + currentGame);
        for(int i = 0; i <=25; i++)
        {
            //Debug.Log("These are my current letters for IS "+ PlayerPrefs.GetInt("initial_letter" + i));
        }
    }

    void Start()
    {
        
        
        if (currentGame == 5)
        {
            gridCols = 6;
            playerPrefName = "final_letter";   // use this as our reference for final letters
            createFinalLetters();   // call the method to only create final letters
            for(int i = 0; i < finalLetters.Length; i++)
            {
               // Debug.Log(PlayerPrefs.GetInt(playerPrefName + i));
            }
        }
        else if (currentGame == 11)
        {
            playerPrefName = "vowel_letter";
            createVowels();
            gridCols = 6;
            // call the method to only create the vowel letters
        }
        else if(currentGame == 6)
        {
            playerPrefName = "initial_letter";
            createAllLetters();
        }

            //print("here is is_letter1: " + PlayerPrefs.GetString("is_letter1"));
            //print("userchoices size: " + userChoices.Count);
            foreach (int id in userChoices)
            {
               print("THESE ARE MY IDS" + id.ToString());
            }
    }

    public int getListSize()
    {
        return userChoices.Count;
    }

    public string getPlayerPrefName()
    {
        return playerPrefName;
    }



    private void letterAppearance(List<initialSounds_settingsMouse> avail)
    {
        for (int i = 0; i < avail.Count; i++)
        {
            Debug.Log("Avail: " + avail[i].getId() + "Here is my pref " + PlayerPrefs.GetInt("initial_letter" + i));
            if (PlayerPrefs.GetInt(playerPrefName + i) != -1)
            {
                avail[PlayerPrefs.GetInt(playerPrefName + i)].GetComponent<SpriteRenderer>().color = Color.grey;
                avail[PlayerPrefs.GetInt(playerPrefName + i)].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            

        }

            //Debug.Log("These are my player prefs so far" +  PlayerPrefs.GetInt("initial_letter" + i));

        }

    private void finalLetterAppearance(List<initialSounds_settingsMouse> avail)
    {
        int index = 0;
        int finalId = 0;
        if (index == 0)
        {
            finalId = 1;
        }
        else if (index == 1)
        {
            finalId = 2;
        }
        else if (index == 2)
        {
            finalId = 5;
        }
        else if (index == 3)
        {
            finalId = 6;
        }
        else if (index == 4)
        {
            finalId = 11;
        }
        else if (index == 5)
        {
            finalId = 13;
        }
        else if (index == 6)
        {
            finalId = 17;
        }
        else if (index == 7)
        {
            finalId = 18;
        }
        else if (index == 8)
        {
            finalId = 19;
        }
        else if (index == 9)
        {
            finalId = 23;
        }
        else if (index == 10)
        {
            finalId = 12;
        }
        else if (index == 11)
        {
            finalId = 3;
        }
    }




    public void createAllLetters()
    {
        Vector3 startPos = originalCard.transform.position; // position of the first card, all cards will be offset from here

        int index = 0;
        for (int i = 0; i < letters.Length; i++)
        {


            if (i == 0)
            {
                letter = originalCard;
            }
            else
            {
                letter = Instantiate(originalCard) as initialSounds_settingsMouse;
               
               // letter = AddCom
            }


            //setLetter from the settingsMouse class, using the sprites provides and creating an id based on the i-iteration
            letter.setLetter(letters[index], i, letters[index].name);
            // allChoices[i] = letter; // add all of the letters into a list 
            //print("My Curr Choices: " + theseLetters[i]);
            allLetterChoices.Add(letter);
            float posX = startPos.x + (i % gridCols) * offsetX;
            float posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            letter.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card


            // Center the remaining 4 letters positioning
            if (letters.Length - i < 5)
            {
                posX = startPos.x + (i % gridCols) * offsetX + 4;
                posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            }

            letter.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card
           
            //letterAppearance(letter, letters); // call the letterAppearance helper method to change how the letter is displayed if a user has already chosen it
            index++;
            

        }
        
        letterAppearance(allLetterChoices);
    }

    public void createFinalLetters()
    {

        Vector3 startPos = new Vector3(-3, originalCard.transform.position.y, originalCard.transform.position.z); // position of the first card, all cards will be offset from here

        int index = 0;
        int finalId = 0;
        for (int i = 0; i < finalLetters.Length; i++)
        {
            if (index == 0)
            {
                finalId = 1;
            }
            else if (index == 1)
            {
                finalId = 2;
            }
            else if (index == 2)
            {
                finalId = 5;
            }
            else if (index == 3)
            {
                finalId = 6;
            }
            else if (index == 4)
            {
                finalId = 11;
            }
            else if (index == 5)
            {
                finalId = 13;
            }
            else if (index == 6)
            {
                finalId = 17;
            }
            else if (index == 7)
            {
                finalId = 18;
            }
            else if (index == 8)
            {
                finalId = 19;
            }
            else if (index == 9)
            {
                finalId = 23;
            }
            else if (index == 10)
            {
                finalId = 12;
            }
            else if (index == 11)
            {
               finalId = 3;
            }


            if (i == 0)
            {
                // take out the 'a'
            }
            else
            {
                letter = Instantiate(originalCard) as initialSounds_settingsMouse;
            }


            //setLetter from the settingsMouse class, using the sprites provides and creating an id based on the i-iteration
            letter.setLetter(finalLetters[index], finalId, finalLetters[index].name);
            allLetterChoices.Add(letter);
                                    //print("My Curr Choices: " + theseLetters[i]);

            float posX = startPos.x + (i % gridCols) * offsetX;
            float posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            letter.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card


            // Center the remaining 4 letters positioning
            if (letters.Length - i < 5)
            {
                posX = startPos.x + (i % gridCols) * offsetX + 4;
                posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            }

            letter.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card
            //letterAppearance(letter, finalLetters); // call the letterAppearance helper method to change how the letter is displayed if a user has already chosen it
            index++;
        }
        letterAppearance(allLetterChoices);
    }

    public void createVowels()
    {

        Vector3 startPos = new Vector3(-3, originalCard.transform.position.y, originalCard.transform.position.z); // position of the first card, all cards will be offset from here

        int index = 0;
        for (int i = 0; i < vowelLetters.Length; i++)
        {


            if (i == 0)
            {
                letter = originalCard;
            }
            else
            {
                letter = Instantiate(originalCard) as initialSounds_settingsMouse;
            }


            //setLetter from the settingsMouse class, using the sprites provides and creating an id based on the i-iteration
            letter.setLetter(vowelLetters[index], i, vowelLetters[index].name);
            allLetterChoices.Add(letter);
                                    //print("My Curr Choices: " + theseLetters[i]);

            float posX = startPos.x + (i % gridCols) * offsetX;
            float posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            letter.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card


            // Center the remaining 4 letters positioning
            if (letters.Length - i < 5)
            {
                posX = startPos.x + (i % gridCols) * offsetX + 4;
                posY = startPos.y + (int)Mathf.Floor((float)i / gridCols) * -offsetY;
            }

            letter.transform.position = new Vector3(posX, posY, startPos.z); // create a new position based on this offset for the newly instatiated card
            index++;
        }
        letterAppearance(allLetterChoices);
    }

    public void onAllRemove()
    {
        
        Debug.Log("I am clicked");
        foreach (initialSounds_settingsMouse card in allLetterChoices)
        {

            card.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            card.GetComponent<SpriteRenderer>().color = Color.white;
            userChoices.Remove(card.getId());

        }
        Debug.Log("This is the length of all choices: " + allLetterChoices.Count);
       
        PlayerPrefs.Save();
    }


    public void lettersChosen(initialSounds_settingsMouse letter)
    {

        if (!userChoices.Contains(letter.getId()))
        {
            userChoices.Add(letter.getId());
            isEmpty = false;
            alreadyChosen = false;
        }
        else if (userChoices.Contains(letter.getId()))
        {
            alreadyChosen = true;
            PlayerPrefs.DeleteKey(playerPrefName + letter.getId());
            //Debug.Log("Deleted this key: " + playerPrefName + letter.getId());
            userChoices.Remove(letter.getId());
            //Debug.Log("Testing if we have this " + userChoices.Contains(letter.getId()));
            PlayerPrefs.Save();
        }
        else if (userChoices.Count == 0)
        {
            isEmpty = true; // this variable will be passed into the playbutton screen, if it is set to true, do not allow the user to continue to the game screen
        }


    }

    private void saveUserPreferences()
    {
        int i = 0;

        foreach (int id in userChoices)
        {
            //print("My Current Initial sound Ids Chosen: " + id);
            PlayerPrefs.SetInt(playerPrefName + i, id);
            i++;
        }
        // Set the remaining choices to -1 so we know which ones to include when we pass them through to the next scene
        //-1 will mean that there is still space left for these letters to be chosen
        for (int numChoices = userChoices.Count; numChoices <= 25; numChoices++)
        {
            PlayerPrefs.SetInt(playerPrefName + numChoices, -1); 
        }
        PlayerPrefs.Save();

    }

    // getter method to return whether or not a user already has this letter they have selected
    public bool hasAlready()
    {
        return alreadyChosen;
    }

    // public method to use in the settings mouse script
    public void setHasAlready()
    {
        alreadyChosen = false;
    }


    void OnDestroy()
    {
        saveUserPreferences();
    }


    public bool getIsEmpty()
    {
        return isEmpty;
    }

    public void setIsEmpty(bool input)
    {
        isEmpty = input;
    }
}
