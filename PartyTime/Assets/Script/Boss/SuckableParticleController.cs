using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SuckableParticleController : MonoBehaviourPun, IOnPhotonViewPreNetDestroy
{
    private bool Absorbed = false;
    private BossAbsorbingController BossController;

    private void OnEnable()
    {
        photonView.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        photonView.RemoveCallbackTarget(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        var boss = GameObject.FindWithTag("Boss");
        this.BossController = boss.GetComponent<BossAbsorbingController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Absorb()
    {
        this.Absorbed = true;
    }


    private void OnDestroy()
    {
        Debug.Log("Destroy a Particle in OnDestroy");
        if (this.Absorbed)
        {
            this.BossController.GetOffAbsorbingList(this.gameObject);
            this.BossController.IncrementParticleNum();
        }
    }

    public void OnPreNetDestroy(PhotonView rootView)
    {
        Debug.Log("Destroy a Particle in OnPreNetDestroy");
        if (this.Absorbed)
        {
            
            this.BossController.GetOffAbsorbingList(this.gameObject);
            this.BossController.IncrementParticleNum();
        }
    }
}
