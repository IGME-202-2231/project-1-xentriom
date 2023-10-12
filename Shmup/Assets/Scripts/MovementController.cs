using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Variable field
    [SerializeField] float speed = 5.0f;
    SpriteRenderer spriteRenderer;
    Camera cam;
    float camHeight;
    float camWidth;

    Vector3 objectPosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        camHeight = 2.0f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        objectPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = speed * Time.deltaTime * direction;
        objectPosition += velocity;

        transform.position = objectPosition;

        // Clamp the x value to be between -camWidth/2 and 0, which are the edges of the left half of the screen
        objectPosition.x = Mathf.Clamp(objectPosition.x, -camWidth / 2f + .1f, 0f - (camWidth / 2 * .20f));

        // Assign the clamped object position to the object's transform
        transform.position = objectPosition;
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

            // Flip the sprite only if the current direction is different from the previous direction and not zero
            if ((direction.x < 0f) != spriteRenderer.flipX && direction.x != 0f)
            {
                spriteRenderer.flipX = direction.x < 0f;
            }
        }
    }

    public void Jump()
    {
        Debug.Log("Jump");
        
    }
}