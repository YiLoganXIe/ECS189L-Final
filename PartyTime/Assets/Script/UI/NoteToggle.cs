using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteToggle : MonoBehaviour
{
    [SerializeField] private string NoteName;
    private GameObject note;
    private bool status = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (note == null)
            note = GameObject.Find(NoteName);
        if (note != null)
            note.SetActive(status);
    }

    public void setStatus(bool s)
    {
        status = s;
    }
}
