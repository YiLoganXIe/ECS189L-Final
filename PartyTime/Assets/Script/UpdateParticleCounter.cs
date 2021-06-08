using Michsky.DreamOS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateParticleCounter : MonoBehaviour
{
    private GameObject playerRef;
    [SerializeField] private int num;
    [SerializeField] private GameObject Notification;
    [SerializeField] private GameObject NoteToggleObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnDestroy()
    {
        playerRef.GetComponent<PlayerController>().addNumParticles();
        Notification.GetComponent<NotificationCreator>().CreateNotification();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRef = collision.gameObject;
            Destroy(this.gameObject);
            NoteToggleObject.GetComponent<NoteToggle>().setStatus(true);
            // increment user's particle count --> use OnDestroy script on Particle
        }
    }
}
