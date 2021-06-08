using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeNote : MonoBehaviour
{
    private GameObject Player;
    private GameObject Note;
    private bool step1 = false;
    private bool step2 = false;
    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.FindWithTag("Player");
        this.Note = GameObject.FindGameObjectWithTag("QuestNote");
    }

    // Update is called once per frame
    void Update()
    {
        if (step1)
        {
            if (this.Player == null)
            {
                this.Player = GameObject.FindWithTag("Player");
            }
            else
            {
                if (Note == null)
                {
                    this.Note = GameObject.FindGameObjectWithTag("QuestNote");
                }
                else
                {
                    //Debug.Log(Note.GetComponent<TextMeshProUGUI>().text);
                    Note.GetComponent<TMP_InputField>().text = $"Collect Memory Shards: {this.Player.GetComponent<PlayerController>().GetNumPlayerParticles()}";
                }
            }
        }
    }

    public void openNote()
    {
        if (!step1)
            step1 = true;
    }
}
