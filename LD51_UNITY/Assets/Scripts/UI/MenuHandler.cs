using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject OptionsScreen;

    public void GoToScene(string scene) {
        SceneManager.LoadScene(scene);
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
        OptionsScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
