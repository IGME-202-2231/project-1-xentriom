using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XInput;

public class SpriteInfo : MonoBehaviour
{
    public SpriteRenderer sRenderer;

    public Vector2 TopLeft 
    {
        get { return new Vector2(transform.position.x - sRenderer.size.x, transform.position.x + sRenderer.size.x); }
    }
    public Vector2 TopRight
    {
        get { return new Vector2(transform.position.x + sRenderer.size.x, transform.position.y + sRenderer.size.y); }
    }
    public Vector2 BottomLeft
    {
        get { return new Vector2(transform.position.x - sRenderer.size.x, transform.position.y - sRenderer.size.y); }
    }
    public Vector2 BottomRight
    {
        get { return new Vector2(transform.position.x + sRenderer.size.x, transform.position.y - sRenderer.size.y); }
    }
    public Vector2 RectMin
    {
        get { return new Vector2(BottomLeft.x, BottomLeft.y); }
    }
    public Vector2 RectMax
    {
        get { return new Vector2(TopRight.x, TopRight.y); }
    }

    void Update()
    {
        sRenderer = GetComponent<SpriteRenderer>();
    }
}