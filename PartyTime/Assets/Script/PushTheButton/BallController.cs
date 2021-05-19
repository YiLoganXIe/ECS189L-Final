using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Photon.Pun;

public class BallController : MonoBehaviourPun
{
    // Record how much force will be appied to ball each button click.
    [SerializeField] private float PushForce = 10f;
    [SerializeField] private float RebounceInterval = 0.4f;

    private bool GameOver;
    private float TimeCounter;
    private float AccelerationTimeCounter;
    private bool AtStart = true;

    // Start is called before the first frame update
    void Start()
    {
        this.GameOver = false;
        this.TimeCounter = 0.0f;
        this.AccelerationTimeCounter = 0.0f;
    }

    private void OnValidate()
    {
        this.PushForce = Mathf.Abs(this.PushForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.ResetGame();
            this.GameOver = false;
        }

        if (this.GameOver) return;

        
        this.TimeCounter += Time.deltaTime;
        if (this.TimeCounter > this.RebounceInterval && !this.AtStart)
        {
            // Apply automatical counterforce for a certain period with acceleration.
            this.Rebounce(1 + Mathf.Pow(this.AccelerationTimeCounter, 2) / 10);
            this.TimeCounter = 0.0f;
        }

        if (!this.AtStart)
        {
            this.AccelerationTimeCounter += Time.deltaTime;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            photonView.RPC("AddForce", RpcTarget.All);
        }
    }

    [PunRPC]
    private void AddForce()
    {
        Debug.Log("Add force");
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, this.PushForce));
        this.AccelerationTimeCounter = 0.0f;
    }

    private void Rebounce(float multiplier = 1.0f)
    {
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, multiplier * (-this.PushForce)));
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RightWall")
        {
            this.AtStart = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LeftWall")
        {
            //Destroy(collision.gameObject);
            //this.Mushrooms++;
            Debug.Log("Game over! Reached left");
            this.GameOver = true;
        }
        else if (collision.gameObject.tag == "RightWall")
        {
            this.AtStart = true;
        }
    }

    private void ResetGame()
    {
        var position = this.gameObject.transform.position;
        this.gameObject.transform.position = new Vector3(position.x, position.y, -4.25f);
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GameOver = false;
        this.AtStart = true;
        this.TimeCounter = 0.0f;
        this.AccelerationTimeCounter = 0.0f;
        Debug.Log("Game has been reset.");
    }
}
