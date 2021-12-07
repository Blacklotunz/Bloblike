using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void Hit(Vector2 direction)
    {
        Debug.Log("not implemented!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) collision.gameObject.SendMessage("TakeDamage", dmg);
    }
}
