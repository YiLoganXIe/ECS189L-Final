using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Monster;
    [SerializeField] private int Amount = 10;

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
        var x = Random.Range(-50f, 50f);
        var z = Random.Range(-50f, 50f);
        var angleY = Random.Range(-180f, 180f);
        Instantiate(this.Monster, new Vector3(x, 5f, z), Quaternion.AngleAxis(angleY, Vector3.up));
    }
}
