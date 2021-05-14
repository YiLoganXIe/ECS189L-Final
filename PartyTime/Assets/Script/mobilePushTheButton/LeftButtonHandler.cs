using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MobileBallGame
{
    public class LeftButtonHandler : MonoBehaviour, IButtonHandler
    {
        public GameObject ball;
        
        public void MoveBall()
        {
            // Push to the right.
            Debug.Log("left button pushes right");
            this.ball.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -this.ball.GetComponent<BallController>().getPushForce()));
        }
    }
}
