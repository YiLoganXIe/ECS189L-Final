using UnityEngine;
using Photon.Pun;

public class LookAtBoss : MonoBehaviourPun
{
    private bool triggered = false;
    private float t = 0;
    private bool inCollider = false;
    [SerializeField] GameObject Boss;
    // Angular speed in radians per sec.
    public float speed = 1.0f;

    private void Start()
    {
        this.SetEnableComponents(false);
    }

    private void SetEnableComponents(bool isEnable = false)
    {
        if (Boss.GetComponent<MeshRenderer>().enabled != isEnable)
        {
            Boss.GetComponent<MeshRenderer>().enabled = isEnable;
        }

        if (Boss.GetComponent<CapsuleCollider>().enabled != isEnable)
        {
            Boss.GetComponent<CapsuleCollider>().enabled = isEnable;
        }

        if (Boss.GetComponent<BossMovement>().enabled != isEnable)
        {
            Boss.GetComponent<BossMovement>().enabled = isEnable;
        }

        if (Boss.GetComponent<BossAbsorbingController>().enabled != isEnable)
        {
            Boss.GetComponent<BossAbsorbingController>().enabled = isEnable;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && inCollider)
        {
            t += Time.deltaTime;
            if (t < 4)
            {
                // Determine which direction to rotate towards
                Vector3 targetDirection = Boss.transform.position - Camera.main.transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(Camera.main.transform.forward, targetDirection, singleStep, 0.0f);


                // Calculate a rotation a step closer to the target and applies rotation to this object
                Camera.main.transform.rotation = Quaternion.LookRotation(newDirection);
            }
            //Camera.main.transform.LookAt(Boss.transform);
            else
            {
                inCollider = false;
                triggered = true;
                var cameraController = Camera.main.GetComponent<Opsive.UltimateCharacterController.Camera.CameraController>();
                cameraController.enabled = true;
            }
        }
    }

    [PunRPC]
    private void EnableBoss()
    {
        this.SetEnableComponents(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "PlayerCollider") && (!triggered))
        {
            var cameraController = Camera.main.GetComponent<Opsive.UltimateCharacterController.Camera.CameraController>();
            cameraController.enabled = false;
            this.inCollider = true;

            photonView.RPC("EnableBoss", RpcTarget.All);
        }
    }
}
