using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BossAbsorbingController : MonoBehaviour
{
    private List<GameObject> AbsorbingObjects = new List<GameObject>();
    private List<GameObject> AbsorbedObjects = new List<GameObject>();
    [SerializeField] private float LerpAhead = 1.25f;
    [SerializeField] private GameObject LerpTarget;
    [SerializeField] private float TargetRadius = 5f;

    [SerializeField] private int ParticleThreshold = 2;
    private int ParticleNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.ParticleNum >= this.ParticleThreshold)
        {
            Destroy(this.gameObject);
        }
        this.Absorb();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("On Trigger From Boss!");
       
        if (other.gameObject.CompareTag("SuckableObject") || other.gameObject.CompareTag("SuckableParticle"))
        {
            this.AddToAbsorbingList(other.gameObject);
            if (other.gameObject.CompareTag("SuckableParticle"))
            {
                other.gameObject.GetComponent<SuckableParticleController>().Absorb();
            }
        }
    }

    private void AddToAbsorbingList(GameObject obj)
    {
        if (!this.AbsorbingObjects.Contains(obj))
        {
            this.AbsorbingObjects.Add(obj);
        }
    }

    public void MoveFromAbsorbingToAbsorbed(GameObject obj)
    {
        this.AbsorbingObjects.Remove(obj);
        this.AbsorbedObjects.Add(obj);
    }

    private void Absorb()
    {
        // Still lerping.
        foreach (var obj in this.AbsorbingObjects)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, this.LerpTarget.transform.position, this.LerpAhead * Time.deltaTime);
        }

        // Stick to the target.
        foreach (var obj in this.AbsorbedObjects)
        {
            obj.transform.position = this.LerpTarget.transform.position;
        }

        //int index = 0;
        //while(index < this.AbsorbingObjects.Count)
        //{
        //    var obj = this.AbsorbingObjects[index];

        //    var curPosition = obj.transform.position;
        //    obj.transform.position = Vector3.Lerp(curPosition, this.LerpTarget.transform.position, this.LerpAhead * Time.deltaTime);
        //if ((curPosition - this.LerpTarget.transform.position).magnitude > this.TargetRadius)
        //{
        //    // Not close enough to the LerpPoint, continue lerping.

        //}

        //else if (obj.CompareTag("SuckableParticle"))
        //{
        //    // Particle close enough to the LerpPoint, Destroy.
        //    this.ParticleNum++;
        //    Debug.Log($"Boss has absorbed a Particle! Total particles absorbed: {this.ParticleNum}");
        //    this.AbsorbingObjects.Remove(obj);
        //    PhotonNetwork.Destroy(obj);

        //    if (this.ParticleNum >= this.ParticleThreshold)
        //    {
        //        Destroy(this.gameObject);
        //        return;
        //    }

        //    index += 2;
        //    continue;
        //}
        //else
        //{
        //    // Object close enough to the LerpPoint, stick to it.
        //    obj.transform.position = this.LerpTarget.transform.position;
        //}
        //    index++;
        //}
    }

    public void IncrementParticleNum()
    {
        this.ParticleNum++;
        Debug.Log($"Boss has absorbed a Particle! Total particles absorbed: {this.ParticleNum}");
    }

    public void GetOffAbsorbingList(GameObject obj)
    {
        this.AbsorbingObjects.Remove(obj);
    }
}
