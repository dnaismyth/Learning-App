using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(AudioSource))]
public class InitialSounds: MonoBehaviour {
    public GUISkin skin;
    public GUISkin imageSkin;
    public Texture2D buttonWrong;  // hold the texture for wrong button
    public Texture2D buttonCorrect; // hold the texture image for correct
    public Texture2D buttonNormal;  // hold texture for normal appearance
    public Texture2D buttonActive;
    public bool isWrong = false;    // use these to check if the button has updated its appearance 
    public bool isRight = false;
    public bool userChoices = false;    // use this to check if the user has selected some letters to handle error   
	//Arrays to hold Picture and Letter Cues
	public Texture[]pictureLetters;
	public Texture[]fontLetters;

	//For Word Storage
	public Texture[]Words;
	public Texture[]Words1;
	public Texture[]Words2;
	public Texture[]Words3;
	public int[]WordsId;
	public int prev;

	//Border Image
	public Texture border;
	//Arrays to hold Sounds for Words and Individual Letters
	public AudioClip[]wordSounds;
	public AudioClip[]wordSounds1;
	public AudioClip[]wordSounds2;
	public AudioClip[]wordSounds3;
	public AudioClip[]letterSounds;
	//Yay sounds
	public AudioClip[]Yay;
	//Nay sounds
	public AudioClip[]Nay;
	//Audio Source
	public AudioSource playAud;
    //Random Number to Choose Picture Cue
    public int ranDisplay = 0;
	//Check value to check if a new Word needs to be generated
	public int newWord = -1;
	//Holds the four Options to match
	public int[] letterOptions = new int[4];
	//Boolean to catch and hold until second cue has played
	public bool letter;
	//Boolean to store correctness
	public bool respons;
	//Level indicator
	public int level;
	//Boolean values for the four buttons to indicate which has been selected
	public bool b1;
	public bool b2;
	public bool b3;
	public bool b4;
	//Button to skip question
	public bool b5;
	//Button to replay sound
	public bool b6;
	//Boolean to check itchy mode
	public bool itchyMode;
    //Array to store whether or not certain letters are being used
    public bool[] usable;
	//Holds alphabet value (0-25)
	public int indeX;
	//Checks if congrat audio should be played 0=no, 1=yay sound, 2=nay sound
	public int congrat;
	//Keeps track of remaining rounds
	public int rounds;
	//keeps track of negative score points
	public int score=0;
	//Holds Skip Icon Texture
	public Texture skip;
	//Holds replay Icon Texture
	public Texture replay;

	//Runs at Startup
    void Awake()
    {
        PlayerPrefs.SetInt("currentGame", 5);       // set the current game to the appropriate scene, this will be used to pass throug
        PlayerPrefs.Save();
        // Set the current game corresponding to scene number
        initializeUsable();
        ranDisplay = UnityEngine.Random.Range(0, 52);

    }

    void Start()
	{
        PlayerPrefs.SetInt("currentGame", 5);
        PlayerPrefs.Save();
        //Initializes usable letters

        //Initializes level
        level = 1;
        //Initializes Itchy Mode
        if (PlayerPrefs.GetInt("itchyMode") == 1)
            itchyMode = true;
        else
            itchyMode = false;
        //Initalize congrat
        congrat = 0;

		//Initializes AudioSource
		playAud=gameObject.AddComponent<AudioSource> ();
		//Initializes ranDisplay in order to initialize prev in SetRandom();
		ranDisplay = -1;
		//sets array of pictures/sounds and rounds based on level
		if (level == 2) {
			rounds=15;
			Words=Words2;
			wordSounds=wordSounds2;
		}
		else if (level == 3) {
			rounds=20;
			Words=Words3;
			wordSounds=wordSounds3;
		}
		else{
			rounds=10;
			Words=Words1;
			wordSounds=wordSounds1;
		}
		//Sets ranDisplay
		SetRandom ();
	}

    public void loadMenu()
    {
        SceneManager.LoadScene(11);

    }

