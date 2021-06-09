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
                    Note.GetComponent<TMP_InputField>().text = $"Collected Memory Shards: {this.Player.GetComponent<PlayerController>().GetNumPlayerParticles()} \n" +
                        $"Find a way to escape this area";
                }
            }
        }
        if (step2)
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
                    Note.GetComponent<TMP_InputField>().text = $"Collected Memory Shards: {this.Player.GetComponent<PlayerController>().GetNumPlayerParticles()} \n" +
                        $"Press G to cast memory shards";
                }
            }
        }
        
    }

    public void openNote()
    {
        if (!step1)
            step1 = true;
    }

    public void BossFight()
    {
        step2 = true;
        step1 = false;
    }
}
