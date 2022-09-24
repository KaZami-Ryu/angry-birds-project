using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{

    [SerializeField]
    private AudioState state;

    public AudioState State
    {
        get { return state; }
    }

    [SerializeField]
    private AudioSource source;

    public AudioSource Source
    {
        get { return source; }
        set { source = value; }
    }
}
