using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator animator;
    public bool awake, dead;
    public float attackCooldown;
    public int health;

    public void Start()
    {
        animator = GetComponent<Animator>();
        awake = false;
    }

    void Update()
    {
        if (!awake || dead) return;

        if(attackCooldown <= 0)
        {
            this.Attack();
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {

    }

    void Attack()
    {

    }


    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
            return;
        }
    }


    public void Die()
    {

    }
   
}
