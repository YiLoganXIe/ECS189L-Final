using Michsky.DreamOS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameButtonController : MonoBehaviour
{
    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject App;
    [SerializeField] private GameObject AppIcon;
    [SerializeField] private Sprite LockIcon;
    [SerializeField] private Sprite UnLockIcon;
    private TextMeshProUGUI TitleText;
    // Start is called before the first frame update
    void Start()
    {
        TitleText = Title.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TitleText.text = "Unlock!";
            App.GetComponent<DoubleClickEvent>().enabled = true;
            AppIcon.GetComponent<Image>().sprite = UnLockIcon;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TitleText.text = "Unknown";
            App.GetComponent<DoubleClickEvent>().enabled = false;
            AppIcon.GetComponent<Image>().sprite = LockIcon;
        }
    }
    
}
