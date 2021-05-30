using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckableParticleController : MonoBehaviour
{
    private bool Absorbed = false;
    private BossAbsorbingController BossController;

    // Start is called before the first frame update
    void Start()
    {
        var boss = GameObject.FindWithTag("Boss");
        this.BossController = boss.GetComponent<BossAbsorbingController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Absorb()
    {
        this.Absorbed = true;
    }

    private void OnDestroy()
    {
        if (this.Absorbed)
        {
            this.BossController.GetOffAbsorbingList(this.gameObject);
            this.BossController.IncrementParticleNum();
        }
    }
}
