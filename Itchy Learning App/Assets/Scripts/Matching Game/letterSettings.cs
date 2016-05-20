﻿/***********************************************************************************************************************/
// NOTES TO SELF: things that need fixing
// 2) maintain "bolded" images of letters that are chosen on destroying the scene
// 3) "add all" button
/**********************************************************************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class letterSettings : MonoBehaviour {

    [SerializeField] private Sprite[] letters; // array for the sprites assets to be added
    [SerializeField] private settingsMouse originalCard; // reference for the card in the scene
    static bool isEmpty = false; // boolean to check if the user selections list is empty
	static bool alreadyChosen = false;
    public int gridRows = 3; // value for how many grid spaces to make + how far apart to place them
    public int gridCols = 4;
    public float offsetX = 1.5f;
    public float offsetY = 4f;
	private SpriteRenderer sprite;
	public List<settingsMouse> allChoices = new List<settingsMouse>();
    public settingsMouse letter;
    static List<int> userChoicesMem = new List<int>(); // a list to store the id's of the letters in which they have chosen, will somehow need to pass this into the game scene. (memory game)
    // Use this for initialization

    void Start () {
        for (int i = 0; i < 26; i++)
        {
            Debug.Log("These are my player prefs " + PlayerPrefs.GetInt("letter" + i));
        }
        createLetters();
        
       
	}

    public int getListSize()
    {
        return userChoicesMem.Count;
    }

    

	private void letterAppearanceMemory(List<settingsMouse> choices){
        Debug.Log("This is my count " + choices.Count);
		for (int i = 0; i < choices.Count; i++) {
            
			if (PlayerPrefs.GetInt("letter" + i)!=-1) {
                //GetComponent<SpriteRenderer>().sprite = letters[thisLetter.getId ()];
                //this.sprite.color = Color.grey;
                choices[PlayerPrefs.GetInt("letter"+ i)].GetComponent<SpriteRenderer>().color = Color.grey;
				choices[PlayerPrefs.GetInt("letter"+ i)].transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
			}
		}


	}

   /* public void letterAppearance(List<settingsMouse> avail)
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

    }*/

    public void createLetters()
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
                letter = Instantiate(originalCard) as settingsMouse;
            }


            //setLetter from the settingsMouse class, using the sprites provides and creating an id based on the i-iteration
            letter.setLetter(letters[index], i);
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
            index++;

            
        }
        Debug.Log("I am at the end of the loop about to call method");
        letterAppearanceMemory(allChoices); // call the letterAppearance helper method to change how the letter is displayed if a user has already chosen it
    }

    public void onAllRemove(){
		foreach (settingsMouse card in allChoices) {
			card.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            card.GetComponent<SpriteRenderer>().color = Color.white;
            userChoicesMem.Remove(card.getId());

        }
        PlayerPrefs.Save();
    }

  

    public void lettersChosen(settingsMouse letter)
	{

        if (!userChoicesMem.Contains(letter.getId()))
        {
            userChoicesMem.Add(letter.getId());
            isEmpty = false;
			alreadyChosen = false;
        }
        else if (userChoicesMem.Contains(letter.getId()))
        {
			alreadyChosen = true;
			PlayerPrefs.DeleteKey ("letter" + letter.getId ());
            userChoicesMem.Remove(letter.getId());
			PlayerPrefs.Save ();
        }
        else if (userChoicesMem.Count == 0)
        {
            isEmpty = true; // this variable will be passed into the playbutton screen, if it is set to true, do not allow the user to continue to the game screen
        }
      
            
    }

    private void saveUserPreferences()
    {
        Debug.Log("HAHA I AM BEING CALLED");
		int i = 0;

        foreach (int id in userChoicesMem)
        {
                PlayerPrefs.SetInt("letter" + i, id);
            	i++;
        }
		// Set the remaining choices to -1 so we know which ones to include when we pass them through to the next scene
		//-1 will mean that there is still space left for these letters to be chosen
		for (int numChoices = userChoicesMem.Count; numChoices <= 25; numChoices++) {
			PlayerPrefs.SetInt ("letter" + numChoices, -1);
		}
        PlayerPrefs.Save();
       
    }

	// getter method to return whether or not a user already has this letter they have selected
	public bool hasAlready(){
		return alreadyChosen;
	}

	// public method to use in the settings mouse script
	public void setHasAlready(){
		alreadyChosen = false;
	}


    void OnDestroy(){
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