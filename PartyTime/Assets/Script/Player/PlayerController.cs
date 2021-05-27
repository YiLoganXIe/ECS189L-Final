using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    // normal capsule collider that hugs player's body -- good for bumping into particles to collect them
    private CapsuleCollider CapsuleCollider;

    [SerializeField] private GameObject suckableParticlePrefab;

    [SerializeField] private int numParticles = 0;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.gameObject.GetComponent<Rigidbody>();
        this.CapsuleCollider = this.gameObject.transform.Find("Colliders/CapsuleCollider").GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PlaceParticle"))
        {
            Instantiate(this.suckableParticlePrefab, transform.position + (transform.forward * 2), Quaternion.identity);
        }
    }

    public int GetNumPlayerParticles()
    {
        return this.numParticles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision);
        if (collision.gameObject.CompareTag("Particle"))
        {
            Destroy(collision.gameObject);
            // increment user's particle count --> use OnDestroy script on Particle
        }
    }
}
