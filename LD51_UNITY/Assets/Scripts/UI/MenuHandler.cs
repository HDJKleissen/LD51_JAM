using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject OptionsScreen;
    [SerializeField] GameObject PauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToPauseMenu();
        }
    }

    private void GoToPauseMenu()
    {
        if(GameManager.Instance.stateMachine != GameManager.StateMachine.InMenu)
        {
            GameManager.Instance.stateMachine = GameManager.StateMachine.InMenu;
            PauseScreen.SetActive(true);
        }
    }

    private void DisableAllMenuStates()
    {
        MainMenu.SetActive(false);
        OptionsScreen.SetActive(false);
        PauseScreen.SetActive(false);
    }

    public void ToMainMenu()
    {
        GameManager.Instance.stateMachine = GameManager.StateMachine.InMenu;
        DisableAllMenuStates();
        MainMenu.SetActive(true);
    }
    
    public void ToOptionsScreen()
    {
        GameManager.Instance.stateMachine = GameManager.StateMachine.InMenu;
        OptionsScreen.SetActive(true);
    }

    public void ToPauseScreen()
    {
        GameManager.Instance.stateMachine = GameManager.StateMachine.InMenu;
        DisableAllMenuStates();
        PauseScreen.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.Instance.InitGame();
    }

    public void StartGame()
    {
        DisableAllMenuStates();

        //go to scene or change
        GameManager.Instance.stateMachine = GameManager.StateMachine.InGame;
    }

    public void Resume()
    {
        GameManager.Instance.stateMachine = GameManager.StateMachine.InGame;
        PauseScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
