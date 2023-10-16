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
        // Set the player's bounds
        playerBounds[0] = new Vector2(
            playerSprite.GetComponent<SpriteInfo>().RectMin.x, 
            playerSprite.GetComponent<SpriteInfo>().RectMax.x);
        playerBounds[1] = new Vector2(
            playerSprite.GetComponent<SpriteInfo>().RectMin.y,
            playerSprite.GetComponent<SpriteInfo>().RectMax.y);

        // Get list of spawned monsters and set their bounds
        spawnedMonsters = roundManager.SpawnedMonsters;
        for (int i = 0; i < spawnedMonsters.Count; i++)
        {
            SpriteRenderer monster = spawnedMonsters[i];
            Bounds bounds = monster.bounds;

            Vector2[] boundsArray = new Vector2[2];
            boundsArray[0] = new Vector2(bounds.min.x, bounds.max.x);
            boundsArray[1] = new Vector2(bounds.min.y, bounds.max.y);

            monsterBounds.Add(boundsArray);
        }

        // Get list of active fireballs and set their bounds
        activeFireballs = monsterAttack.ActiveFireballs;
        for (int i = 0; i < activeFireballs.Count; i++)
        {
            SpriteRenderer fireball = activeFireballs[i];
            Bounds bounds = fireball.bounds;

            Vector2[] boundsArray = new Vector2[2];
            boundsArray[0] = new Vector2(bounds.min.x, bounds.max.x);
            boundsArray[1] = new Vector2(bounds.min.y, bounds.max.y);

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

        // Check if firefalls collide with player
        for (int i = 0; i < activeFireballs.Count; i++)
        {
            // Sprite A = player, Sprite B = fireball
            if (IsColliding(playerBounds[0], playerBounds[1], fireballBounds[i][0], fireballBounds[i][0]))
            {
                // Remove and destroy fireball, and decrease player health
                Destroy(activeFireballs[i].gameObject);
                monsterAttack.ActiveFireballs.Remove(activeFireballs[i]);
                player.DamagePlayer();
            }
        }

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
        if (AspriteX.y < BspriteX.x || AspriteX.x > BspriteX.y)
        {
            return false; // No horizontal overlap
        }

        if (AspriteY.y < BspriteY.x || AspriteY.x > BspriteY.y)
        {
            return false; // No vertical overlap
        }

        return true;
    }
}
