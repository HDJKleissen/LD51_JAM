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

    public void InitGame()
    {
        mainMenu = GameObject.Find("MainMenuCanvas").transform.GetChild(0).gameObject;
        Camera = GameObject.FindObjectOfType<Camera>();
        stateMachine = StateMachine.InMenu;
    }

    void Update()
    {
        if(Camera == null)
        {
            InitGame();
        }

    }

    public IEnumerator GameOver()
    {
        if (gameOver)
            yield break;

        stateMachine = StateMachine.InMenu;

        yield return new WaitForSeconds(1.5f);
        
        //todo
    }

    protected override void Awake()
    {

        base.Awake();
    }


}
