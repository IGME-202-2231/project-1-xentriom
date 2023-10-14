using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    abstract public void TakeDamage(int damage);
    abstract public void Spawn();
    abstract public void Destroy();
}