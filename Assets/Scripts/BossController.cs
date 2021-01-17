using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator animator;
    public bool awake, dead, hidden, damageble;
    public float attackCooldown, projectileSpeed, showTime, hideTime;
    public int health;
    public GameObject projectiles, loot;
    private float attackTime, hideTimer, showTimer;

    public void Start()
    {
        animator = GetComponent<Animator>();
        awake = false;
        hidden = false;
        hideTimer = hideTime;
        showTimer = showTime;
    }

    void Update()
    {
        if (!awake || dead) return;

        if (Time.time >= attackTime + attackCooldown)
        {
            attackTime = Time.time;
            animator.SetTrigger("shoot");
            //this.Attack(); invoked at the end of the animation
        }

        if (hidden)
        {
            showTimer -= Time.deltaTime;
            if (showTimer <= 0)
            {
                damageble = false;
                animator.SetBool("hide", false);
                hidden = !hidden;
                showTimer = showTime;
            }
        }
        else
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0)
            {
                animator.SetBool("hide", true);
                hidden = !hidden;
                hideTimer = hideTime;
            }
        }
    }

    void Attack()
    {
        GameObject projectile = Instantiate(projectiles, this.transform.position, Quaternion.Euler(new Vector3(1f, 2f, 0f)));
        projectile.GetComponent<Rigidbody2D>().AddForce((GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position).normalized * projectileSpeed);
    }

    public void TakeDamage(int dmg)
    {
        if (dead || !damageble) return;
        health -= dmg;
        if (health <= 0)
        {
            Die();
            return;
        }
        animator.SetTrigger("dmg");
    }

    public void Die()
    {
        dead = true;
        animator.SetTrigger("death");
        Destroy(gameObject, 3f);
        Instantiate(loot, transform.position, Quaternion.identity); 
    }
}
