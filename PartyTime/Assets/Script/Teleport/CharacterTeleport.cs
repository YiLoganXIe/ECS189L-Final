using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Character;

public class CharacterTeleport : MonoBehaviour
{

    [SerializeField] GameObject DestinationObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Triggered with player");
            var characterLocomotion = collision.gameObject.GetComponent<UltimateCharacterLocomotion>();
            if (characterLocomotion != null)
            {
                characterLocomotion.SetPosition(DestinationObject.transform.position);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
