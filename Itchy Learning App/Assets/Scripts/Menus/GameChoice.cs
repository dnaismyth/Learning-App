using UnityEngine;
using System.Collections;

public class GameChoice : MonoBehaviour {
    public ButtonScroll bs;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseDown()
    {
        Debug.Log("I am clicked");
        bs.setIsScrolling(false);
    }
}
