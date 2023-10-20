using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private Camera cam;
    private ScoreManager scoreManager;
    private Player player;
    private RoundManager roundManager;
    private MonsterAttack monsterAttack;
    private AttackController playerAttack;

    private SpriteInfo playerSprite;
    private List<SpriteRenderer> spawnedMonsters = new List<SpriteRenderer>();
    private List<SpriteRenderer> activeFireballs = new List<SpriteRenderer>();
    private List<SpriteRenderer> activeDaggers = new List<SpriteRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        scoreManager = FindObjectOfType<ScoreManager>();

        // Find corresponding scripts
        player = FindObjectOfType<Player>();
        roundManager = FindObjectOfType<RoundManager>();
        monsterAttack = FindObjectOfType<MonsterAttack>();
        playerAttack = FindObjectOfType<AttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateListOfCollidables();
        UpdateSpriteInfo();
        PlayerAttacksToEnemyCollision();
        EnemyAttacksToPlayerCollision();
        OutOfBounds();
    }

    public void UpdateListOfCollidables()
    {
        spawnedMonsters = roundManager.SpawnedMonsters;
        activeFireballs = monsterAttack.ActiveFireballs;
        activeDaggers = playerAttack.ActiveDaggers;
    }

    public void UpdateSpriteInfo()
    {
        playerSprite = player.GetComponent<SpriteInfo>();
    }

    public bool CheckCollisionBetween(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        if (spriteB.RectMin.x < spriteA.RectMax.x &&
            spriteB.RectMax.x > spriteA.RectMin.x &&
            spriteB.RectMin.y < spriteA.RectMax.y &&
            spriteB.RectMax.y > spriteA.RectMin.y)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if the enemies attach hit player
    /// </summary>
    public void EnemyAttacksToPlayerCollision()
    {
        for (int i = 0; i < activeFireballs.Count; i++)
        {
            if (CheckCollisionBetween(playerSprite, activeFireballs[i].GetComponent<SpriteInfo>()))
            {
                player.DamagePlayer();
                Destroy(activeFireballs[i].gameObject);
                activeFireballs.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Check if players attack hit enemy
    /// </summary>
    public void PlayerAttacksToEnemyCollision()
    {
        for (int i = 0; i < activeDaggers.Count; i++)
        {
            if (CheckCollisionBetween(activeDaggers[i].GetComponent<SpriteInfo>(), spawnedMonsters[i].GetComponent<SpriteInfo>()))
            {
                Destroy(activeDaggers[i].gameObject);
                activeDaggers.RemoveAt(i);

                Destroy(spawnedMonsters[i].gameObject);
                spawnedMonsters.RemoveAt(i);

                scoreManager.DefeatedMonster();
            }
        }
    }

    public void OutOfBounds()
    {
        // Check if fireballs are out of bounds
        for (int i = activeFireballs.Count - 1; i >= 0; i--)
        {
            SpriteInfo fireballSprite = activeFireballs[i].GetComponent<SpriteInfo>();
            Vector2 fireballMin = fireballSprite.RectMin;
            Vector2 fireballMax = fireballSprite.RectMax;

            if (fireballMax.x < cam.ViewportToWorldPoint(Vector3.zero).x)
            {
                Destroy(activeFireballs[i].gameObject);
                activeFireballs.RemoveAt(i);
            }
        }

        // Check if daggers are out of bounds
        for (int i = activeDaggers.Count - 1; i >= 0; i--)
        {
            SpriteInfo daggerSprite = activeDaggers[i].GetComponent<SpriteInfo>();
            Vector2 daggerMin = daggerSprite.RectMin;
            Vector2 daggerMax = daggerSprite.RectMax;

            if (daggerMin.x > cam.ViewportToWorldPoint(Vector3.one).x)
            {
                Destroy(activeDaggers[i].gameObject);
                activeDaggers.RemoveAt(i);
            }
        }

        // Check if monsters are out of left bounds
        for (int i = spawnedMonsters.Count - 1; i >= 0; i--)
        {
            SpriteInfo monsterSprite = spawnedMonsters[i].GetComponent<SpriteInfo>();
            Vector2 monsterMin = monsterSprite.RectMin;
            Vector2 monsterMax = monsterSprite.RectMax;

            if (monsterMax.x < cam.ViewportToWorldPoint(Vector3.zero).x)
            {
                Destroy(spawnedMonsters[i].gameObject);
                spawnedMonsters.RemoveAt(i);
            }
        }
    }
}
