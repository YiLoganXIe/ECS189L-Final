using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class CountDownController : MonoBehaviourPun
{
    [SerializeField] private int MaxNum = 69;
    private int CurrentNum;
    [SerializeField] private float Interval = 1.4f;
    private float ElapsedTime = 0f;
    private bool InGame = false;
    private bool ButtonPressed = false;
    [SerializeField] private GameObject TextNumber;
    [SerializeField] private GameObject StartButton;

    // Chains
    [SerializeField] GameObject Chain1;
    [SerializeField] GameObject Chain2;
    [SerializeField] GameObject Chain3;
    [SerializeField] GameObject Chain4;
    [SerializeField] GameObject Chain5;
    [SerializeField] GameObject Chain6;
    [SerializeField] GameObject Chain7;
    private List<GameObject> Chains;

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentNum = this.MaxNum;

        this.Chains = new List<GameObject>();
        this.Chains.Add(Chain1);
        this.Chains.Add(Chain2);
        this.Chains.Add(Chain3);
        this.Chains.Add(Chain4);
        this.Chains.Add(Chain5);
        this.Chains.Add(Chain6);
        this.Chains.Add(Chain7);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.InGame)
        {
            if (CurrentNum == 0)
            {
                // Success.
                this.Success();
                return;
            }

            if (this.ElapsedTime >= this.Interval)
            {
                if (this.CheckCorrectAction())
                {
                    // Next number.
                    CurrentNum--;
                    this.TextNumber.GetComponent<TextMeshProUGUI>().text = this.CurrentNum.ToString();
                    this.ElapsedTime = 0f;
                }
                else
                {
                    // Game over.
                    this.Failure();
                    return;
                }
            }
            else
            {
                this.ElapsedTime += Time.deltaTime;
            }
        }
    }

    private bool CheckCorrectAction()
    {
        bool isCorrect = true;

        if (this.CurrentNum % 10 == 7 || this.CurrentNum % 7 == 0)
        {
            if (this.ButtonPressed)
            {
                // Player pushed the button at the right time.
                if (this.CurrentNum % 10 == 7)
                {
                    // Destroy one of the chains.
                    photonView.RPC("RemoveChain", RpcTarget.All, this.CurrentNum / 10);
                }
            }
            else
            {
                // Player failed to push the button. 
                isCorrect = false;
            }

        }
        else if (this.ButtonPressed)
        {
            // Player pushed the button at a wrong time. 
            isCorrect = false;
        }

        this.ButtonPressed = false;
        return isCorrect;
    }

    private void Success()
    {
        Debug.Log("Unlock Succeeded!");
        this.InGame = false;

        photonView.RPC("DropLock", RpcTarget.All);
    }

    private void Failure()
    {
        Debug.Log("Unlock Failed at umber!" + this.CurrentNum);
        this.InGame = false;
        this.ElapsedTime = 0f;
        this.CurrentNum = this.MaxNum;

        this.TextNumber.GetComponent<TextMeshProUGUI>().text = this.CurrentNum.ToString();
        this.StartButton.SetActive(true);

        photonView.RPC("ResetChains", RpcTarget.All);
    }


    [PunRPC]
    private void RemoveChain(int chainNum)
    {
        this.Chains[chainNum].SetActive(false);
    }

    [PunRPC]
    private void DropLock()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        var padLock = this.gameObject.transform.GetChild(0);
        padLock.GetComponent<Rigidbody>().useGravity = true;
    }

    [PunRPC]
    private void ResetChains()
    {
        foreach (var chain in this.Chains)
        {
            chain.SetActive(true);
        }
    }


    public void StartGame()
    {
        this.InGame = true;
        this.ButtonPressed = false;
        this.StartButton.SetActive(false);
        Debug.Log("Game Starts!");
    }

    public void PressButton()
    {
        this.ButtonPressed = true;
    }
}
