using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private RoundManager roundManager;
    [SerializeField] SpriteRenderer player;
    [SerializeField] SpriteRenderer fireball;

    // Start is called before the first frame update
    void Start()
    {
        roundManager = FindObjectOfType<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundManager.SpawnedMonsters.Count > 0)
        {
            foreach (var monster in roundManager.SpawnedMonsters)
            {
                // Add your monster attack logic here
                // Example: monster.AttackPlayer();
            }
        }
        Fire();
    }

    public void Fire()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            foreach (var monster in roundManager.SpawnedMonsters)
            {
                // Instantiate a fireball prefab at the monster's location
                Instantiate(fireball, monster.transform.position, Quaternion.identity);

                // Get the direction towards the player
                Vector3 direction = (player.transform.position - monster.transform.position).normalized;

                // Move the fireball towards the player's location
                float speed = 5f; // Adjust this value for desired speed
                float distance = Vector3.Distance(monster.transform.position, player.transform.position);
                float duration = distance / speed;
                float elapsedTime = 0f;

                while (elapsedTime < duration)
                {
                    elapsedTime += Time.deltaTime;
                    float t = elapsedTime / duration;
                    Vector3 newPosition = Vector3.Lerp(monster.transform.position, player.transform.position, t);
                    fireball.transform.position = newPosition;
                    yield return null;
                }

                // Destroy the fireball
                Destroy(fireball);
            }

            // Wait for 1 second before shooting again
            yield return new WaitForSeconds(1f);
        }
    }
}
