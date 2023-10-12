using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    // Variable field
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] Vector2 rectSize = Vector2.one;
    [SerializeField] float radius = 1f;
    private bool isColliding = false;

    /// <summary>
    /// Get the radius of the sprite
    /// </summary>
    public float Radius { get { return radius; } }

    /// <summary>
    /// Set the collision state of the sprite
    /// </summary>
    public bool IsColliding { get { return isColliding; } set { isColliding = value; } }

    /// <summary>
    /// Bottom left
    /// </summary>
    public Vector2 RectMin
    {
        get
        {
            return new Vector2(
                transform.position.x - (rectSize.x / 2f),
                transform.position.y - (rectSize.y / 2f));
        }
    }

    /// <summary>
    /// Top right
    /// </summary>
    public Vector2 RectMax
    {
        get
        {
            return new Vector2(
                transform.position.x + (rectSize.x / 2f),
                transform.position.y + (rectSize.y / 2f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If colliding turn sprite red
        if (isColliding)
        {
            renderer.color = Color.red;
        }
        else
        {
            renderer.color = Color.white;
        }
    }

    /// <summary>
    /// Draw the collision bound of radius and rect
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw the AABB collision bounds of sprite
            Gizmos.DrawWireCube(transform.position, rectSize);

        // Draw the circle collision bounds of sprite
            Gizmos.DrawWireSphere(transform.position, radius);
    }
}