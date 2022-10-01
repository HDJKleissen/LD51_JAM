using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{

    GameObject mainMenu;
    [HideInInspector] public bool gameOver { get; private set; } = false;

    [field: SerializeField] public Camera Camera { get; private set; }

    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public GameObject World { get; private set; }
    [HideInInspector] public List<RobotSpawner> RobotSpawners = new List<RobotSpawner>();


    //statemachine should be a stack for more complex UI states prob with transitions etc
    public enum StateMachine
    {
        InMenu,
        InGame,
        InSceneTransition,
        PopUp,
    }
    public StateMachine stateMachine = StateMachine.InMenu;

    // Start is called before the first frame update
    void Start()
    {

        InitGame();
    }

    //gameManager is not destroyed on reload, so make sure to reset everything on scenechange
    //nasty way to reload references..
    public void InitGame()
    {
        mainMenu = GameObject.Find("MainMenuCanvas").transform.GetChild(0).gameObject;
        Camera = GameObject.FindObjectOfType<Camera>();
        gameOver = false;
        stateMachine = StateMachine.InGame;
    }

    void Update()
    {
         //escape to go to menu and return
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (stateMachine == StateMachine.InMenu)
            {
                stateMachine = StateMachine.InGame;
                mainMenu.SetActive(false);
            }
            else
            {
                stateMachine = StateMachine.InMenu;
                mainMenu.SetActive(true);

            }
        }

        if (gameOver)
            return;

        if(stateMachine == StateMachine.InGame)
        {

        }
    }

    public IEnumerator GameOver()
    {
        if (gameOver)
            yield break;

        gameOver = true;

        yield return new WaitForSeconds(1.5f);
        mainMenu.SetActive(true);
    }

    protected override void Awake()
    {
        //Make this game object persistent
        DontDestroyOnLoad(gameObject);


        base.Awake();
    }


}
