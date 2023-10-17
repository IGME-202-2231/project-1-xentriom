using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Variable field
    [SerializeField] float speed = 5.0f;

    private SpriteRenderer spriteRenderer;
    private Camera cam;

    private float camHeight;
    private float camWidth;
    private bool isJumping;

    private Vector3 objectPosition = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        camHeight = 2.0f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;

        objectPosition = transform.position;
        isJumping = false;
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

    /// <summary>
    /// If the object is not already jumping, start the jump coroutine
    /// </summary>
    public void Jump()
    {
        if (!isJumping)
        {
            StartCoroutine(ToJump());
        }
    }

    /// <summary>
    /// Gradually move the object up and down through a parabola
    /// </summary>
    /// <returns></returns>
    IEnumerator ToJump()
    {
        // Set jumping to true
        isJumping = true;

        // Set the start position, initial velocity, gravity, and time
        Vector3 startPos = objectPosition;
        float initialVelocity = 10f;
        float gravity = 17f;
        float time = 0f;

        // Move the object up through a parabola until it reaches the start position
        while (objectPosition.y >= startPos.y)
        {
            time += Time.deltaTime;
            float y = startPos.y + initialVelocity * time - 0.5f * gravity * time * time;
            objectPosition = new Vector3(objectPosition.x, y, objectPosition.z);
            yield return null;
        }

        // Set jumping to false
        isJumping = false;
    }
}