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


    #endregion

    #region Private Variables


    #endregion


    void Start()
    {
        Instance = this;
    }

    #region Audio Management


    public void PlayAudio(AudioState newState)
    {
        foreach (var sound in sounds)
        {
            if (sound.State == newState)
            {
                sound.Source.Play();
            }
        }
    }

    #endregion


    #region Helper Functions


    #endregion
}
