using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PushButtonGameController : MonoBehaviourPunCallbacks, IPunObservable
{
    private int AccumlatedPushSinceLastSync = 0;
    private BallController BallController;
    
    public PushButtonGameController(BallController ballController)
    {
        this.BallController = ballController;
    }

    void Start()
    {
        // TODO: Change to dynamic binding.
        var cube = GameObject.Find("Cube");
        this.BallController = cube.GetComponent<BallController>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player; send our data.
            stream.SendNext(this.AccumlatedPushSinceLastSync);
            this.AccumlatedPushSinceLastSync = 0;
        }
        else
        {
            this.AccumlatedPushSinceLastSync = (int)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Pressed!");
                this.AccumlatedPushSinceLastSync += 1;
                this.BallController.AddForce();
            }
        }
        else
        {
            // TODO: Correct the physics of the pushing force.
            // TODO: Race condition on AccumulatedPushSinceLastSync.
            this.BallController.AddForce(this.AccumlatedPushSinceLastSync);
            Debug.LogFormat("Sync %d pushes at one shot", this.AccumlatedPushSinceLastSync);
            this.AccumlatedPushSinceLastSync = 0;
        }
    }
}
