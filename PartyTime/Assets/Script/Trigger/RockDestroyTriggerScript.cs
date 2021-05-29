using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;


public class RockDestroyTriggerScript : MonoBehaviour
{
    private bool isInRange = false;
    private bool animating = false;
    private bool animationComplete = false;

    [SerializeField] private GameObject BlockingCaveRock;
    [SerializeField] private float Radius;
    private GameObject RockDestroyTrigger;

    [SerializeField] private GameObject postProcProfile;
    private PostProcessVolume volume;
    private Grain volGrain;
    private ColorGrading volCG;
    
    private float startTime;
    [SerializeField] private float IntensityLerpSpeed = 0.1f;
    [SerializeField] private float SaturationLerpSpeed = 3f;
    private float startIntensity;
    [SerializeField] private float endIntensity = 0f;
    private float startSaturation;
    [SerializeField] private float endSaturation = -70f;

    // Start is called before the first frame update
    void Start()
    {
        // this.postProcProfile = GameObject.Find("Global Post Processing");
        this.volume = postProcProfile.GetComponent<PostProcessVolume>();
        if (!volume.profile.TryGetSettings(out this.volGrain))
        {
            Debug.Log("volumeGrain set incorrectly");
        }
        if (!volume.profile.TryGetSettings(out this.volCG))
        {
            Debug.Log("volCG set incorrectly");
        }
            
        // this.BlockingCaveRock = GameObject.Find("BlockingCaveRock");
        this.RockDestroyTrigger = this.gameObject.transform.Find("RockDestroyTrigger").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        this.isInRange = checkPlayerNum();
        if (this.isInRange && !this.animating)
        {
            // Debug.Log("destroying trigger and rock!");
            Destroy(this.BlockingCaveRock);
            Destroy(this.RockDestroyTrigger);

            this.animating = true;
            this.startTime = Time.time;
            this.startIntensity = this.volGrain.intensity.value;
            this.startSaturation = this.volCG.saturation.value;
            Debug.Log("starting animation: " + this.startTime);
        }
        if (this.animating)
        {
            if (this.volGrain.intensity.value != this.endIntensity)
            {
                // Distance moved equals elapsed time times speed..
                float distCovered = (Time.time - this.startTime) * this.IntensityLerpSpeed;

                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / (this.endIntensity - this.startIntensity);

                this.volGrain.intensity.value = Mathf.Lerp(this.volGrain.intensity.value, this.endIntensity, fractionOfJourney);
            }
            if (this.endIntensity - this.volGrain.intensity.value <= 0.1)
            {
                this.volGrain.intensity.value = Mathf.Lerp(this.volGrain.intensity.value, this.endIntensity, 1);
            }

            if (this.volCG.saturation.value != this.endSaturation)
            {
                // Distance moved equals elapsed time times speed..
                float distCovered = (Time.time - this.startTime) * this.SaturationLerpSpeed;

                // Fraction of journey completed equals current distance divided by total distance.
                float fractionOfJourney = distCovered / (this.endSaturation - this.startSaturation);

                this.volCG.saturation.value = Mathf.Lerp(this.volCG.saturation.value, this.endSaturation, fractionOfJourney);
            }
            if (this.endSaturation - this.volCG.saturation.value <= 0.1)
            {
                this.volCG.saturation.value = Mathf.Lerp(this.volCG.saturation.value, this.endSaturation, 1);
            }
            
            if (this.endIntensity == this.volGrain.intensity.value)
            {
                Debug.Log("finished intensity animation in : " + (Time.time - this.startTime));
            }
            if (this.endSaturation == this.volCG.saturation.value)
            {
                Debug.Log("finished saturation animation in : " + (Time.time - this.startTime));
            }
            this.animationComplete = ((this.volGrain.intensity.value == this.endIntensity) && (this.volCG.saturation.value == this.endSaturation));
        }
        if (this.animationComplete)
        {
            Debug.Log("animation completed in : " + (Time.time - this.startTime));
            // Destroy(this.gameObject);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter for RockDestroyTrigger!");
        Debug.Log(other);
        if (other.gameObject.CompareTag("PlayerCollider"))
        {
            Debug.Log("Player entered radius of RockDestroyTrigger");

            this.isInRange = true;
        }
    }
    */


    private bool checkPlayerNum()
    {
        int counter = 0;
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, 50);
        foreach (var collider in colliders){
            Debug.Log("Sphere Detect" + collider);
        }
        foreach (var player in Players)
        {
            if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) <= this.Radius)
                counter++;
        }
        return counter == 2;
    }
}
