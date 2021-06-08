using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LightTowerCamTrigger : MonoBehaviour
{
    private bool triggered = false;
    private float t = 0;
    private bool inCollider = false;
    private float speed = 1.0f;

    [SerializeField] private GameObject EndCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inCollider)
        {   
            t += Time.deltaTime;
            if (t < 4)
            {
                // Determine which direction to rotate towards
                Vector3 targetDirection = EndCam.transform.position - Camera.main.transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(Camera.main.transform.forward, targetDirection, singleStep, 0.0f);


                // Calculate a rotation a step closer to the target and applies rotation to this object
                Camera.main.transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else
            {
                // inCollider = false;
                // triggered = true;
                // var cameraController = Camera.main.GetComponent<Opsive.UltimateCharacterController.Camera.CameraController>();
                // cameraController.enabled = true;
                Debug.Log("load ending scene");
                SceneManager.LoadScene("Ending");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") && (!triggered))
        {
            // toggle off player for camera panning
            other.gameObject.SetActive(false);

            var cameraController = Camera.main.GetComponent<Opsive.UltimateCharacterController.Camera.CameraController>();
            cameraController.enabled = false;
            this.inCollider = true;
        }
    }
}
