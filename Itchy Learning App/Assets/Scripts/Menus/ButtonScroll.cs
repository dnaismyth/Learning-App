using UnityEngine;
using System.Collections;

public class ButtonScroll : MonoBehaviour
{
    private Vector3 myPos;
    private Vector3 top;
    private bool isScrolling = true;
    public GameObject buttons;
    public Sprite[] buttonImages;
    private Vector3[] bPos;
    private GameObject[] allButtons;
    public float speed = 0.4f;
    private Vector3 initialPosition;
    // Use this for initialization

    void Awake()
    {
        for(int i = 0; i <= 25; i++)
        {
            PlayerPrefs.SetInt("initial_letter" + i, i);
            PlayerPrefs.SetInt("letter" + i, i);
        }
        for(int j = 0; j <=4; j++)
        {
            PlayerPrefs.SetInt("vowel_letter" + j, j);
        }
        for(int k = 0; k <= 11; k++)
        {

            PlayerPrefs.SetInt("final_letter" + k, k);
          
        }
        /*Vector3 temp = this.transform.position;
        temp.y = -3;
        buttons.transform.position = temp;*/
        initialPosition = buttons.transform.position;
        createButtons();
        PlayerPrefs.Save();
   
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        getIsScrolling();       // check if a button has been clicked, and scrolling is stopped
        if (isScrolling == true)
        {
            scrollButtons();
        }
    }

    void createButtons()
    {
        allButtons = new GameObject[buttonImages.Length];
        bPos = new Vector3[buttonImages.Length];
        for (int i = 0; i < buttonImages.Length; i++)
        {
            GameObject currButton;
            if (i == 0)
            {
                currButton = buttons;
                bPos[i] = initialPosition;

                allButtons[i] = currButton;
                //(allButtons[i]) as GameObject;
            }
            else
            {
                currButton = Instantiate(buttons) as GameObject;
                currButton.GetComponent<SpriteRenderer>().sprite = buttonImages[i];
                initialPosition.y = initialPosition.y + 3;
                bPos[i] = initialPosition;
      
                currButton.transform.position = initialPosition;
                allButtons[i] = currButton;
            }
         
        }
        myPos = allButtons[buttonImages.Length-1].transform.position;
    }

    public bool getIsScrolling()
    {
        return isScrolling;
    }

    public void setIsScrolling(bool b)
    {
        isScrolling = b;
    }
    

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5);
    }

    void scrollButtons()
    {
      
        for (int i = 0; i < bPos.Length; i++)
        {
            bPos[i].y -= Time.deltaTime * speed;
            allButtons[i].transform.position = bPos[i];

            if (Camera.main.WorldToScreenPoint(bPos[i]).y < -7)
            {
                bPos[i].y = myPos.y;        // return to the original position
            }
        }      
    }

    void OnDestroy()
    {
        
        PlayerPrefs.Save();
      
    }

}