    void initializeUsable()
    {
        //Debug current letters player has picked
        for (int i = 0; i <= 25; i++)
        {
            int letterId = PlayerPrefs.GetInt("initial_letter" + i);
            if(/*letterId != null &&*/ letterId != -1)  // user has selected specific letters to be chosen, therefore default will not be used (all letters)
            {
                Debug.Log("CURRENT LETTER ID CHOSEN: " + letterId);
                usable[letterId] = true;
                userChoices = true;
                Debug.Log(usable[i]);
            }
            else if(userChoices == false){ 
                usable[i] = true;      // if user has not selected specific letters, set them all to be used as default
            }


        }
    }

    //Creates Buttons
    void OnGUI()
    {

        //Holds new Word Texture
        Texture temp = Words[ranDisplay];
        //Checks if a new Word is necessary, if so play the corresponding sound
        if (newWord == 0)
        {
            playSound(wordSounds[ranDisplay], 0.8f, 1);
            newWord = 1;
        }
        //Replays sound if image is pressed
        if (b6 == true)
        {
            playSound(wordSounds[ranDisplay], 0.8f, 1);
            b6 = false;
        }
        //Checks if user wants to skip
        if (b5 == true)
        {
            score++;
            rounds++;
            b5 = false;
            SetRandom();
        }
        Color colour = Color.white;
        colour.a = 1;
        GUI.backgroundColor = colour;
        GUI.skin = skin;
        skin.button.normal.background = buttonNormal;
        skin.button.hover.background = buttonActive;
        //Option Buttons b1-b4 and positioning
        if (itchyMode == true)
        {

            b1 = GUI.Button(new Rect(Screen.width / 4.1f, Screen.height / 10, 150, 150), pictureLetters[letterOptions[0]]);

            b2 = GUI.Button(new Rect(Screen.width / 1.6f, Screen.height / 10, 150, 150), pictureLetters[letterOptions[1]]);

            b3 = GUI.Button(new Rect(Screen.width / 4.1f, Screen.height / 1.5f, 150, 150), pictureLetters[letterOptions[2]]);
            b4 = GUI.Button(new Rect(Screen.width / 1.6f, Screen.height / 1.5f, 150, 150), pictureLetters[letterOptions[3]]);
        }
        else if (itchyMode == false)
        {

            b1 = GUI.Button(new Rect(Screen.width / 4.1f, Screen.height / 10, 150, 150), fontLetters[letterOptions[0]]);

            b2 = GUI.Button(new Rect(Screen.width / 1.6f, Screen.height / 10, 150, 150), fontLetters[letterOptions[1]]);

            b3 = GUI.Button(new Rect(Screen.width / 4.1f, Screen.height / 1.5f, 150, 150), fontLetters[letterOptions[2]]);

            b4 = GUI.Button(new Rect(Screen.width / 1.6f, Screen.height / 1.5f, 150, 150), fontLetters[letterOptions[3]]);
        }

        //b5 = GUI.Button(new Rect(1200, 575, 150, 100), skip);
        //Creates Word Image
        GUI.skin = imageSkin;
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
        b6 = GUI.Button(new Rect(Screen.width / 2.4f, Screen.height / 2.9f, 200, 200), replay);

        //GUI.DrawTexture(new Rect(Screen.width/2.5f, Screen.height/3.5f, 200, 200), temp, ScaleMode.ScaleToFit, true, 1.0F);
        //Creates surrounding border
        GUI.DrawTexture(new Rect(Screen.width / 2.4f, Screen.height / 2.9f, 200, 200), border, ScaleMode.ScaleToFit, true, 1.0F);
    }
	

    // helper method to change the background of a button
    public void changeSkin(Texture2D texture, GUISkin skin)
    {
        skin.button.normal.background = texture;
    }

	//Sets Random Location in Word List for the selected word
	void SetRandom()
	{
		prev = ranDisplay;
       
        ranDisplay = UnityEngine.Random.Range(0, 52);
       
		//index (0-25) of alphabet
		indeX = ranDisplay % 2;
		if (indeX == 0)
			indeX = ranDisplay / 2;
		else
			indeX = (ranDisplay - 1) / 2;

		if (prev == indeX|!usable[indeX])
			SetRandom ();
	
		//Indicates a new Word is ready to be displayed
		newWord = 0;
		//Creates new Letter options for users to choose
		SetLetOptions ();
	}

