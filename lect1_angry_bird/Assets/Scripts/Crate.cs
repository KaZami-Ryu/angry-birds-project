using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            gameObject.SetActive(false);
            AudioManager.Instance.PlayAudio(AudioState.CRATEHIT);
        }
    }
}
