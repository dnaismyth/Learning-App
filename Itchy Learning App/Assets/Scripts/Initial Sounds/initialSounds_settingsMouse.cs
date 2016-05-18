using UnityEngine;
using System.Collections;

public class initialSounds_settingsMouse : MonoBehaviour {

    SpriteRenderer sprite;
    [SerializeField]
    private initialsound_settings settings;
    [SerializeField]
    private GameObject letter;
    private selectAll allSelected = new selectAll(); // initialize an instance of the selectAll class
    private int id;
    private string thisLetter;
    private int avail;
    private int[] userChoices; // an array to store the letters which a user would like to use in the game
                               // Use this for initialization


    public int getId()
    {
        return id;
    }

    public string getLetterName()
    {
        return thisLetter;
    }

    public void setId(int input)
    {
        id = input;
    }

    public void OnMouseDown()
    {
        allSelected.setBoolSelectAll(false); // this will now trigger the selectAll boolean to be false, therefore instead using players preferences
        sprite = GetComponent<SpriteRenderer>();
        // call the lettersChosen method from the letterSettings screen, the input will refer to the current button being clicked.
        settings.lettersChosen(this);
       

        if (settings.hasAlready() == true)
        {
            settings.setHasAlready();
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            sprite.color = Color.white;
            settings.lettersChosen(this);
            Debug.Log("Has already");
        }
        else {
            this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            sprite.color = Color.grey;
            Debug.Log(this.getId());
        }



    }

    public void setLetter(Sprite image, int letterId, string l_name)
    {
        id = letterId;
        thisLetter = l_name;
        GetComponent<SpriteRenderer>().sprite = image;
 
    }

    
}


