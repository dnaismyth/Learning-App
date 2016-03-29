using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {
    public RectTransform settingsPos;
    private GameObject parent;

	// Use this for initialization
	void Start () {
        Debug.Log(settingsPos.sizeDelta.ToString());
    }

    // Update is called once per frame
    void Update () {
	
	}

    void Awake()
    {
        //parent = this.transform.parent.gameObject;
        settingsPos = settingsPos.transform.GetComponent<RectTransform>();
    }

    public void settingsDown()
    {

        settingsPos.sizeDelta.Set(settingsPos.sizeDelta.x, settingsPos.sizeDelta.y + 500);
        Debug.Log(settingsPos.sizeDelta.ToString());
    }
}
