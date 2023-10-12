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
    private bool isJumping;
    private Coroutine jumpCoroutine;


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
    /// Set the object's position to the top of the jump arc and then move it down to the ground
    /// </summary>
    /// <returns></returns>
    IEnumerator ToJump()
    {
        isJumping = true;

        Vector3 startPos = objectPosition;
        float initialVelocity = 10f;
        float gravity = 14f;
        float time = 0f;

        while (objectPosition.y >= startPos.y)
        {
            time += Time.deltaTime;
            float y = startPos.y + initialVelocity * time - 0.5f * gravity * time * time;
            objectPosition = new Vector3(objectPosition.x, y, objectPosition.z);
            yield return null;
        }

        isJumping = false;
    }
}