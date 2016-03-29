using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class initialsound_settings : MonoBehaviour {

    [SerializeField]
    private Sprite[] letters; // array for the sprites assets to be added
    [SerializeField] private
    Sprite[] finalLetters;      // array for the final sounds letters
    [SerializeField]
    private Sprite[] vowelLetters;
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
    public List<initialSounds_settingsMouse> allChoices = new List<initialSounds_settingsMouse>();
    public initialSounds_settingsMouse letter;
    static List<int> userChoices = new List<int>(); // a list to store the id's of the letters in which they have chosen, will somehow need to pass this into the game scene. 
    // Use this for initialization
    
    void Awake()
    {
        currentGame = PlayerPrefs.GetInt("currentGame");        // store the current game being played into a variable to determine letters
        Debug.Log("This is the current game: " + currentGame);
    }

    void Start()
    {
        
        
        if (currentGame == 4)
        {
            gridCols = 6;
            playerPrefName = "final_letter";   // use this as our reference for final letters
            createFinalLetters();   // call the method to only create final letters
            for(int i = 0; i < finalLetters.Length; i++)
            {
               // Debug.Log(PlayerPrefs.GetInt(playerPrefName + i));
            }
        }
        else if (currentGame == 12)
        {
            playerPrefName = "vowel_letter";
            createVowels();
            gridCols = 6;
            // call the method to only create the vowel letters
        }
        else if(currentGame == 5)
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



    public void letterAppearance(initialSounds_settingsMouse thisLetter, Sprite [] array)
    {
        for (int i = 0; i <= array.Length; i++)
        {
            //Debug.Log(playerPrefName + i);
            int let = PlayerPrefs.GetInt(playerPrefName + i);
            Debug.Log("Let: " + let);
            Debug.Log("This letter: " + thisLetter.getId());
            if (thisLetter.getId() == let && let != -1)
            {
                thisLetter.GetComponent<SpriteRenderer>().color = Color.grey;

                thisLetter.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else
            {
                thisLetter.GetComponent<SpriteRenderer>().color = Color.grey;

                thisLetter.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }

        
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
            allChoices.Add(letter); // add all of the letters into a list 
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
            letterAppearance(letter, letters); // call the letterAppearance helper method to change how the letter is displayed if a user has already chosen it
            index++;


        }

    }

    public void createFinalLetters()
    {
        Vector3 startPos = new Vector3(-3, originalCard.transform.position.y, originalCard.transform.position.z); // position of the first card, all cards will be offset from here

        int index = 0;
        for (int i = 0; i < finalLetters.Length; i++)
        {


            if (i == 0)
            {
                // take out the 'a'
            }
            else
            {
                letter = Instantiate(originalCard) as initialSounds_settingsMouse;
            }


            //setLetter from the settingsMouse class, using the sprites provides and creating an id based on the i-iteration
            letter.setLetter(finalLetters[index], i, finalLetters[index].name);
            allChoices.Add(letter); // add all of the letters into a list 
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
            letterAppearance(letter, finalLetters); // call the letterAppearance helper method to change how the letter is displayed if a user has already chosen it
            index++;
        }
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
            allChoices.Add(letter); // add all of the letters into a list 
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
            letterAppearance(letter, vowelLetters); // call the letterAppearance helper method to change how the letter is displayed if a user has already chosen it
            index++;
        }
    }

    public void onAllRemove()
    {
        foreach (initialSounds_settingsMouse card in allChoices)
        {

            card.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            card.GetComponent<SpriteRenderer>().color = Color.white;
            userChoices.Remove(card.getId());

            for (int i = 0; i < 25; i++)
            {
                PlayerPrefs.DeleteKey(getPlayerPrefName() + i); // change this to delete key
            }
        }
        
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
            userChoices.Remove(letter.getId());
            PlayerPrefs.Save();
        }
        else if (userChoices.Count == 0)
        {
            isEmpty = true; // this variable will be passed into the playbutton screen, if it is set to true, do not allow the user to continue to the game screen
        }


    }

    public void saveUserPreferences()
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
