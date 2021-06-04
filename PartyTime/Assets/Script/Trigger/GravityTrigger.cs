using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{
    [SerializeField] private float LerpAhead;

    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float LowerDownDistance;
    
    private bool PlayerDetected;
    private GameObject ParentObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            Debug.Log("On Rock!");
            this.PlayerDetected = true;
            ParentObj.transform.position = Vector3.Lerp(ParentObj.transform.position, this.endPosition, this.LerpAhead * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            Debug.Log("Left Rock!");
            this.PlayerDetected = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.ParentObj = this.gameObject.transform.parent.gameObject;
        this.initialPosition = ParentObj.transform.position;
        var position = this.initialPosition;
        this.endPosition = new Vector3(position.x, position.y - this.LowerDownDistance, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.PlayerDetected && this.ParentObj.transform.position != this.initialPosition)
        {
            this.ParentObj.transform.position = Vector3.Lerp(this.ParentObj.transform.position, this.initialPosition, this.LerpAhead * Time.deltaTime);
        }
        else if (this.PlayerDetected)
        {
            this.ParentObj.transform.position = Vector3.Lerp(this.ParentObj.transform.position, this.endPosition, this.LerpAhead * Time.deltaTime);
        }
    }
}
