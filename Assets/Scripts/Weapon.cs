using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int dmg;
    public float cooldown, speed;

    public abstract void Hit(Vector2 direction);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) collision.gameObject.SendMessage("TakeDamage", dmg);
    }
}
