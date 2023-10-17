using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private Camera camera;
    private Player player;
    private RoundManager roundManager;
    private MonsterAttack monsterAttack;
    private AttackController playerAttack;

    private SpriteInfo playerSprite;
    private List<SpriteInfo> spawnedMonsterSprites = new List<SpriteInfo>();
    private List<SpriteInfo> activeFireballSprites = new List<SpriteInfo>();
    private List<SpriteInfo> activeDaggerSprites = new List<SpriteInfo>();

    private List<SpriteRenderer> spawnedMonsters = new List<SpriteRenderer>();
    private List<SpriteRenderer> activeFireballs = new List<SpriteRenderer>();
    private List<SpriteRenderer> activeDaggers = new List<SpriteRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

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
        spawnedMonsterSprites = roundManager.SpawnedMonsterSprites;
        activeDaggerSprites = playerAttack.ActiveDaggerSprites;
        activeFireballSprites = monsterAttack.ActiveFireballSprites;
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

    public void EnemyAttacksToPlayerCollision()
    {
        for (int i = 0; i < activeFireballSprites.Count; i++)
        {
            if (CheckCollisionBetween(playerSprite, activeFireballSprites[i]))
            {
                player.DamagePlayer();
                Destroy(activeFireballs[i].gameObject);
                activeFireballs.RemoveAt(i);
                activeFireballSprites.RemoveAt(i);
            }
        }
    }

    public void PlayerAttacksToEnemyCollision()
    {
        for (int i = 0; i < activeDaggerSprites.Count; i++)
        {
            if (CheckCollisionBetween(activeDaggerSprites[i], spawnedMonsterSprites[i]))
            {
                Destroy(activeDaggers[i].gameObject);
                activeDaggers.RemoveAt(i);
                activeDaggerSprites.RemoveAt(i);

                Destroy(spawnedMonsters[i].gameObject);
                spawnedMonsters.RemoveAt(i);
                spawnedMonsterSprites.RemoveAt(i);
            }
        }
    }

    public void OutOfBounds()
    {
        // Check if fireballs are out of bounds
        for (int i = activeFireballSprites.Count - 1; i >= 0; i--)
        {
            SpriteInfo fireballSprite = activeFireballSprites[i];
            Vector2 fireballMin = fireballSprite.RectMin;
            Vector2 fireballMax = fireballSprite.RectMax;

            if (fireballMax.x < camera.ViewportToWorldPoint(Vector3.zero).x)
            {
                Destroy(activeFireballs[i].gameObject);
                activeFireballs.RemoveAt(i);
                activeFireballSprites.RemoveAt(i);
            }
        }

        // Check if daggers are out of bounds
        for (int i = activeDaggerSprites.Count - 1; i >= 0; i--)
        {
            SpriteInfo daggerSprite = activeDaggerSprites[i];
            Vector2 daggerMin = daggerSprite.RectMin;
            Vector2 daggerMax = daggerSprite.RectMax;

            if (daggerMin.x > camera.ViewportToWorldPoint(Vector3.one).x)
            {
                Destroy(activeDaggers[i].gameObject);
                activeDaggers.RemoveAt(i);
                activeDaggerSprites.RemoveAt(i);
            }
        }

        // Check if monsters are out of left bounds
        for (int i = spawnedMonsterSprites.Count - 1; i >= 0; i--)
        {
            SpriteInfo monsterSprite = spawnedMonsterSprites[i];
            Vector2 monsterMin = monsterSprite.RectMin;
            Vector2 monsterMax = monsterSprite.RectMax;

            if (monsterMax.x < camera.ViewportToWorldPoint(Vector3.zero).x)
            {
                Destroy(spawnedMonsters[i].gameObject);
                spawnedMonsters.RemoveAt(i);
                spawnedMonsterSprites.RemoveAt(i);
            }
        }
    }
}
