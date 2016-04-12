using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class letterButton : MonoBehaviour
{
    public Color highlight = Color.grey;

    public void OnMouseEnter()
    {

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlight;
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
        if (PlayerPrefs.GetInt("currentGame") == 1)
        {
          SceneManager.LoadScene(0);       //redirect to initialsoundssetting screen, if have time, change this later
        }
        else
        {
            SceneManager.LoadScene(6);
        }
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
    public void OnMouseUp()
    {
        transform.localScale = Vector3.one;

    }

}

