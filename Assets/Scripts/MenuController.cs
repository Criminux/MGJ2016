using UnityEngine;
using System.Collections;
using System;

public class MenuController : MonoBehaviour
{
    public enum States
    {
        MainMenu,
        GamePlay,
        GameOver
    }

    private static MenuController instance;
    public static MenuController Instance { get { return instance; } }

    private States currentGameState;

    public States CurrentGameState
    {
        get { return currentGameState; }
        set { currentGameState = value; }
    }

    public Transform MainMenuPlay;
    public Transform MainMenuQuit;
    public TextMesh MainMenuHighscore;

    public TextMesh IngameScore;

    public TextMesh GameOverHighscore;
    public TextMesh GameOverScore;
    public Transform GameOverRestart;
    public Transform GameOverQuit;

    public GameObject MainMenuGroup;
    public GameObject GamePlayGroup;
    public GameObject GameOverGroup;

    [SerializeField]
    float activeDistance;
    float passiveDistance;

    [SerializeField]
    float activeDistanceGameOver;
    float passiveDistanceGameOver;

    int selectedButton;
    private float previousSelection;
    private float previousSelectionGameOver;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentGameState = States.MainMenu;
        passiveDistance = MainMenuPlay.transform.localPosition.y;
        SetButtonPosition(MainMenuPlay, activeDistance);
    }

    void Update()
    {
        switch (currentGameState)
        {
            case States.MainMenu:
                MainMenu();
                break;
            case States.GamePlay:
                GamePlay();
                break;
            case States.GameOver:
                GameOver();
                break;
        }
    }

    private void GameOver()
    {
        float playerSelection = Input.GetAxis("VerticalControllerMovement");
        //print(playerSelection);
        if (playerSelection < -0.2f && previousSelectionGameOver > -0.2f)
        {
            selectedButton++;
            if (selectedButton > 1)
                selectedButton = 0;
            UpdateSelectionGameOver();
        }
        if (playerSelection > 0.2f && previousSelectionGameOver < 0.2f)
        {
            selectedButton--;
            if (selectedButton < 0)
                selectedButton = 1;
            UpdateSelectionGameOver();
        }
        previousSelectionGameOver = playerSelection;

        if (Input.GetAxis("Fire") != 0)
        {
            if (selectedButton == 0)
            {
                GameManager.Instance.Restart();
                GamePlayGroup.SetActive(true);
                currentGameState = States.GamePlay;
            }
            else
                Application.Quit();
        }
    }

    private void UpdateSelectionGameOver()
    {
        SetButtonPosition(GameOverRestart, passiveDistanceGameOver);
        SetButtonPosition(GameOverQuit, passiveDistanceGameOver);

        switch (selectedButton)
        {
            case 0:
                SetButtonPosition(GameOverRestart, activeDistanceGameOver);
                break;
            case 1:
                SetButtonPosition(GameOverQuit, activeDistanceGameOver);
                break;
        }
    }

    private void GamePlay()
    {

    }

    private void MainMenu()
    {
        float playerSelection = Input.GetAxis("VerticalControllerMovement");
        //print(playerSelection);
        if (playerSelection < -0.2f && previousSelection > -0.2f)
        {
            selectedButton++;
            if (selectedButton > 1)
                selectedButton = 0;
            UpdateSelectionMainMenu();
        }
        if (playerSelection > 0.2f && previousSelection < 0.2f)
        {
            selectedButton--;
            if (selectedButton < 0)
                selectedButton = 1;
            UpdateSelectionMainMenu();
        }
        previousSelection = playerSelection;

        if (Input.GetAxis("Fire") != 0)
        {
            if (selectedButton == 0)
            {
                GameManager.Instance.Restart();
                GamePlayGroup.SetActive(true);
                currentGameState = States.GamePlay;
            }
            else
                Application.Quit();
        }
    }

    private void UpdateSelectionMainMenu()
    {
        SetButtonPosition(MainMenuPlay, passiveDistance);
        SetButtonPosition(MainMenuQuit, passiveDistance);

        switch (selectedButton)
        {
            case 0:
                SetButtonPosition(MainMenuPlay, activeDistance);
                break;
            case 1:
                SetButtonPosition(MainMenuQuit, activeDistance);
                break;
        }
    }

    private void SetButtonPosition(Transform button, float distance)
    {
        button.transform.localPosition = new Vector3(button.transform.localPosition.x, distance, button.transform.localPosition.z);
    }
}
