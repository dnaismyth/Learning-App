using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
  
    void Start()
    {

    }

    void Awake()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
     
        Renderer rend = GetComponent<Renderer>();
        rend.material.mainTextureOffset = new Vector2(0f, Time.time * 0.05f);
    }

    // setter for scrolling
  /* public void setScrolling(bool b)
    {
        scrolling = b;
    }

    void streamPosition()
    {
       
        s2Pos.y += 1.5f * -1 * Time.deltaTime;
        s1Pos.y += 1.5f * -1 * Time.deltaTime;
    
        stream2.transform.position = s2Pos;
        stream1.transform.position = s1Pos;
        Vector3 tmpPos1 = Camera.main.WorldToScreenPoint(stream1.transform.position);
        Vector3 tmpPos2 = Camera.main.WorldToScreenPoint(stream2.transform.position);
        Debug.Log(tmpPos1.y);
        if (tmpPos1.y < -Screen.height/0.915f)
        {
            s1Pos = restart;      // move the sprite back to the top of the screen
            stream1.transform.position = s1Pos;

        }
        else if (tmpPos2.y < -Screen.height/0.915f)/*-Screen.height/0.915f*/
        /*{
            s2Pos = restart;
            stream2.transform.position = s2Pos;  // move the sprite back to the top of the screen
        }
    }*/

}
