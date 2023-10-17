using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] SpriteRenderer dagger;
    SpriteRenderer newDagger;
    private float lastThrown;
    private float cooldown = 0.6f;
    private float speed = 16f;
    private float spin = 500f;

    private List<SpriteRenderer> activeDaggers = new List<SpriteRenderer>();
    private List<SpriteInfo> spriteInfos = new List<SpriteInfo>();

    public List<SpriteRenderer> ActiveDaggers
    {
        get { return activeDaggers; }
        set { activeDaggers = value; }
    }

    public List<SpriteInfo> ActiveDaggerSprites
    {
        get
        {
            for (int i = 0; i < activeDaggers.Count; i++)
            {
                spriteInfos.Add(activeDaggers[i].GetComponent<SpriteInfo>());
            }
            return spriteInfos;
        }
        set { spriteInfos = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
            StartCoroutine(MoveDagger(newDagger, direction));

            lastThrown = currentTime;
        }
    }

    /// <summary>
    /// A coroutine that moves the dagger with a constant speed and rotation
    /// </summary>
    /// <param name="currentDagger"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    IEnumerator MoveDagger(SpriteRenderer currentDagger, Vector2 direction)
    {
        // While the dagger is active in the scene
        while (currentDagger.gameObject.activeSelf && currentDagger != null)
        {
            // Move the dagger by adding the direction vector multiplied by speed and time
            currentDagger.transform.position += (Vector3)direction * speed * Time.deltaTime;

            // Rotate the dagger by adding the spin value multiplied by time
            currentDagger.transform.Rotate(0, 0, spin * Time.deltaTime);

            yield return null;
        }
    }
}