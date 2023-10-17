using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSprite;

    private Player player;
    private RoundManager roundManager;
    private MonsterAttack monsterAttack;
    private AttackController playerAttack;

    private List<SpriteRenderer> spawnedMonsters = new List<SpriteRenderer>();
    private List<SpriteRenderer> activeFireballs = new List<SpriteRenderer>();
    private List<SpriteRenderer> activeDaggers = new List<SpriteRenderer>();

    private Vector2[] playerBounds = new Vector2[2];
    private List<Vector2[]> monsterBounds = new List<Vector2[]>();
    private List<Vector2[]> fireballBounds = new List<Vector2[]>();
    private List<Vector2[]> daggerBounds = new List<Vector2[]>();

    private SpriteInfo playerSpriteInfo;
    private List<SpriteInfo> fireballSpriteInfo;

    // Start is called before the first frame update
    void Start()
    {
        // Find corresponding scripts
        player = FindObjectOfType<Player>();
        roundManager = FindObjectOfType<RoundManager>();
        monsterAttack = FindObjectOfType<MonsterAttack>();
        playerAttack = FindObjectOfType<AttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttacksToPlayerCollision();

        playerSpriteInfo = playerSprite.GetComponent<SpriteInfo>();

        // Set the player's bounds
        playerBounds[0] = playerSprite.GetComponent<SpriteInfo>().RectMin;
        playerBounds[1] = playerSprite.GetComponent<SpriteInfo>().RectMax;

        // Get list of spawned monsters and set their bounds
        spawnedMonsters = roundManager.SpawnedMonsters;
        for (int i = 0; i < spawnedMonsters.Count; i++)
        {
            Vector2[] boundsArray = new Vector2[2];
            boundsArray[0] = new Vector2(
                spawnedMonsters[i].GetComponent<SpriteInfo>().RectMin.x,
                spawnedMonsters[i].GetComponent<SpriteInfo>().RectMax.x);
            boundsArray[1] = new Vector2(
                spawnedMonsters[i].GetComponent<SpriteInfo>().RectMin.y,
                spawnedMonsters[i].GetComponent<SpriteInfo>().RectMax.y);

            monsterBounds.Add(boundsArray);
        }

        // Get list of active fireballs and set their bounds
        activeFireballs = monsterAttack.ActiveFireballs;
        for (int i = 0; i < activeFireballs.Count; i++)
        {
            Vector2[] boundsArray = new Vector2[2];
            boundsArray[0] = new Vector2(
                activeFireballs[i].GetComponent<SpriteInfo>().RectMin.x,
                activeFireballs[i].GetComponent<SpriteInfo>().RectMax.x);
            boundsArray[1] = new Vector2(
                activeFireballs[i].GetComponent<SpriteInfo>().RectMin.y,
                activeFireballs[i].GetComponent<SpriteInfo>().RectMax.y);

            fireballBounds.Add(boundsArray);
        }

        // Get list of active daggers and set their bounds
        activeDaggers = playerAttack.ActiveDaggers;
        for (int i = 0; i < activeDaggers.Count; i++)
        {
            SpriteRenderer dagger = activeDaggers[i];
            Bounds bounds = dagger.bounds;

            Vector2[] boundsArray = new Vector2[2];
            boundsArray[0] = new Vector2(bounds.min.x, bounds.max.x);
            boundsArray[1] = new Vector2(bounds.min.y, bounds.max.y);

            daggerBounds.Add(boundsArray);
        }

/*        // Check if firefalls collide with player
        for (int i = 0; i < activeFireballs.Count; i++)
        {
            Vector2 playerX = new Vector2(playerBounds[0].x, playerBounds[1].x);
            Vector2 playerY = new Vector2(playerBounds[0].y, playerBounds[1].y);

            Debug.Log(IsColliding(playerX, playerY, fireballBounds[i][0], fireballBounds[i][0]));

            // Sprite A = player, Sprite B = fireball
            if (IsColliding(playerX, playerY, fireballBounds[i][0], fireballBounds[i][0]))
            {
                // Remove and destroy fireball, and decrease player health
                Destroy(activeFireballs[i].gameObject);
                monsterAttack.ActiveFireballs.Remove(activeFireballs[i]);
                player.DamagePlayer();
            }
        }*/

        // Check if daggers collide with monsters
        for (int i = 0; i < activeDaggers.Count; i++)
        {
            // Sprite A = monster, Sprite B = dagger
            if (!IsColliding(monsterBounds[i][0], monsterBounds[i][1], daggerBounds[i][0], daggerBounds[i][1]))
            {
                // Remove and destroy monster and dagger
                Destroy(spawnedMonsters[i].gameObject);
                roundManager.SpawnedMonsters.Remove(spawnedMonsters[i]);
            }
        }
    }

    /// <summary>
    /// Calculates if the two sprites are colliding using AABB collision detection
    /// </summary>
    /// <param name="AspriteX">First sprite's min and max X bounds</param>
    /// <param name="AspriteY">First sprite's min and max Y bounds</param>
    /// <param name="BspriteX">Second sprite's min and max X bounds</param>
    /// <param name="BspriteY">Second sprite's min and max Y bounds</param>
    /// <returns>True if they collide</returns>
    public bool IsColliding(Vector2 AspriteX, Vector2 AspriteY, Vector2 BspriteX, Vector2 BspriteY)
    {
        if (BspriteX.x < AspriteX.y && 
            BspriteX.y > AspriteX.x && 
            AspriteY.x < AspriteY.y && 
            AspriteY.y > AspriteY.x)
        {
            return true;
        }
        return false;
    }

    public bool CheckCollision(SpriteInfo spriteA, SpriteInfo spriteB)
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
    /// Checks if the enemy attacks are colliding with the player
    /// </summary>
    public void EnemyAttacksToPlayerCollision()
    {
        for (int i = 0; i < activeFireballs.Count; i++)
        {
            if (CheckCollision(playerSpriteInfo, fireballSpriteInfo[i]))
            {
                player.DamagePlayer();
            }
        }
    }

    /// <summary>
    /// Checks if the player attacks are colliding with the enemy
    /// </summary>
    public void PlayerAttacksToEnemyCollision()
    {

    }

    public void OutOfBounds()
    {
        // Check if fireballs and daggers are out of bounds

        // Check if monsters are out of left bounds
    }
}
