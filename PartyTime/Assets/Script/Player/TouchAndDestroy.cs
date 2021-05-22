using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TouchAndDestroy : MonoBehaviourPun
{
    [PunRPC]
    private void DestroyMonster(PhotonMessageInfo info)
    {
        Debug.Log($"Destroy the Monster from {info.Sender}.");
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("In collision.");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collide Player.");
            //var test = collision.gameObject.GetPhotonView().ViewID;
            photonView.RPC("DestroyMonster", RpcTarget.All);
        }
        
    }
}
