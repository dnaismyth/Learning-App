using UnityEngine;
using System.Collections;

public class ItchyMode : MonoBehaviour
{

    public Color highlightColor = Color.white;
    //public initialsound_settings settings;
    //static bool selectAllLetters = true;


    void Start()
    {
        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if(PlayerPrefs.GetInt("itchyMode") == 1)
        {
            this.GetComponent<SpriteRenderer>().color = Color.grey;
            this.transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        }
        //settings = GetComponent<letterSettings> ();
    }


    //This button will clear all of the current user choices for the letters
    public void OnMouseDown()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.GetInt("itchyMode") == 0)
        {
            PlayerPrefs.SetInt("itchyMode", 1); // 1 indicates true
            transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
            sprite.color = Color.grey;

        }
        else
        {
            PlayerPrefs.SetInt("itchyMode", 0);
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            sprite.color = Color.white;
        }
            
        PlayerPrefs.Save();

    }

    public void OnMouseUp()
    {
        //transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

    }

}
