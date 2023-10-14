using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Fly : Monster
{
    [SerializeField] SpriteRenderer spritePrefab;
    private SpriteRenderer monster;
    private int health = 100;
    private bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        if (isDead) Destroy();
    }

    /// <summary>
    /// Remove health by damage amount
    /// </summary>
    /// <param name="damage">Health to lose</param>
    public override void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
        }
    }

    /// <summary>
    /// Spawns the monster
    /// </summary>
    public override void Spawn()
    {
        
    }

    /// <summary>
    /// Destroys the monster
    /// </summary>
    public override void Destroy()
    {
        Destroy(monster.gameObject);
    }
}
