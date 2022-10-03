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
        Time.timeScale = 1;
        SceneController.Instance.TransitionToScene(scene);
    }

    private void DisableAllMenuStates()
    {
        MainMenu?.SetActive(false);
        OptionsScreen?.SetActive(false);
    }

    public void ToMainMenu()
    {
        DisableAllMenuStates();
        MainMenu?.SetActive(true);
    }
    
    public void ToOptionsScreen()
    {
        MainMenu?.SetActive(false);
        OptionsScreen?.SetActive(true);

    }

    public void Quit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
