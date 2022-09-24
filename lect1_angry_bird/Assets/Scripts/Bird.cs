using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    #region Private Variables

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _sprite;
    private Renderer _renderer;
    private Vector3 initialPosition;
    private bool isDraggedOneTime;

    #endregion

    #region Properties

    private bool dragging;

    public bool IsDragging
    {
        get { return dragging; }
        set { dragging = value; }
    }

    private bool reseting;

    public bool IsReseting
    {
        get { return reseting; }
        set { reseting = value; }
    }

    #endregion


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _renderer = GetComponent<Renderer>();
        initialPosition = transform.position;
        isDraggedOneTime = false;
        IsReseting = false;
    }

    private void OnMouseDown()
    {
        if (isDraggedOneTime) return;
        if (LevelManager.Instance.State == LevelState.END) return;
        _sprite.color = Color.red;
        AudioManager.Instance.PlayAudio(AudioState.STRECH);
    }

    private void OnMouseDrag()
    {
        if (isDraggedOneTime) return;
        if (LevelManager.Instance.State == LevelState.END) return;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(pos.x, pos.y,0);
    }

    private void OnMouseUp()
    {
        if (isDraggedOneTime) return;
        if (LevelManager.Instance.State == LevelState.END) return;
        AudioManager.Instance.PlayAudio(AudioState.THROW);
        _sprite.color = Color.white;
        _rigidBody.gravityScale = 1;
        Vector3 distance = initialPosition - transform.position;
        _rigidBody.AddForce(distance * 500);
        isDraggedOneTime = true;
        LevelManager.Instance.NumberOfRetries--;
    }


    #region Public Helper Functions

    public bool IsIdleAfterThrow()
    {
        if (isDraggedOneTime && _rigidBody.velocity.magnitude < 0.02)
        {
            return true;
        }
        return false;
    }

    public bool IsBirdOutOfScene()
    {
        return !_renderer.isVisible;
    }

    public IEnumerator ResetBird(float waitTime = 0)
    {
        IsReseting = true;
        yield return new WaitForSeconds(waitTime);
        transform.position = initialPosition;
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.angularVelocity = 0;
        _rigidBody.gravityScale = 0;
        _rigidBody.transform.rotation = Quaternion.identity;
        isDraggedOneTime = false;
        IsReseting = false;
    }

    #endregion
}
