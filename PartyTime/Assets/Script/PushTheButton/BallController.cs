using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Record how much force will be appied to ball each button click.
    [SerializeField] private float PushForce;

    private bool GameOver;

    // Start is called before the first frame update
    void Start()
    {
        this.PushForce = 10;
        this.GameOver = false;
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

        if (Input.GetButtonDown("Fire1"))
        {
            // Push to the right.
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -this.PushForce));
        }

        if (Input.GetButtonDown("Fire2"))
        {
            // Push to the left.
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, this.PushForce));
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
            Debug.Log("Game over! Reached right");
            this.GameOver = true;
        }
    }

    private void ResetGame()
    {
        var position = this.gameObject.transform.position;
        this.gameObject.transform.position = new Vector3(position.x, position.y, 0);
        this.GameOver = false;
        Debug.Log("Game has been reset.");
    }
}
