using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BossAbsorbingController : MonoBehaviour
{
    private List<GameObject> AbsorbingObjects = new List<GameObject>();
    [SerializeField] private float LerpAhead = 1f;
    [SerializeField] private GameObject LerpTarget;
    [SerializeField] private float TargetRadius = 5f;

    [SerializeField] private int ParticleThreshold = 10;
    private int ParticleNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Absorb();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("On Trigger From Boss!");
       
        if (other.gameObject.CompareTag("SuckableObject") || other.gameObject.CompareTag("SuckableParticle"))
        {
            if (other.gameObject.CompareTag("SuckableObject"))
            {
                if (Random.Range(0, 10) < 2)
                    this.AddToAbsorbingList(other.gameObject);

            }
            else
                this.AddToAbsorbingList(other.gameObject);
        }
    }

    private void AddToAbsorbingList(GameObject obj)
    {
        if (!this.AbsorbingObjects.Contains(obj))
        {
            this.AbsorbingObjects.Add(obj);
        }
    }

    private void Absorb()
    {
        int index = 0;
        while(index < this.AbsorbingObjects.Count)
        {
            var obj = this.AbsorbingObjects[index];

            var curPosition = obj.transform.position;
            if ((curPosition - this.LerpTarget.transform.position).magnitude > this.TargetRadius)
            {
                // Not close enough to the LerpPoint, continue lerping.
                obj.transform.position = Vector3.Lerp(curPosition, this.LerpTarget.transform.position, this.LerpAhead * Time.deltaTime);
            }
            else if (obj.CompareTag("SuckableParticle"))
            {
                // Particle close enough to the LerpPoint, Destroy.
                this.ParticleNum++;
                Debug.Log($"Boss has absorbed a Particle! Total particles absorbed: {this.ParticleNum}");
                this.AbsorbingObjects.Remove(obj);
                Destroy(obj);

                if (this.ParticleNum >= this.ParticleThreshold)
                {
                    Destroy(this.gameObject);
                    return;
                }

                index += 2;
                continue;
            }
            else
            {
                // Object close enough to the LerpPoint, stick to it.
                obj.transform.position = this.LerpTarget.transform.position;
            }

            index++;
        }
    }
}
