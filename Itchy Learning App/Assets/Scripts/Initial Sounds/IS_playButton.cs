using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IS_playButton : MonoBehaviour {

    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private string targetMessage;
    public Color highlightColor;
    [SerializeField]
    private initialsound_settings checkEmpty; // create an instance of letterSettings so we can retrieve the list of id's to be added

    void Awake()
    {
        checkEmpty = checkEmpty.GetComponent<initialsound_settings>();
    }


    public void OnMouseEnter()
    {

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor;
        }
    }
    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        print(checkEmpty.getIsEmpty());
        PlayerPrefs.Save();
        if (checkEmpty.getIsEmpty() == false)
        {
            if (PlayerPrefs.GetInt("currentGame") != 0) // change this
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("currentGame")); // corresponds to initial sounds game
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
        else
            print("pick some letters before starting");


    }
    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }

    
}



