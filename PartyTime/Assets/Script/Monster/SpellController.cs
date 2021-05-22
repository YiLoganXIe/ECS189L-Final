using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    private Vector3 MaxScale = new Vector3(10f, 10f, 10f);
    private Vector3 InitScale;
    private float Interval = 1f;
    private float CurrentInterval = 0.0f;
    private GameObject Owner;

    // Start is called before the first frame update
    void Start()
    {
        this.InitScale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.CurrentInterval > this.Interval)
        {
            // Spell ends
            Destroy(this.gameObject);
        }
        else
        {
            this.CurrentInterval += Time.deltaTime;
            this.gameObject.transform.localScale = Vector3.Lerp(this.InitScale, this.MaxScale, this.CurrentInterval/this.Interval);
            if (Owner)
            {
                this.transform.position = this.Owner.transform.position;
            }
        }
    }

    public void SetOwner(GameObject owner)
    {
        this.Owner = owner;
    }
}
