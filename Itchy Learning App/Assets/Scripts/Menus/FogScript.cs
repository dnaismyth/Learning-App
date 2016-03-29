using UnityEngine;
using System.Collections;

public class FogScript : MonoBehaviour {

    void Awake()
    {
        this.GetComponent<EllipsoidParticleEmitter>().emit = true;

    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<ParticleRenderer>().maxParticleSize -= 0.002f;
        if(this.GetComponent<ParticleRenderer>().maxParticleSize < 0)
        {
            this.GetComponent<EllipsoidParticleEmitter>().emit = false; // stop the particle system once it has cleared
        }
	}
}
