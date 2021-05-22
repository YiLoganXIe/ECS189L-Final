using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private CapsuleCollider CapsuleCollider;
    

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
        
    }

    public int GetNumPlayerParticles()
    {
        return this.numParticles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision);
        if (collision.gameObject.tag == "Particle")
        {
            Destroy(collision.gameObject);
            this.numParticles++;
            Debug.Log($"numParticles: {this.numParticles}");
        }
    }
}
