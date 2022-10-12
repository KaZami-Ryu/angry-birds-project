using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Init,
    Playing,
    LevelEnd,
}
public class GameManager : MonoBehaviour
{
    #region Exposed Fields

    [Tooltip("Game Objects")]

    [SerializeField]
    private List<GameObject> levelPrefabs = new List<GameObject>();


    [Tooltip("Panels")]

    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject gamePanel;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    #endregion

    #region Properties

    private static GameManager gameManager;

    public static GameManager Instance
    {
        get { return gameManager; }
        set { gameManager = value; }
    }

    private GameState gameStates;

    public GameState State
    {
        get { return gameStates; }
        set { gameStates = value; }
    }

    private bool _stateSwitch;

    public bool isSwitchingStates
    {
        get { return _stateSwitch; }
        set { _stateSwitch = value; }
    }

    private int _level;

    public int Level
    {
        get { return _level; }
        set { _level = value; }
    }

    private int _score;

    public int Score
    {
        get { return _score; }
        set
        { 
            _score = value;
            scoreText.text = "SCORE: " + _score.ToString();
        }
    }

    private bool levelEnd;

    public bool isLevelEnded
    {
        get { return levelEnd; }
        set { levelEnd = value; }
    }


    #endregion

    #region Private Variables

    private GameObject currentLevelInstance;

    #endregion


    void Start()
    {
        isLevelEnded = false;
        Instance = this;
        SwitchStates(GameState.MainMenu);
    }

    void Update()
    {
        if (isSwitchingStates) return;
        switch (State)
        {
            case GameState.MainMenu:
                break;
            case GameState.Init:
                SwitchStates(GameState.Playing);
                break;
            case GameState.Playing:
                if (LevelManager.Instance.State == LevelState.END) 
                {
                    SwitchStates(GameState.LevelEnd, 3);
                }
                break;
            case GameState.LevelEnd:
                SwitchStates(GameState.Init, 2);
                break;
            default:
                break;
        }
    }

    #region State Management

    private void SwitchStates(GameState newState, float delay = 0)
    {
        StartCoroutine(SwitchStatesDelay(newState, delay));
    }

    private IEnumerator SwitchStatesDelay(GameState newState, float delay = 0)
    {
        isSwitchingStates = true;
        yield return new WaitForSeconds(delay);
        EndState();
        State = newState;
        BeginState(newState);
        isSwitchingStates = false;
    }

    private void BeginState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                mainMenuPanel.SetActive(true);
                break;
            case GameState.Init:
                Score = 0;
                isLevelEnded = false;
                break;
            case GameState.Playing:
                if(Level < levelPrefabs.Count)
                {
                    currentLevelInstance = Instantiate(levelPrefabs[Level]);
                }
                break;
            case GameState.LevelEnd:
                Level++;
                if(Level >= levelPrefabs.Count)
                {
                    Level = 0;
                }
                gamePanel.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void EndState()
    {
        switch (State)
        {
            case GameState.MainMenu:
                mainMenuPanel.SetActive(false);
                break;
            case GameState.Init:
                gamePanel.SetActive(true);
                break;
            case GameState.Playing:
                currentLevelInstance.SetActive(false);
                break;
            case GameState.LevelEnd:
                Destroy(currentLevelInstance);
                break;
            default:
                break;
        }
    }

    #endregion

    #region ButtonClicks

    public void StartGame()
    {
        SwitchStates(GameState.Init);
    }

    #endregion

}
