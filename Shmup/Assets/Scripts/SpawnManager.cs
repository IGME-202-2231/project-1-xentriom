using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private Monster_Fly flying;
    private List<SpriteRenderer> spawnedMonsters = new List<SpriteRenderer>();

    public List<SpriteRenderer> Sprites
    {
        get { return sprites; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWave(int round)
    {
        for (int i = 0; i < round; i++)
        {
            
        }
    }

    public bool WaveCleared()
    {
        if (spawnedMonsters.Count == 0)
        {
            return true;
        }
        return false;
    }
}