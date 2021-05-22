using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParticleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject LightParticle;
    [SerializeField] private int Amount = 100;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.Amount; i++)
        {
            RandomSpawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomSpawn()
    {
        var x = Random.Range(-100f, 100f);
        var z = Random.Range(-100f, 100f);
        //var y = Random.Range(f, 1.3f);
        Instantiate(this.LightParticle, new Vector3(x, 50f, z), Quaternion.identity);
    }
}
