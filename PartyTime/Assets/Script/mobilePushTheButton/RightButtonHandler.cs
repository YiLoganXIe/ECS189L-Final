using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileBallGame
{
    public class RightButtonHandler : MonoBehaviour, IButtonHandler
    {
        public GameObject ball;
        
        public void MoveBall()
        {
            // Push to the left.
            Debug.Log("Right button pushes left");
            this.ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, this.ball.GetComponent<BallController>().getPushForce()));
        }
    }
}
