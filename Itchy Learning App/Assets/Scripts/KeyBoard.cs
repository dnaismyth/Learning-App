﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(AudioSource))]
public class KeyBoard : MonoBehaviour {
    public GUISkin skin;
    public GUISkin imageSkin;
    public Texture2D buttonWrong;  // hold the texture for wrong button
    public Texture2D buttonCorrect; // hold the texture image for correct
    public Texture2D buttonNormal;  // hold texture for normal appearance
    public Texture2D buttonActive;
    public bool isWrong = false;    // use these to check if the button has updated its appearance 
    public bool isRight = false;
    //Arrays to hold Picture and Letter Cues
    public Texture[]pictureLetters;
	public Texture[]fontLetters;
	//Border Image
	public Texture border;
	//Arrays to hold Sounds for Words and Individual Letters
	public AudioClip[]wordSounds;
	public AudioClip[]letterSounds;
	//Yay sounds
	public AudioClip[]Yay;
	//Nay sounds
	public AudioClip[]Nay;
	//Audio Source
	public AudioSource playAud;
	//Random Number to Choose Picture Cue
	public int ranDisplay;
	//Check value to check if a new Word needs to be generated
	public int newWord = -1;
	//Indicates if answer was correct
	public bool respons = false;
	//True if pictures False if letters
	public bool picKeyBoard;
	//Boolean values for the skip and replay buttons
	public bool b1;
	public bool b2;
	//Checks if congrat audio should be played 0=no, 1=yay sound, 2=nay sound
	public int congrat;
	//Holds Skip Icon Texture
	public Texture skip;
	//Holds replay Icon Texture
	public Texture replay;
	//Keeps track of remaining rounds
	public int rounds;
	//keeps track of negative score points
	public int score=0;


    void Awake()
    {
        PlayerPrefs.SetInt("currentGame", 7);       // set the current game to the appropriate scene, this will be used to pass throug
        PlayerPrefs.Save();
        //Assigns an audio source	
        playAud = gameObject.AddComponent<AudioSource>();
    }
	//Runs at Startup
	void Start()
	{
		//Checks if a new Word is necessary, if so play the corresponding sound
		if (newWord == 0) {
			playSound(wordSounds [ranDisplay],0.8f);
			newWord = 1;
		}
		//Initializes AudioSource
		playAud=gameObject.AddComponent<AudioSource> ();
		//Sets ranDisplay
		SetRandom ();
		//Indicates if a picture or font keyboard is to be used
		picKeyBoard = false;
		//Initialize congrat
		congrat = 0;
	}

    public void loadMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void skipPressed()
    {
            SetRandom();
     
    }

