using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MonsterController : MonoBehaviourPunCallbacks, IPunObservable
{
    private const float DestroyRadius = 5f;
    private const float MaxSpeed = 1f;
    private const float RotationInterval = 2f;
    private float CurrentRotInterval = 0f;
    private float RotationAngle = 0f;
    private float Health = 20f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.Move();
        this.CauseHarm();
    }

    private void Move()
    {
        if (this.CurrentRotInterval < RotationInterval)
        {
            this.transform.Rotate(new Vector3(0f, Time.deltaTime / RotationInterval * this.RotationAngle, 0f), Space.World);
            this.CurrentRotInterval += Time.deltaTime;
        }
        else
        {
            this.CurrentRotInterval = 0f;
            this.RotationAngle = Random.Range(-30f, 30f);
        }
        
        var speed = Random.Range(0, MaxSpeed);
        var nextPos = this.transform.position + this.transform.forward * speed * Time.deltaTime;
        if (nextPos.x > -50f && nextPos.x < 50f && nextPos.z > -50f && nextPos.z < 50f)
        {
            this.transform.position = nextPos;
        }
    }

    private void CauseHarm()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, DestroyRadius);
        Debug.Log(hitColliders.Length);
        foreach (var c in hitColliders)
        {
            if (c.gameObject.tag == "LightParticle")
            {
                Destroy(c.gameObject);
                Debug.Log("destroy!");
            }
            else if (c.gameObject.tag == "Player")
                // TODO: Harm Player
                continue;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spell")
        {
            Debug.Log("Collide!");
            this.Health -= 2f;
            if (this.Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
