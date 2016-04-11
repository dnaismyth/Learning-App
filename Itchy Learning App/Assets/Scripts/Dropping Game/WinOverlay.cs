using UnityEngine;
using System.Collections;

public class WinOverlay : MonoBehaviour
{
    protected bool isShowing = false;
    public CatchController cc;
    public GameObject menu; // Assign in inspector
    public AudioClip congratulations;
    AudioSource playAudio;

    public void Awake()
    {
      
    }
    public void Update()
    {
       
        if (cc.getShowCanvas() == true)
        {
            isShowing = !isShowing;
        }
        if (isShowing)
        {
            menu.SetActive(isShowing);
            /*if (!GetComponent<ParticleSystem>().isPlaying)
            {
                GetComponent<ParticleSystem>().Play();
                menu.SetActive(isShowing);
                playAudio.PlayOneShot(congratulations);
            }*/
        }
        else
        {
            menu.SetActive(isShowing);
            /*if (GetComponent<ParticleSystem>().isPlaying)
            {
                GetComponent<ParticleSystem>().Stop();
                menu.SetActive(isShowing);
            }*/
        }

    }


}