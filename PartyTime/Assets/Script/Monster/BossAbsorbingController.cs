using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbsorbingController : MonoBehaviour
{
    private List<GameObject> AbsorbingObjects = new List<GameObject>();
    private List<float> CurLerpTimes = new List<float>();
    [SerializeField] private float LerpAhead = 10f;
    private float DescendingTime = 2;
    private float DesDistance = 150;
    private float CurTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var obj in this.AbsorbingObjects)
        {
            this.Absorb(obj);
        }
        if (CurTime < DescendingTime)
        {
            this.transform.position -= new Vector3(0f, Time.deltaTime / this.DescendingTime * DesDistance, 0f);
            CurTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger From Boss!");
        if (other.gameObject.CompareTag("SuckableObject"))
        {
            this.AddToAbsorbingList(other.gameObject);
            Debug.Log("Collide with Suckable!");
        }
    }

    private void AddToAbsorbingList(GameObject obj)
    {
        if (!this.AbsorbingObjects.Contains(obj))
        {
            this.AbsorbingObjects.Add(obj);
            this.CurLerpTimes.Add(0f);
        }
    }

    private void Absorb(GameObject obj)
    {
        var curPosition = obj.transform.position;
        var nextPosition = Vector3.Lerp(curPosition, this.transform.position, 1f / this.LerpAhead * Time.deltaTime);
        obj.transform.position = nextPosition;
    }
}
