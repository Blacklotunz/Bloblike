using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int health;
    public bool dead;
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        dead = false;
    }

    public void TakeDamage(int dmg)
    {
        animator.SetTrigger("dmg");
        health -= dmg;
    }
}

