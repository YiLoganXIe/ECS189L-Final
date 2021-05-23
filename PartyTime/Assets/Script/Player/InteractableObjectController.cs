using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectController : MonoBehaviour
{
    [SerializeField] private float radius = 5.0f;
    private Renderer c_renderer;
    private BoxCollider boxcollider;

    private bool isInteractable = false;

    private GameObject InteractableObjectTextDisplay;

    // Start is called before the first frame update
    void Start()
    {
        this.c_renderer = this.gameObject.GetComponent<Renderer>();
        this.boxcollider = this.gameObject.GetComponent<BoxCollider>();

        this.boxcollider.size *= this.radius;

        this.InteractableObjectTextDisplay = GameObject.Find("InteractableObjectTextDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isInteractable && Input.GetButtonDown("Item Interact"))
        {
            Debug.Log("Input - F detected!");
            this.c_renderer.material.color = UnityEngine.Random.ColorHSV();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(collision);
        if (other.tag == "Player")
        {
            Debug.Log("Player entered radius");

            this.InteractableObjectTextDisplay.GetComponent<InteractableObjTextDisplay>().ToggleInteractiveTextOn();
            this.isInteractable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player has left the radius");

            this.InteractableObjectTextDisplay.GetComponent<InteractableObjTextDisplay>().ToggleInteractiveTextOff();
            this.isInteractable = false;
        }
    }
}

