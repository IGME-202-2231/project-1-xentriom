using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] SpriteRenderer player;
    private int health;

    public int Health { get { return health; } }

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.lKey.wasPressedThisFrame)
        {
            DamagePlayer();
        }
    }

    public void DamagePlayer()
    {
        health--;
    }
}
