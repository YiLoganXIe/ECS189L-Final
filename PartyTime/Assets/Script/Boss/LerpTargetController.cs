using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LerpTargetController : MonoBehaviour
{
    private BossAbsorbingController BossController;
    // Start is called before the first frame update
    void Start()
    {
        this.BossController = this.transform.parent.gameObject.GetComponent<BossAbsorbingController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SuckableParticle"))
        {
            PhotonNetwork.Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("SuckableObject"))
        {
            this.BossController.MoveFromAbsorbingToAbsorbed(other.gameObject);
        }
    }
}
