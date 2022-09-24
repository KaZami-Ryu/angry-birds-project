using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioState
{
    STARTSOUND, CRATEHIT, MONSTERHIT, STRECH, THROW
}

public class AudioManager : MonoBehaviour
{
    #region Exposed Fields

    [SerializeField]
    private List<Sound> sounds;

    #endregion

    #region Properties

    private static AudioManager audioManager;

    public static AudioManager Instance
    {
        get { return audioManager; }
        set { audioManager = value; }
    }

    private AudioState audioState;

    public AudioState State
    {
        get { return audioState; }
        set { audioState = value; }
    }

    private bool _stateSwitch;

    public bool isSwitchingStates
    {
        get { return _stateSwitch; }
        set { _stateSwitch = value; }
    }


    #endregion

    #region Private Variables

    private int currentPlayingIndex;

    #endregion


    void Start()
    {
        Instance = this;
    }

    #region Audio Management


    public void PlayAudio(AudioState newState)
    {
        currentPlayingIndex = FindAudio(newState);
        switch (newState)
        {
            case AudioState.STARTSOUND:
                sounds[currentPlayingIndex].Source.Play();
                break;
            case AudioState.CRATEHIT:
                sounds[currentPlayingIndex].Source.Play();
                break;
            case AudioState.MONSTERHIT:
                sounds[currentPlayingIndex].Source.Play();
                break;
            case AudioState.STRECH:
                sounds[currentPlayingIndex].Source.Play();
                break;
            case AudioState.THROW:
                sounds[currentPlayingIndex].Source.Play();
                break;
            default:
                break;
        }
    }

    #endregion


    #region Helper Functions

    private int FindAudio(AudioState _state)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            if(sounds[i].State == _state)
            {
                return i;
            }
        }
        return 0;
    }

    #endregion
}
