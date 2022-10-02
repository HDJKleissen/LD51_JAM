using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject OptionsScreen;

    public void GoToScene(string scene)
    {
        SceneController.Instance.TransitionToScene("Level 1");
    }

    private void DisableAllMenuStates()
    {
        MainMenu.SetActive(false);
        OptionsScreen.SetActive(false);
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
        MainMenu.SetActive(false);
        OptionsScreen.SetActive(true);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
