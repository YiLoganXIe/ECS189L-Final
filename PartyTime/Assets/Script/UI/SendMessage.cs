using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.DreamOS;

public class SendMessage : MonoBehaviour
{
    public MessagingManager messagingApp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void m_SendMessage()
    {
        messagingApp.InitializeChat();
        messagingApp.CreateMessage(null, 0, "Message content");
    }
}
