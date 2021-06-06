using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownController : MonoBehaviour
{
    private int CurrentNum = 69;
    [SerializeField] private float Interval = 0.7f;
    private float ElapsedTime = 0f;
    private bool InGame = false;
    private bool ButtonPressed = false;
    [SerializeField] private GameObject TextNumber;

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
                    this.TextNumber.GetComponent<UnityEngine.UI.Text>().text = this.CurrentNum.ToString();
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

    public void StartGame()
    {
        this.InGame = true;
        Debug.Log("Game Starts!");
    }

    public void PressButton()
    {
        this.ButtonPressed = true;
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
                    this.RemoveChain(this.CurrentNum / 10);
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

    private void RemoveChain(int chainNum)
    {
        this.Chains[chainNum].SetActive(false);
    }

    private void Success()
    {
        this.InGame = false;
        Debug.Log("Unlock Succeeded!");
    }

    private void Failure()
    {
        Debug.Log("Unlock Failed at umber!" + this.CurrentNum);
        this.InGame = false;
        this.CurrentNum = 69;
        this.TextNumber.GetComponent<UnityEngine.UI.Text>().text = this.CurrentNum.ToString();
        this.ElapsedTime = 0f;
    }
}
