using System.Collections;
using System.Collections.Generic;
using Michsky.DreamOS;
using Photon.Pun;
using UnityEngine;

public class SuckableParticleController : MonoBehaviourPun, IOnPhotonViewPreNetDestroy
{
    private bool Absorbed = false;
    private BossAbsorbingController BossController;
    private GameObject Notification1;
    private GameObject Notification2;
    private GameObject Notification3;
    private void OnEnable()
    {
        photonView.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        photonView.RemoveCallbackTarget(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        var boss = GameObject.FindWithTag("Boss");
        this.BossController = boss.GetComponent<BossAbsorbingController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Notification1 == null)
        {
            Notification1 = GameObject.Find("BossHit1");
        }
        if (Notification2 == null)
        {
            Notification2 = GameObject.Find("BossHit2");
        }
        if (Notification3 == null)
        {
            Notification3 = GameObject.Find("BossHit3");
        }
    }

    public void Absorb()
    {
        this.Absorbed = true;
    }


    private void OnDestroy()
    {
        Debug.Log("Destroy a Particle in OnDestroy");
        float choice = Random.Range(0, 10);
        if (choice <=3)
            Notification1.GetComponent<NotificationCreator>().CreateNotification();
        else if (choice <= 6)
            Notification2.GetComponent<NotificationCreator>().CreateNotification();
        else
            Notification2.GetComponent<NotificationCreator>().CreateNotification();
        if (this.Absorbed)
        {
            this.BossController.GetOffAbsorbingList(this.gameObject);
            this.BossController.IncrementParticleNum();
        }
    }

    public void OnPreNetDestroy(PhotonView rootView)
    {
        Debug.Log("Destroy a Particle in OnPreNetDestroy");
        float choice = Random.Range(0, 10);
        if (choice <= 3)
            Notification1.GetComponent<NotificationCreator>().CreateNotification();
        else if (choice <= 6)
            Notification2.GetComponent<NotificationCreator>().CreateNotification();
        else
            Notification2.GetComponent<NotificationCreator>().CreateNotification();
        if (this.Absorbed)
        {
            
            this.BossController.GetOffAbsorbingList(this.gameObject);
            this.BossController.IncrementParticleNum();
        }
    }
}
