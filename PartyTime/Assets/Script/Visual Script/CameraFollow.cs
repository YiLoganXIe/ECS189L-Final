using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Camera main_camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var m_camera = this.gameObject.GetComponent<Camera>();
        this.gameObject.transform.position = main_camera.transform.position;
        this.gameObject.transform.rotation = main_camera.transform.rotation;
        m_camera.fieldOfView = main_camera.fieldOfView;
    }
}
