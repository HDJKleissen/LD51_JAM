using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class InfoBox : MonoBehaviour
{
    public GameObject title, message, acceptButton, declineButton;

    public InfoBox()
    {

    }

    public static void CreateYesPopUp(string title, string msg, string acceptButton, Action onYesClick)
    {
        GameObject popup = Resources.Load<GameObject>("Popups/YesPopUp");
        InfoBox infoBox = Instantiate(popup).GetComponent<InfoBox>();
        Debug.Assert(infoBox != null, "Prefab file not found");
        infoBox.title.GetComponent<TMP_Text>().text = title;
        infoBox.message.GetComponent<TMP_Text>().text = msg;
        infoBox.acceptButton.GetComponentInChildren<TMP_Text>().text = acceptButton;

        infoBox.acceptButton.GetComponent<Button>().onClick.AddListener(delegate { Debug.Log("CLICKED"); });
        infoBox.acceptButton.GetComponent<Button>().onClick.AddListener(delegate { Destroy(infoBox.gameObject); });
        infoBox.acceptButton.GetComponent<Button>().onClick.AddListener(delegate { onYesClick(); });
        infoBox.acceptButton.GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.stateMachine = GameManager.StateMachine.InGame; });

        GameManager.Instance.stateMachine = GameManager.StateMachine.PopUp;
    }

    public static GameObject CreateTemporaryPopUp(string title, string msg)
    {
        GameObject popup = Resources.Load<GameObject>("Popups/YesPopUp");
        InfoBox infoBox = Instantiate(popup).GetComponent<InfoBox>();
        Debug.Assert(infoBox != null, "Prefab file not found");
        infoBox.title.GetComponent<TMP_Text>().text = title;
        infoBox.message.GetComponent<TMP_Text>().text = msg;
        infoBox.acceptButton.SetActive(false);

        return infoBox.gameObject;
    }

    public static void CreateYesNoPopUp(string title, string msg, string acceptText, string declineText, Action onYesClick, Action onNoClick)
    {
        GameObject popup = Resources.Load<GameObject>("Popups/YesNoPopUp");
        InfoBox infoBox = Instantiate(popup).GetComponent<InfoBox>();
        infoBox.title.GetComponent<TMP_Text>().text = title;
        infoBox.message.GetComponent<TMP_Text>().text = msg;
        infoBox.acceptButton.GetComponentInChildren<TMP_Text>().text = acceptText;
        infoBox.declineButton.GetComponentInChildren<TMP_Text>().text = declineText;

        infoBox.acceptButton.GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.stateMachine = GameManager.StateMachine.InGame; });
        infoBox.acceptButton.GetComponent<Button>().onClick.AddListener(delegate { onYesClick(); });
        infoBox.acceptButton.GetComponent<Button>().onClick.AddListener(delegate { Destroy(infoBox.gameObject); });

        infoBox.declineButton.GetComponent<Button>().onClick.AddListener(delegate { GameManager.Instance.stateMachine = GameManager.StateMachine.InGame; });
        infoBox.declineButton.GetComponent<Button>().onClick.AddListener(delegate { onNoClick(); });
        infoBox.declineButton.GetComponent<Button>().onClick.AddListener(delegate { Destroy(infoBox.gameObject); });

        GameManager.Instance.stateMachine = GameManager.StateMachine.PopUp;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
