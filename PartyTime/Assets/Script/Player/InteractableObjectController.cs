using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectController : MonoBehaviour
{
    [SerializeField] private float radius = 5.0f;
    private BoxCollider boxcollider;
    private SphereCollider radiusTrigger;

    private bool isInRange = false;

    private GameObject InteractableObjectTextDisplay;

    // Start is called before the first frame update
    void Start()
    {
        // obj box collider will be affected by physics system
        this.boxcollider = this.gameObject.GetComponent<BoxCollider>();
        
        // sphere trigger will not be affected by physics system
        this.radiusTrigger = this.gameObject.transform.Find("Trigger").GetComponent<SphereCollider>();
        // set trigger's radius based on desired value for obj
        this.radiusTrigger.radius = this.radius;

        // retrieve Canvas Text UI for text display
        this.InteractableObjectTextDisplay = GameObject.Find("InteractableObjectTextDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isInRange && Input.GetButtonDown("Item Interact"))
        {
            Debug.Log("Input - F detected!");
            this.gameObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter!");
        Debug.Log(other);
        Debug.Log(other.gameObject.tag + other.gameObject.name);
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            Debug.Log("Player entered radius");

            this.InteractableObjectTextDisplay.GetComponent<InteractableObjDisplay>().ToggleInteractiveTextOn();
            this.isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit!");
        Debug.Log(other);
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            Debug.Log("Player has left the radius");

            this.InteractableObjectTextDisplay.GetComponent<InteractableObjDisplay>().ToggleInteractiveTextOff();
            this.isInRange = false;
        }
    }
}

