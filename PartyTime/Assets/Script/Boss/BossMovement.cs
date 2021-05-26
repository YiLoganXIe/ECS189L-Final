using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private GameObject Destination1;
    [SerializeField] private GameObject Destination2;
    [SerializeField] private GameObject Destination3;
    [SerializeField] private GameObject Destination4;
    [SerializeField] private float RotationSpeed;
    // Movement speed in units per second.
    [SerializeField] private float speed = 1.0F;

    private int currentDestination = 0;
    private int previousDestination = 3;
    private List<GameObject> DestinationList;
    // Time when the movement started.
    private float startTime;
    // Total distance between the markers.
    private float journeyLength;
    // Start is called before the first frame update

    void Start()
    {
        DestinationList = new List<GameObject>()
        {
            Destination1,
            Destination2,
            Destination4,
            Destination3
        };

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(this.gameObject.transform.position, DestinationList[currentDestination].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject CurDestination = DestinationList[currentDestination];
        if (this.gameObject.transform.position != CurDestination.transform.position)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(this.gameObject.transform.position, CurDestination.transform.position, fractionOfJourney);
        }
        if (Vector3.Distance(this.gameObject.transform.position, DestinationList[currentDestination].transform.position) < 0.1)
        {
            Debug.Log("-------------------Reached----------------------------");

            this.previousDestination = this.currentDestination;
            this.currentDestination += 1;
            if (currentDestination > 3)
                currentDestination = 0;

            // Keep a note of the time the movement started.
            startTime = Time.time;

            // Calculate the journey length.
            journeyLength = Vector3.Distance(this.gameObject.transform.position, DestinationList[currentDestination].transform.position);
        }
        // Determine which direction to rotate towards
        Vector3 targetDirection = CurDestination.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = RotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }


}
