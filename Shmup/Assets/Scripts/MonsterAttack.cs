using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private RoundManager roundManager;
    [SerializeField] SpriteRenderer player;
    [SerializeField] SpriteRenderer fireballPrefab;
    [SerializeField] Camera cam;
    SpriteRenderer fireball;
    private float fireballSpeed = 10f;
    private bool canFire = true;

    private List<SpriteRenderer> ActiveFireballs = new List<SpriteRenderer>();
    private List<SpriteRenderer> spawned = new List<SpriteRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        roundManager = FindObjectOfType<RoundManager>();
        spawned = roundManager.GetSpawnedMonsters();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            if (IsVisibleOnCamera(spawned[i].transform.position) && canFire)
            {
                StartCoroutine(FireProjectile(spawned[i].transform.position));
                canFire = false;
                StartCoroutine(ResetFireCooldown());
            }
        }
    }

    private IEnumerator FireProjectile(Vector3 monsterPosition)
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = (playerPosition - monsterPosition).normalized;

        fireball = Instantiate(fireballPrefab, monsterPosition, Quaternion.identity);
        ActiveFireballs.Add(fireball);

        while (IsVisibleOnCamera(fireball.transform.position))
        {
            fireball.transform.position += direction * Time.deltaTime * fireballSpeed;
            yield return null;
        }

        ActiveFireballs.Remove(fireball);
        Destroy(fireball);
    }

    private IEnumerator ResetFireCooldown()
    {
        yield return new WaitForSeconds(2f);
        canFire = true;
    }

    private bool IsVisibleOnCamera(Vector3 position)
    {
        Vector3 viewportPos = cam.WorldToViewportPoint(position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
    }
}