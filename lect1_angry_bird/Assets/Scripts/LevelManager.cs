using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState
{
    START, PLAYING, END
}

public class LevelManager : MonoBehaviour
{

    #region Exposed Fields

    [SerializeField]
    private int numberOfRetries;

    [SerializeField]
    private int maximumScore;

    [SerializeField]
    private Bird bird;

    #endregion

    #region Properties

    private static LevelManager _level;

    public static LevelManager Instance
    {
        get { return _level; }
        set { _level = value; }
    }


    private LevelState levelState;

    public LevelState State
    {
        get { return levelState; }
        set { levelState = value; }
    }

    private bool _stateSwitch;

    public bool isSwitchingStates
    {
        get { return _stateSwitch; }
        set { _stateSwitch = value; }
    }

    public int NumberOfRetries
    {
        get
        {
            return numberOfRetries;
        }
        set
        {
            numberOfRetries = value;
        }
    }
    public int MaximumScore
    {
        get
        {
            return maximumScore;
        }
        set
        {
            maximumScore = value;
        }
    }

    #endregion

    #region Private Variables



    #endregion

    private void Start()
    {
        SwitchStates(LevelState.START);
        Instance = this;
    }

    private void Update()
    {
        if (isSwitchingStates) return;
        switch (State)
        {
            case LevelState.START:
                SwitchStates(LevelState.PLAYING);
                break;
            case LevelState.PLAYING:
                if(GameManager.Instance.Score == maximumScore || NumberOfRetries <= 0)
                {
                    SwitchStates(LevelState.END);
                    return;
                }

                if(bird.IsIdleAfterThrow() && State != LevelState.END)
                {
                    StartCoroutine(bird.ResetBird(3));
                }

                if(bird.IsBirdOutOfScene() && State != LevelState.END)
                {
                    StartCoroutine(bird.ResetBird(2));
                }
                break;
            case LevelState.END:
                break;
            default:
                break;
        }
    }


    #region State Management

    public void SwitchStates(LevelState newState, float delay = 0)
    {
        StartCoroutine(SwitchStatesDelay(newState, delay));
    }

    private IEnumerator SwitchStatesDelay(LevelState newState, float delay = 0)
    {
        isSwitchingStates = true;
        yield return new WaitForSeconds(delay);
        EndState();
        State = newState;
        BeginState(newState);
        isSwitchingStates = false;
    }

    private void BeginState(LevelState newState)
    {
        switch (newState)
        {
            case LevelState.START:
                AudioManager.Instance.PlayAudio(AudioState.STARTSOUND);
                break;
            case LevelState.PLAYING:
                break;
            case LevelState.END:
                break;
            default:
                break;
        }
    }

    private void EndState()
    {
        switch (State)
        {
            case LevelState.START:
                break;
            case LevelState.PLAYING:
                break;
            case LevelState.END:
                break;
            default:
                break;
        }
    }

    #endregion
    
}
