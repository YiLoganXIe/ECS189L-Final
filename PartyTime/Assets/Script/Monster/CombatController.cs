using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private GameObject Spell;
    private Camera Camera;
    // Start is called before the first frame update
    void Awake()
    {
        this.Camera = this.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }
    
    private void Attack()
    {
        var pos = this.gameObject.transform.position;
        var spell = Instantiate(this.Spell, new Vector3(pos.x, pos.y + 1, pos.z), Quaternion.identity);
        spell.GetComponent<SpellController>().SetOwner(this.gameObject);
    }
}
