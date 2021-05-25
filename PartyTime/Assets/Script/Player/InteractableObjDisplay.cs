using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InteractableObjDisplay : MonoBehaviour
{
    public void ToggleInteractiveTextOn()
    {
        this.gameObject.GetComponent<Text>().enabled = true;
    }

    public void ToggleInteractiveTextOff()
    {
        this.gameObject.GetComponent<Text>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
