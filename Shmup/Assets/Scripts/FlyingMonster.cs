using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : MonoBehaviour
{
    [SerializeField] SpriteRenderer spritePrefab;
    private SpriteRenderer monster;
    private float health = 100;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) Destroy();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            isDead = true;
        }
    }

    public SpriteRenderer Spawn()
    {
        // Spawn monster
        return monster = Instantiate(spritePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }

    public void Destroy()
    {
        Destroy(monster.gameObject);
    }
}
