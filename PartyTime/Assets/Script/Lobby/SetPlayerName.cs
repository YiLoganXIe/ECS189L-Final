using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class SetPlayerName : MonoBehaviour
{
    const string playerNamePrefKey = "PlayerName";
    [SerializeField] GameObject FirstName;
    [SerializeField] GameObject LastName;

    public void setName()
    {
        string first = FirstName.GetComponent<TMPro.TMP_InputField>().text;
        string second = LastName.GetComponent<TMPro.TMP_InputField>().text;
        Debug.Log($"Name:{first} {second}");
        PhotonNetwork.NickName = first + " " + second;
        PlayerPrefs.SetString(playerNamePrefKey, first + second);
    }
}
