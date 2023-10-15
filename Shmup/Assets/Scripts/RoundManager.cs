using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    // Text variables
    [SerializeField] TextMesh textPrefab;
    private TextMesh text;

    [SerializeField] int round;
    
    // Monster variables
    private List<SpriteRenderer> spawnedMonsters = new List<SpriteRenderer>();
    [SerializeField] SpriteRenderer flyingMonsterPrefab;
    [SerializeField] SpriteRenderer groundedMonsterPrefab;

    public List<SpriteRenderer> SpawnedMonsters { get { return spawnedMonsters; } }

    public SpriteRenderer SpawnFlyingMonster()
    {
        return Instantiate(flyingMonsterPrefab);
    }
    public SpriteRenderer SpawnGroundedMonster()
    {
        return Instantiate(groundedMonsterPrefab);
    }

    // Start is called before the first frame update
    void Start()
    {
        text = Instantiate(textPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        round = 0;
        StartNextRound();
    }

    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.yKey.wasPressedThisFrame)
        {
            TempKillAll();
        }
        if (spawnedMonsters.Count > 0) MoveMonster();
        if (spawnedMonsters.Count == 0) StartNextRound();
    }

    /// <summary>
    /// Start the next round
    /// </summary>
    public void StartNextRound()
    {
        round++;
        text.text = $"Round: {round}";
        SpawnMonster(round);
        StartCoroutine(DisableTextAfterSeconds(2));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator DisableTextAfterSeconds(float seconds)
    {
        // Set the text active
        text.gameObject.SetActive(true);

        // Wait for the specified seconds
        yield return new WaitForSeconds(seconds);

        // Set the text inactive
        text.gameObject.SetActive(false);
    }

    /// <summary>
    /// Spawn monsters based on the round
    /// </summary>
    /// <param name="round">Round</param>
    public void SpawnMonster(int round)
    {
        if (round == 1)
        {
            // 1 grounded monster
            spawnedMonsters.Add(SpawnGroundedMonster());
            spawnedMonsters[0].transform.position = new Vector2(8.41f, -4.04f);
        }
        else if (round == 2)
        {
            // 1 flying monster
            spawnedMonsters.Add(SpawnFlyingMonster());
            spawnedMonsters[0].transform.position = new Vector2(8.84f, -0.3f);
        }
        else if (round > 2 && round <= 10)
        {
            for (int i = 0; i < round - 2; i++)
            {
                spawnedMonsters.Add(SpawnGroundedMonster());
                spawnedMonsters.Add(SpawnFlyingMonster());

                // Set the position of the monsters
                foreach (var monster in spawnedMonsters)
                {
                    if (monster.sprite == groundedMonsterPrefab.sprite)
                    {
                        monster.transform.position = new Vector2(
                            Random.Range(8.5f, 13f),
                            Random.Range(-4.5f, -3.4f));
                    }
                    if (monster.sprite == flyingMonsterPrefab.sprite)
                    {
                        monster.transform.position = new Vector2(
                            Random.Range(8.2f, 13f),
                            Random.Range(-1.5f, 1.2f));
                    }
                }
            }
        }
        else if (round > 10 && round <= 50)
        {
            for (int i = 0; i < round - 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    spawnedMonsters.Add(SpawnGroundedMonster());
                    spawnedMonsters.Add(SpawnFlyingMonster());
                }

                // Set the position of the monsters
                foreach (var monster in spawnedMonsters)
                {
                    if (monster.sprite == groundedMonsterPrefab.sprite)
                    {
                        monster.transform.position = new Vector2(
                            Random.Range(8.5f, 13f + round),
                            Random.Range(-4.5f, -3.4f));
                    }
                    if (monster.sprite == flyingMonsterPrefab.sprite)
                    {
                        monster.transform.position = new Vector2(
                            Random.Range(8.2f, 13f + round),
                            Random.Range(-1.5f, 1.2f));
                    }
                }
            }
        }
        else if (round > 50 && round <= 100)
        {
            for (int i = 0; i < round - 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    spawnedMonsters.Add(SpawnGroundedMonster());
                    spawnedMonsters.Add(SpawnFlyingMonster());
                }

                // Set the position of the monsters
                foreach (var monster in spawnedMonsters)
                {
                    if (monster.sprite == groundedMonsterPrefab.sprite)
                    {
                        monster.transform.position = new Vector2(
                            Random.Range(8.5f, 13f + round),
                            Random.Range(-4.5f, -3.4f));
                    }
                    if (monster.sprite == flyingMonsterPrefab.sprite)
                    {
                        monster.transform.position = new Vector2(
                            Random.Range(8.2f, 13f + round),
                            Random.Range(-1.5f, 1.2f));
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < round - 2; i++)
            {
                for (int j = 0; j < (int)round/5; j++)
                {
                    spawnedMonsters.Add(SpawnGroundedMonster());
                    spawnedMonsters.Add(SpawnFlyingMonster());
                }

                // Set the position of the monsters
                foreach (var monster in spawnedMonsters)
                {
                    if (monster.sprite == groundedMonsterPrefab.sprite)
                    {
                        monster.transform.position = new Vector2(
                            Random.Range(8.5f, 13f + round),
                            Random.Range(-4.5f, -3.4f));
                    }
                    if (monster.sprite == flyingMonsterPrefab.sprite)
                    {
                        monster.transform.position = new Vector2(
                            Random.Range(8.2f, 13f + round),
                            Random.Range(-1.5f, 1.2f));
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void MoveMonster()
    {
        foreach (var monster in spawnedMonsters)
        {
            // Calculate the new position of the monster
            float newX = monster.transform.position.x - Time.deltaTime;
            float newY = monster.transform.position.y;

            // Add a small chance to move up or down
            float randomChance = Random.Range(0f, 1f);
            float changeChance = 0.1f;

            if (Mathf.Approximately(newY, monster.transform.position.y))
            {
                changeChance = 0.1f;
            }
            else
            {
                changeChance = 0.05f;
            }

            if (randomChance < changeChance)
            {
                newY += Random.Range(-0.2f, 0.2f);
            }

            // Restrict the y position based on the monster type
            if (monster.sprite == flyingMonsterPrefab.sprite)
            {
                newY = Mathf.Clamp(newY, -2.2f, 1.7f);
            }
            else if (monster.sprite == groundedMonsterPrefab.sprite)
            {
                newY = Mathf.Clamp(newY, -4.4f, -3.35f);
            }

            // Update the position of the monster
            monster.transform.position = new Vector2(newX, newY);
        }
    }

    public void TempKillAll()
    {
        foreach (var monster in spawnedMonsters)
        {
            Destroy(monster.gameObject);
        }

        spawnedMonsters.Clear();
    }
}