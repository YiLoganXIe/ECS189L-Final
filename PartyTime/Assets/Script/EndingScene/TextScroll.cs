using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TextScroll : MonoBehaviour
{
    [SerializeField] private GameObject TextObj;
    // endPosition is position where all of text is above the top of the UI CANVAS
    [SerializeField] private Vector3 endPosition;

    // Adjust the speed for the application.
    public float speed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        endPosition = new Vector3(TextObj.transform.position.x, 1800, TextObj.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (TextObj.transform.position.y < endPosition.y)
        {
            // Move our position a step closer to the target.
            float step =  speed * Time.deltaTime; // calculate distance to move
            TextObj.transform.position = new Vector3(TextObj.transform.position.x, TextObj.transform.position.y + (Time.deltaTime * speed), TextObj.transform.position.z);
            // TextObj.transform.position = Vector3.MoveTowards(TextObj.transform.position, endPosition, step);
        }
        else
        {
            Debug.Log("load Splash scene");
            SceneManager.LoadScene("Splash");
        }
    }
}
