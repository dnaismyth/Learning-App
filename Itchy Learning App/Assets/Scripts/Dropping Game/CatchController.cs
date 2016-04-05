using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CatchController : MonoBehaviour
{
    private Vector3 imagePos;
    public Font font;
    public Font spellingFont;
    private GameImage imageClass = new GameImage(0, "l", false); // use this to create imageClass objects to create bool checks for letters being collected
    private GameImage[] imageObjects;
    private string currentLetter;
    private GUIStyle style; //create a new variable
    private GUIStyle style2; 
    private int pos = Screen.width / 9; // holds the position of the font
    private string collected = ""; // a string to hold the current letters that are collected
    private string imageName; // hold the name of the image we are trying to match the letters to 
    public ImageController currImage;
    //private Dictionary<string, bool> checkCaught = new Dictionary<string, bool>(); // hold the letter and bool to check if it has been printed
    public int lives = 3; // set their lives to 3, we can change this later if we allow the user to decide the amount of lives (difficulty) etc.. 
    private int gameType; // use this for now to choose the game type (in order spelling or random) will change to player prefs later
    private bool isPlaying = false; // use this to make game pause etc.
    private int countDownTime = 3;
    private string startMessage; // hold a start message in here after the count down is finished
    private bool countActive = true;
    public AudioClip beep;
    public CoconutDropper dropper; // create a CoconutDropper instance to use to call the spawn function
    AudioSource playAudio;
    public bool pause = false;
    private bool fontReady = false;
    String pauseMessage = "";



    //genereic waiting method we can use
    IEnumerator Wait(float wait)
    {
        yield return new WaitForSeconds(wait);
        playAudio.PlayOneShot(currImage.wordSounds[currImage.getIndex()]); // play the corresponding letter sound on drop

    }


    // Update is called once per frame
    void Update()
    {
        getIsPlaying();
        if (isPlaying)
        {
            dropper.positionDropper();

            checkLives();

            if (imageClass.checkWinning(imageObjects) == true)
            {
                Debug.Log("You have won"); // will have to change this later to randomize a new image for a user to spell
                pauseGame(); // pause the game for now, will include Jeff's winning screen
                StartCoroutine(Wait(1.5f)); // wait 2 seconds and then replay the word they have just spelt

            }

        }

    }

    void Start()
    {
        StartCoroutine(Countdown(countDownTime));
       
    }

    void Awake()
    {
        style = new GUIStyle();
        style2 = new GUIStyle();
        playAudio = GetComponent<AudioSource>();
        gameType = PlayerPrefs.GetInt("droppingOptions");
    }

    public bool getIsPlaying()
    {
        return isPlaying;
    }

    public void setIsPlaying(bool b)
    {
        isPlaying = b;
    }

    // Use this as the initial screen to start a countdown
    IEnumerator Countdown(int seconds)
    {
        countActive = true;
        for (int i = seconds; i >= 0; i--)
        {
            currImage.GetComponent<Light>().enabled = true; // use to flicker the game image lighting
            if (i > 0)
            {
                Debug.Log(i.ToString());
                countDownTime = i;
                playAudio.PlayOneShot(beep);
                if (i % 2 != 0)
                {
                    currImage.GetComponent<Light>().enabled = false; //use to flicker the game image lighting
                }

                yield return new WaitForSeconds(1);
            }
            else if (i <= 0)
            {
                startMessage = "start";
                setName(); // set the name of the current Image as it is being displayed on awake in ImageController script. (Will have to recall both functions once the user has spelt out the word)
                createPairs();
                try {
                    playAudio.PlayOneShot(currImage.wordSounds[currImage.getIndex()]); // play the corresponding letter sound on drop
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.Log("still need to get these sounds");
                }
         
                    dropper.createCoconuts(); // create + spawn coconuts
                    countActive = false;
                    isPlaying = true;
                
       
                yield return new WaitForSeconds(1.5f); // wait for 1.5 seconds and clear the message
                    startMessage = "";
                    yield return new WaitForSeconds(2.0f); // wait another 1.5 seconds and start dropping
         
                    dropper.Spawn();
               
               


            }

        }


    }


    // This method will grab the name of the image as it is being updated
    void setName()
    {
        imagePos = currImage.GetComponent<SpriteRenderer>().transform.position;
        imageName = currImage.GetComponent<SpriteRenderer>().sprite.name.ToString(); // grab name of the image currently being displayed
    }

    void createPairs()
    {
        Debug.Log(imageName.ToString());
        imageObjects = new GameImage[imageName.Length]; // initialize our array to the length of the imageName
        char[] imgChars = imageName.ToCharArray();  // split the image name into individual characters to check for matching
        for (int i = 0; i < imgChars.Length; i++)
        {
            //checkCaught.Add(imgChars[i].ToString(), false); // add all of the letters to a key value pair, initial bool = false because not yet caught
            imageObjects[i] = new GameImage(i, imgChars[i].ToString(), false); // create GameImage objects and store into array
            Debug.Log("These letters are in the dictionary: " + imageObjects[i].getLetter());
        }
        fontReady = true;
    }

    // Destroy the coconut once it has collied with the catcher
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Coconut")
        {
            playAudio.PlayOneShot(dropper.letterSounds[dropper.getRand()]); // play the sound of the letter on collision
            currentLetter = other.gameObject.GetComponent<SpriteRenderer>().sprite.name.ToString();
            /**** BEGIN IF STATEMENTS FOR RANDOM SPELLING ORDER ******/
            if (gameType == 0) // (if random order of letter selection is allowed)
            {
                if (imageClass.Contains(imageObjects, currentLetter) == true)
                {

                    imageClass.findWithLetter(currentLetter, imageObjects).setCheck(true); // find the object within the array that contains the matching letter, and set it to true (it has been caught)
                    collected = collected + currentLetter;
                    Debug.Log(collected.ToString());
                }
                else if (collected.Contains(currentLetter))
                {
                    lives--; // decrement lives :(
                    Debug.Log("You already collected this letter");
                }
                else
                    lives--; // decrement lives

                Destroy(other.gameObject);
                //Debug.Log ("I'm Colliding");
            }

            /**** BEGIN IF STATEMENTS FOR CORRECT SPELLING ORDER ******/
            else if (gameType == 1) //(if correct order of spelling is chosen)
            {
                if (imageClass.correctOrder(imageObjects, currentLetter) == true)
                {
                    imageClass.findWithLetter(currentLetter, imageObjects).setCheck(true); // find the object within the array that contains the matching letter, and set it to true (it has been caught)
                    collected = collected + currentLetter;
                    Debug.Log(collected.ToString());
                }
                else if (collected.Contains(currentLetter))
                {
                    lives--; // decrement lives :(
                    Debug.Log("You already collected this letter");
                }
                else
                    lives--; // decrement lives

                Destroy(other.gameObject);
                //Debug.Log ("I'm Colliding");
            }
        }
    }



    private string getCollected()
    {
        return collected;
    }



    void OnGUI()
    {
        //GUI.contentColor = Color.clear;

        GUI.skin.font = font;
        style.fontSize = 60;
        style.normal.textColor = Color.white;
        if (!isPlaying && countActive == true)
        {
            GUI.Label(new Rect(Screen.width/2, Screen.height/3, 100, 50), countDownTime.ToString(), style); // display the letter based on it's index key within the word
        }
        GUI.Label(new Rect(Screen.width/2 - 80, Screen.height/3, 100, 50), startMessage, style); // display the letter based on it's index key within the word

        style.fontSize = 55;
        GUI.Label(new Rect(Screen.width / 1.08f, Screen.height/30.0f, 120, 30), lives.ToString(), style); // display the letter based on it's index key within the word
       if (fontReady == true)
        {

            for (int i = 0; i < imageObjects.Length; i++)
            {
                if (imageObjects[i].getCheck() == true) // if the check is set to true, it has been caught and will be printed to screen
                {
                    style.fontSize = 105;

                    GUI.skin.font = spellingFont; // change the font to Futura for the spelling of the word
                    style.normal.textColor = Color.black;
                   // GUI.Label(new Rect(((pos2 * imageObjects[i].getKey())), Screen.height / 1.21f, 100, 50), imageObjects[i].getLetter(), style); // display the letter based on it's index key within the word

                    //GUI.Label(new Rect(((pos * imageObjects[i].getKey()) / 2) + 60, Screen.height / 2.3f, 100, 50), imageObjects[i].getLetter(), style); // display the letter based on it's index key within the word
                    GUI.Label(new Rect(((pos * imageObjects[i].getKey()) / 2) + 30, imagePos.y + 250, 100, 50), imageObjects[i].getLetter(), style); // display the letter based on it's index key within the word

                }
            }
        }


        if (pause == true)
        {
            pauseMessage = "paused";

            GUI.skin.font = font;
            style2.fontSize = 60;
            style2.normal.textColor = Color.white;
            GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 3, 100, 50), pauseMessage, style2); // display the letter based on it's index key within the word
        }
        else
        {
            pauseMessage = "";
            GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 3, 100, 50), pauseMessage, style2); // display the letter based on it's index key within the word
        }




    }

    /***Use these methods to pause and play the game, will pass into pauseButton script***/
    public void pauseGame()
    {
        dropper.pauseDropper();
        setIsPlaying(false);
    }

    public void gamePlaying()
    {
        setIsPlaying(true);
        dropper.startDropper();
        dropper.Spawn(); // start dropping coconuts again
    }
    /**********************************************************************************/

    public void checkLives()
    {
        if (lives == 0)
        {
           // dropper.pauseDropper();
            lives = 0;
            //isPlaying = false;
            Debug.Log("You lose"); // change this later, pause the game or redirect to a new screen asking if they want to play again
        }
    }

}
