using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] SpriteRenderer dagger;
    SpriteRenderer newDagger;
    private Camera cam;
    private float lastThrown;
    private float cooldown = 0.6f;
    private float speed = 16f;
    private float spin = 500f;

    private List<SpriteRenderer> activeDaggers = new List<SpriteRenderer>();

    public List<SpriteRenderer> ActiveDaggers
    {
        get { return activeDaggers; }
        set { activeDaggers = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDaggersOutsideCameraView();
    }

    public void Swing()
    {
        // Swing sword in front of the player
        Debug.Log("Swing Sword");
    }

    public void Throw(Vector2 mousePosition)
    {
        float currentTime = Time.time;
        if (currentTime > lastThrown + cooldown)
        {
            // Instantiate a new dagger at the player's position and add it to list
            newDagger = Instantiate(dagger, transform.position, Quaternion.identity);
            activeDaggers.Add(newDagger);

            // Get the direction vector from the player to the mouse
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            // Start a coroutine to move the dagger in the direction of the mouse
            StartCoroutine(MoveDagger(newDagger.transform, direction));

            lastThrown = currentTime;
        }
    }

    /// <summary>
    /// A coroutine that moves the dagger with a constant speed and rotation
    /// </summary>
    /// <param name="daggerTransform"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    IEnumerator MoveDagger(Transform daggerTransform, Vector2 direction)
    {
        // While the dagger is active in the scene
        while (daggerTransform.gameObject.activeSelf)
        {
            // Move the dagger by adding the direction vector multiplied by speed and time
            daggerTransform.position += (Vector3)direction * speed * Time.deltaTime;

            // Rotate the dagger by adding the spin value multiplied by time
            daggerTransform.Rotate(0, 0, spin * Time.deltaTime);

            yield return null;
        }
    }

    public void CheckDaggersOutsideCameraView()
    {
        for (int i = activeDaggers.Count - 1; i >= 0; i--)
        {
            SpriteRenderer daggerRenderer = activeDaggers[i];
            Vector3 daggerViewportPosition = cam.WorldToViewportPoint(daggerRenderer.transform.position);

            // Check if dagger is outside the camera view
            if (daggerViewportPosition.x < 0 || daggerViewportPosition.x > 1 || daggerViewportPosition.y < 0 || daggerViewportPosition.y > 1)
            {
                // Remove dagger from the active daggers list
                activeDaggers.RemoveAt(i);

                // Destroy the game object
                Destroy(daggerRenderer.gameObject);
            }
        }
    }
}