    void OnGUI()
	{


        //Replays sound if image is pressed
        if (b2 == true) {
			playSound(wordSounds [ranDisplay],0.8f);
			b2=false;
		}
        //Checks if user wants to skip
        /*if (b1 == true) {
			//score++;
			//rounds++;
			b1=false;
			SetRandom();
		}*/

        //b1 = GUI.Button (new Rect (480, 300, 75, 50), skip);

        int buttonSizeWidth = Screen.width / 14;
        float buttonSizeHeight = Screen.height / 8.5f;
        int buttonSpacing = Screen.width/110;
		int xOffset = Screen.width/30;
		int yOffset = Screen.height/2;
		int numCols = 12;
		int InDeX = 0;
		int numButtons = 26;
        Color colour = Color.white;
        colour.a = 1;
        GUI.backgroundColor = colour;
        GUI.skin = skin;
        skin.button.normal.background = buttonNormal;
        skin.button.hover.background = buttonActive;
        //float nextUse = 0f;
        //float delay = 5f;
        /**********************************/
        Texture[] display;//Holds letter images that will be displayed on the keyboard
		//Checks whether picture letters or font letters are being displayed
		if (picKeyBoard) {
			display = fontLetters;
		} else {
			display = pictureLetters;
		}
		/**********************************/
		//Checks if a new Word is necessary, if so play the corresponding sound
		if (newWord == 0) {
			playSound(wordSounds [ranDisplay],0.8f);
			newWord = 1;
		}


        //Creates Image to match
       // GUI.DrawTexture (new Rect (Screen.width/2.5f, 10, 200, 200), display [ranDisplay], ScaleMode.ScaleToFit, true, 1.0F);
		//Creates surrounding border
		GUI.DrawTexture (new Rect (Screen.width / 2.7f, Screen.height / 15, Screen.width / 4.0f, Screen.height / 2.8f), border, ScaleMode.StretchToFill, true, 1.0F);
		//*****************************************************************************************
		Rect r = new Rect (0, 0, buttonSizeWidth, buttonSizeHeight); // rect for picture cues
		Rect r2 = new Rect (0, 0, buttonSizeWidth, buttonSizeHeight);

		//Loop through the amount of buttons user will have selected and assign a new Button object per texture.
		for (int i = 0; i < numButtons; i++) {
			if (i < 12) {
				r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing);
				r.y = yOffset + (int)Mathf.Floor ((float)i / numCols) * (buttonSizeHeight + buttonSpacing);
			}
			if (i < 22 && i > 11) {
				numCols = 10;
				r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing);
				r.y = yOffset + (int)Mathf.Floor ((float)i / numCols) * (buttonSizeHeight + buttonSpacing);
			}
			if (i > 21) {
				numCols = 8;
                r.x = xOffset + (i % numCols) * (buttonSizeWidth + buttonSpacing) + Screen.width / 36;
                r.y = yOffset + (int)Mathf.Floor ((float)i / numCols) * (buttonSizeHeight + buttonSpacing);
			}
			//Texture currTexture = pictureLetters[i];
			
		}
		for (int i = 0; i < numButtons; i++) {
			if (i < 12) {
				r2.x = xOffset + (i % 12) * (buttonSizeWidth + buttonSpacing);
				r2.y = yOffset + (int)Mathf.Floor ((float)i / 12) * (buttonSizeHeight + buttonSpacing);
			}
			if (i < 20 && i > 11) {
				r2.x = xOffset + (i % 10) * (buttonSizeWidth + buttonSpacing);
				r2.y = yOffset + (int)Mathf.Floor ((float)i / 10) * (buttonSizeHeight + buttonSpacing);
			}
			if (i > 19 && i < 26) {
                r2.x = (xOffset + 58) + (i % 9) * (buttonSizeWidth + buttonSpacing) + Screen.width / 36;
				r2.y = yOffset + (int)Mathf.Floor ((float)i / 9) * (buttonSizeHeight + buttonSpacing);
			}
			if (picKeyBoard == true && InDeX < 26) {
				if (GUI.Button (new Rect (r2), pictureLetters [InDeX]) == true) {
					if (InDeX == ranDisplay) {
						playSound(letterSounds [InDeX],0.8f);
						respons=true;
						congrat=1;
					} else
					{
						playSound(letterSounds [InDeX],0.8f);
						congrat=2;
						score++;
					}
				}
				InDeX++;
			} else if (picKeyBoard == false && InDeX < 26) {
				if (GUI.Button (new Rect (r2), fontLetters [InDeX]) == true) { //object type = bool (determine whether user has clicked (true) or not (false)
					if (InDeX == ranDisplay) {
						playSound(letterSounds [InDeX],0.8f);
						respons=true;
						congrat=1;
					} else
					{
						playSound(letterSounds [InDeX],0.8f);
						congrat=2;
						score++;
					}
				}
				InDeX++;
			}
		}

        //Creates Word Image
        GUI.skin = imageSkin;
        Texture temp = pictureLetters[ranDisplay];

        imageSkin.button.normal.background = (Texture2D)temp;
        imageSkin.button.hover.background = null;
        if (isWrong == true)
        {
            imageSkin.button.normal.background = buttonWrong;
            StartCoroutine(waitForSeconds(1.5f));
        }
        else if (isRight == true)
        {
            imageSkin.button.normal.background = buttonCorrect;
            StartCoroutine(waitForSeconds(1.5f));
        }
        b2 = GUI.Button(new Rect(Screen.width / 2.7f, Screen.height / 15, Screen.width / 4.0f, Screen.height / 2.8f), replay);
    }

    IEnumerator waitForSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        isWrong = false;
        isRight = false;
    }

    //Sets Random Location in Word Array for the selected word
    void SetRandom()
	{
		//Stores index of previous word
		int prev = ranDisplay;
		//Sets new index(word)
		ranDisplay = Random.Range (0, 26);
		//Checks that new word is not the same as the previous word
		if (prev == ranDisplay)
			SetRandom ();
		//Indicates a new Word is ready to be displayed
		newWord = 0;
		//Increases rounds
		rounds++;
	}
	
	//Plays Sounds
	void playSound(AudioClip sound, float vol)
	{
		
        if (playAud.isPlaying)
        {
            playAud.Stop();
        }
		//Assigns Clip and Volume then plays sound
		playAud.clip = sound;
		playAud.volume = vol;
        playAud.Play();
       
	}

	// Update is called once per frame
	void Update () {
		/*If the audio has stopped playing and letter is true
		(a letter sound was played and the one chosen was correct) reset letter to false 
		and set a new random word using SetRandom()*/
		if(!playAud.isPlaying){
			if(congrat==1){
                isRight = true;
				congrat=Random.Range(0, 10);
				playSound(Yay[congrat], 0.8f);
				congrat=0;
			}
			else if(congrat==2){
                isWrong = true;
				congrat=Random.Range(0, 5);
				playSound(Nay[congrat], 0.8f);
				congrat=0;
			}
			else if (congrat==0 && respons == true) {
				SetRandom ();
				respons=false;
			}
		}
	}
}