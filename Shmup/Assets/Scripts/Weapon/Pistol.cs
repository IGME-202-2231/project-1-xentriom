using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] public Sprite gunSprite;
    [SerializeField] public Sprite AmmoSprite;

    public int AmmoCount { get; set; }

    public void Reload()
    {
        AmmoCount = 2;
    }

    public void Fire()
    {
        if (AmmoCount > 0)
        {
            // Spawn the ammo sprite
            GameObject ammo = new GameObject();
            SpriteRenderer renderer = ammo.AddComponent<SpriteRenderer>();
            renderer.sprite = AmmoSprite;

            // Decrement the ammo count
            AmmoCount--;
        }
    }
}
