using UnityEngine;
using System.Collections;

public class SpeedScroll : MonoBehaviour {
    public ButtonScroll scrollerSettings; // call the buttonscroll to set the speed
    private bool increaseSpeed = false; // initially set increased speed to false
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        getSpeedBoost(); // check if the speed has been increased on update
	}

    public void OnMouseDown()
    {
        speedClicked();
        Debug.Log("Im clicked speed");
    }

    public void speedClicked()
    {
        if (increaseSpeed == false)
        {
            increaseSpeed = true;   // set increase speed to true to speed up button scroll
        }
        else if (increaseSpeed == true)
        {
            increaseSpeed = false; // set the speed off on a second touch
        }

        speedScroll();
    }

    // method to increase the speed of the scrolling buttons by double, will be called on button click
    public void speedScroll()
    {

        if (increaseSpeed == true)
        {
            scrollerSettings.speed = 1.3f;   // if clicked, increase the speed to 1.3
        }
        else if (increaseSpeed == false)
        {
            scrollerSettings.speed = 0.4f;   // reduce speed on a double touch
        }
    }

    private bool getSpeedBoost()
    {
        return increaseSpeed;
    }
}
