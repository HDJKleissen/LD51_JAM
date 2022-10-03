using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    public MenuHandler menuHandler;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {

        PauseMenu.SetActive(!PauseMenu.activeInHierarchy);
        Time.timeScale = PauseMenu.activeInHierarchy ? 0 : 1;
    }
}
