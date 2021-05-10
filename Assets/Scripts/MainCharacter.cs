using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player character
/// </summary>
public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        EventManager.OnGameReset += ResetPlayerPosition;
    }

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.position += Vector3.left * speed;
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.position += Vector3.right * speed;
        }
    }

    /// <summary>
    /// Reset the player back to the starting position, used for restarting
    /// </summary>
    void ResetPlayerPosition()
    {
        gameObject.transform.position = new Vector3(0, 4.63f, 0);
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bounds"))
        {
            EventManager.GameOver();
        }
        else if (other.CompareTag("Platform"))
        {
            EventManager.IncreaseScore();
        }
    }

    private void OnDestroy()
    {
        EventManager.OnGameReset -= ResetPlayerPosition;
    }
}