	//Creates new Letter options for users to choose
	void SetLetOptions()
	{
		//Randomly generates options
		int i = 0;
		int j;
		//Values for already selected values
		int x=-1;
		int y=-1;
		int z=-1;

		while (i<3) {
			j = Random.Range (0, 26);
			//Checks that value is not the same as correct answer
			if(j!=indeX)
			{
				if(i==0)
				{
					x=j;
					i++;
				}
				else if(i==1&&j!=x)
				{
					y=j;
					i++;
				}
				else if(i==2)
					if(j!=x&&j!=y)
					{
						z=j;
						i++;
					}
			}
		}
		//Sets a Random Place in the Array to move the correct answer to a random location
		j = Random.Range (0, 4);
		//Switches the value at the random location and the correct answer at location 0
		if (j == 1) {
			letterOptions [0] = x;
			letterOptions [1] = indeX;
			letterOptions [2] = y;
			letterOptions [3] = z;
		} else if (j == 2) {
			letterOptions [0] = y;
			letterOptions [1] = x;
			letterOptions [2] = indeX;
			letterOptions [3] = z;
		} else if (j == 3) {
			letterOptions [0] = z;
			letterOptions [1] = x;
			letterOptions [2] = y;
			letterOptions [3] = indeX;
		} else {
			letterOptions [0] = indeX;
			letterOptions [1] = x;
			letterOptions [2] = y;
			letterOptions [3] = z;
		}

	}

   

    IEnumerator waitForSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        isWrong = false;
        isRight = false;
    }

    //Checks that the correct answer was selected
    void checkCorrectness()
	{
		//Placeholder value
		int buttonClicked = 27;

		//Checks boolean values to determine which button was clicked
		if (b1 == true) {
			buttonClicked = 1;
		}
		if (b2 == true) {
			buttonClicked = 2;
		}
		if (b3 == true) {
			buttonClicked = 3;
		}
		if (b4 == true) {
			buttonClicked = 4;
		}
		//Resets Boolean Values
		b1=false;
		b2=false;
		b3=false;
		b4=false;

		//Calls response to indicate if the button chosen results in true or false answers
		response (buttonClicked);
		//Plays the sound of the chosen letter
		playSound(letterSounds [letterOptions [buttonClicked - 1]],0.8f, 2);
	}

    /*Checks if audio is playing and stops it, then indicates with the respons value if 
	 answer is true or false*/
    void response(int buttonClicked)
    {
        if (playAud.isPlaying)
            playAud.Stop();

        if (letterOptions[buttonClicked - 1] == indeX) {
            //Debug.Log("buttonclicked" + buttonClicked);
            isRight = true;
            congrat = 1;
            respons = true;
            rounds = rounds - 1;
            //if rounds=0 escape
        } else {
            isWrong = true;
			congrat=2;
			respons = false;
			score=1;
		}
	}

  

    //Plays Sounds
    void playSound(AudioClip sound, float vol, int version)
	{
		//Assigns an audio source	
		playAud = gameObject.AddComponent<AudioSource> ();

		//Assigns Clip and Volume then plays sound
			playAud.clip = sound;
			playAud.volume = vol;
			playAud.Play();

		/*If this is a letter sound (version: 2) and the letter chosen is correct 
		(respons: true) the letter value is true*/
		if (version == 2&&respons==true)
			letter = true;
	}
	
	// Update is called once per frame
	void Update () {

		//If any of the letter buttons have been clicked (true) check the buttons correctness
		if (b1 == true || b2 == true || b3 == true || b4 == true) {
			checkCorrectness();
		}
		/*If the audio has stopped playing and letter is true
		(a letter sound was played and the one chosen was correct) reset letter to false 
		and set a new random word using SetRandom()*/
		if(!playAud.isPlaying){
			if(congrat==1){
				congrat=Random.Range(0, 9);
				playSound(Yay[congrat], 0.8f, 1);
				congrat=0;
			}
			else if(congrat==2){
				congrat=Random.Range(0, 6);
				playSound(Nay[congrat], 0.8f, 1);
				congrat=0;
			}
			else if (congrat==0 && respons == true) {
				SetRandom ();
				respons=false;
			}
		}
	}

    void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}