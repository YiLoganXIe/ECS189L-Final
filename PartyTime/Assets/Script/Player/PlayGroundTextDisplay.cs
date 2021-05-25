using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGroundTextDisplay : MonoBehaviour
{
    private GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.Player == null)
        {
            this.Player = GameObject.FindWithTag("Player");
        }
        else
        {
            this.gameObject.GetComponent<Text>().text = "x" + this.Player.GetComponent<PlayerController>().GetNumPlayerParticles();
        }
    }
}
