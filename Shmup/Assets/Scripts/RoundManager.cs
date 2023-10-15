using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    // Variable field
    [SerializeField] TextMesh textPrefab;
    private TextMesh text;
    [SerializeField] int round;
    
    private List<SpriteRenderer> spawnedMonsters = new List<SpriteRenderer>();
    [SerializeField] SpriteRenderer flyingMonsterPrefab;
    [SerializeField] SpriteRenderer groundedMonsterPrefab;

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
        // bat = GetComponent<Monster_Fly>();
        text = Instantiate(textPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        round = 0;
        StartNextRound();
    }

    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.yKey.wasPressedThisFrame)
        {
            TempKillAll();
            StartNextRound();
        }
        //StartNextRound();
    }

    /// <summary>
    /// Start the next round
    /// </summary>
    public void StartNextRound()
    {
        if (spawnedMonsters.Count == 0)
        {
            round++;
            text.text = $"Round: {round}";
            SpawnMonster(round);
            StartCoroutine(DisableTextAfterSeconds(2));
        }
    }

    IEnumerator DisableTextAfterSeconds(float seconds)
    {
        // Set the text active
        text.gameObject.SetActive(true);

        // Wait for the specified seconds
        yield return new WaitForSeconds(seconds);

        // Set the text inactive
        text.gameObject.SetActive(false);
    }

    public void SpawnMonster(int round)
    {
        // Spawn a grounded monster on round 1
        // Spawn a flying monster on round 2
        // Spawn a grounded and flying monster on round 3+
        if (round == 1)
        {
            spawnedMonsters.Add(SpawnGroundedMonster());
            spawnedMonsters[0].transform.position = new Vector2(8.41f, -4.04f);
        }
        else if (round == 2)
        {
            spawnedMonsters.Add(SpawnFlyingMonster());
            spawnedMonsters[0].transform.position = new Vector2(8.84f, -0.3f);
        }
        else
        {
            for (int i = 0; i < round - 2; i++)
            {
                spawnedMonsters.Add(SpawnGroundedMonster());
                spawnedMonsters.Add(SpawnFlyingMonster());

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