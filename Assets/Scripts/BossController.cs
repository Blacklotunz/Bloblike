using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator animator;
    public bool awake, dead;
    public float attackCooldown, projectileSpeed;
    public int health;
    public GameObject projectiles;
    private float attackTime;

    public void Start()
    {
        animator = GetComponent<Animator>();
        awake = false;
        attackTime = Time.time;
    }

    void Update()
    {
        if (!awake || dead) return;

        if(Time.time >= attackTime + attackCooldown)
        {
            attackTime = Time.time;
            animator.SetTrigger("shoot");
            //this.Attack(); invoked at the end of the animation
        }

    }

    void Attack()
    {  
        GameObject projectile = Instantiate(projectiles, this.transform.position, Quaternion.Euler(new Vector3(1f,2f,0f)));
        projectile.GetComponent<Rigidbody2D>().AddForce((GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position).normalized * projectileSpeed);
    }


    public void TakeDamage(int dmg)
    {
        animator.SetTrigger("dmg");
        health -= dmg;
        if (health <= 0)
        {
            Die();
            return;
        }
    }


    public void Die()
    {
        dead = true;
        animator.SetTrigger("death");
        Destroy(gameObject, 3f);
    }
   
}
