using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] SpriteRenderer player;
    private RoundManager round;
    private List<SpriteRenderer> spawnedMonsters = new List<SpriteRenderer>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsColliding(Vector2 spriteA, Vector2 spriteB)
    {

    }
}
