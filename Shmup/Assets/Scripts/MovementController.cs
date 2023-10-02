using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Camera cam;
    float camHeight;
    float camWidth;

    Vector3 objectPosition = Vector3.zero;

    [SerializeField] float speed = 5.0f;

    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        camHeight = 2.0f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed * Time.deltaTime;
        objectPosition += velocity;

        transform.position = objectPosition;

        // Check if the object's x is out of bound
        if (transform.position.x > camWidth / 2 + .5 ||
            transform.position.x < -(camWidth / 2 + .5))
        {
            objectPosition.x *= -1f;
            transform.position = objectPosition;
        }

        // Check if the object's y is out of bound
        if (transform.position.y > camHeight / 2 + .5 ||
            transform.position.y < -(camHeight / 2 + .5))
        {
            objectPosition.y *= -1f;
            transform.position = objectPosition;
        }
    }

    /// <summary>
    /// Sets the direction of the object
    /// </summary>
    /// <param name="newDirection">New direction</param>
    public void SetDirection(Vector3 newDirection)
    {
        if (newDirection != null)
        {
            direction = newDirection.normalized;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
            }
        }
    }
}
