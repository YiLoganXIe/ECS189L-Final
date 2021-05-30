using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateParticleCounter : MonoBehaviour
{
    private GameObject playerRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        playerRef.GetComponent<PlayerController>().addNumParticles();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRef = collision.gameObject;
            Destroy(this.gameObject);
            // increment user's particle count --> use OnDestroy script on Particle
        }
    }
}
