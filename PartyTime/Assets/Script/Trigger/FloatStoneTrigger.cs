using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatStoneTrigger : MonoBehaviour
{
    // User Inputs
    [SerializeField] private float degreesPerSecond = 15.0f;
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private bool ShouldBeDestroyed = false;

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.ShouldBeDestroyed)
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }

    public void DeactiveStone()
    {
        this.ShouldBeDestroyed = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        Destroy(this.transform.GetChild(0).gameObject);
    }
}
