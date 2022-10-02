using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIIngame : MonoBehaviour
{
    [SerializeField] Transform itemList;
    [SerializeField] GameObject ItemPrefab;
    Player player;

    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject VictoryScreen;

    // Update is called once per frame
    void Update()
    {
        UpdateUI();



        //enable pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToPauseMenu();
        }

    }

    private void Start()
    {
        //quick fix
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    void UpdateUI()
    {


        foreach(Transform t in itemList)
        {
            Destroy(t.gameObject);
        }

        foreach (Collectable c in player.CollectedItems)
        {
            GameObject ItemUI = Instantiate(ItemPrefab, itemList);
            ItemUI.GetComponentInChildren<Image>().sprite = c.UIImage;
        }
    }


    public void GoToPauseMenu()
    {
        if (GameManager.Instance.stateMachine != GameManager.StateMachine.InMenu)
        {
            GameManager.Instance.stateMachine = GameManager.StateMachine.InMenu;
            PauseScreen.SetActive(true);
        }
    }

    public void ResumeFromPauseScreen()
    {
        GameManager.Instance.stateMachine = GameManager.StateMachine.InGame;
        PauseScreen.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GoToVictoryScreen()
    {
        GameManager.Instance.stateMachine = GameManager.StateMachine.InMenu;
        VictoryScreen.SetActive(true);
    }
}
