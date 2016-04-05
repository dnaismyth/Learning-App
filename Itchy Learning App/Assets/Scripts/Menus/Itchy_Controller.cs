using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Itchy_Controller : MonoBehaviour {

    // Use this for initialization
    private GameObject gameO;
    Text instruction;
    private bool textFinished = false;
	void Start () {
        instruction = gameObject.GetComponent<Text>();
        gameO = this.gameObject.transform.GetChild(0).gameObject;
        StartCoroutine(AnimateText()); // call the text animation function
	}
	
    /**Method used to print out one letter at a time for scrolling text effect**/
    IEnumerator AnimateText()
    {
        int i = 0;
        string str = "Ahoy, welcome to Itchy's Adventures.  Follow me and we'll check out the island of knowledge... be prepared to have some fun!";
        while (i < str.Length)
        {
            instruction.text += str[i++];
     
            yield return new WaitForSeconds(0.1f);
            if(i == str.Length)
            {
                yield return new WaitForSeconds(0.8f);
                textFinished = true;
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (textFinished)
        {
            instruction.CrossFadeAlpha(0, 0.5f, true); // once the text has finished printing to screen, fade out the panel
            Camera.main.orthographicSize -= 0.05f; // zoom in and exit into a new scene *change this*
            gameO.GetComponent<Animator>().enabled = false; // stop itchy from talking
            if(Camera.main.orthographicSize < 0) // once the camera has zoomed in enough, divert to the island menu
            {
                SceneManager.LoadScene(6);
            }
        }
    }
}
