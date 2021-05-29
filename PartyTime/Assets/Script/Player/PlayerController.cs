using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    private Rigidbody rb;

    // normal capsule collider that hugs player's body -- good for bumping into particles to collect them
    private CapsuleCollider CapsuleCollider;

    [SerializeField] private GameObject suckableParticlePrefab;

    [SerializeField] private int numParticles = 0;

    [SerializeField] private float ParticleSpawnUpwardForce;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.gameObject.GetComponent<Rigidbody>();
        this.CapsuleCollider = this.gameObject.transform.Find("Colliders/CapsuleCollider").GetComponent<CapsuleCollider>();
    }

    [PunRPC]
    private void SpawnLightParticle(Vector3 position, PhotonMessageInfo info)
    {
        Debug.Log($"{info.Sender} spawn the suckable particle.");
        var particle = Instantiate(this.suckableParticlePrefab, position, Quaternion.identity);
        particle.GetComponent<Rigidbody>().AddForce(new Vector3(0, this.ParticleSpawnUpwardForce, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PlaceParticle"))
        {
            var generationPosition = transform.position + (transform.forward * 2);
            photonView.RPC("SpawnLightParticle", RpcTarget.All, generationPosition);
        }
    }

    public int GetNumPlayerParticles()
    {
        return this.numParticles;
    }

    public void addNumParticles()
    {
        numParticles += 1;
    }

    public void minusNumParticles()
    {
        numParticles -= 1;
    }

}
