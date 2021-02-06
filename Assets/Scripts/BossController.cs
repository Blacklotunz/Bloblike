using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator animator;
    public bool awake, dead, hidden, damageble;
    public float attackCooldown, projectileSpeed, showTime, hideTime, tiredCoolDown;
    public int health, attacksToSickness;
    public GameObject projectiles, loot;
    private float attackTime, hideTimer, showTimer;
    private int attacksLeft;

    public void Start()
    {
        animator = GetComponent<Animator>();
        awake = false;
        hidden = false;
        hideTimer = hideTime;
        showTimer = showTime;
        attacksLeft = attacksToSickness;
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
            if(attacksLeft <= 0)
            {
                animator.SetBool("tired", true);
                Invoke("SetActive", tiredCoolDown);
            }
        }
    }

    void SetActive()
    {
        animator.SetBool("tired", false);
        attacksLeft = attacksToSickness;
    }

    void Attack()
    {
        GameObject projectile = Instantiate(projectiles, new Vector3(this.transform.position.x, this.transform.position.y - 1f, this.transform.position.z), Quaternion.Euler(new Vector3(1f, 2f, 0f)));
        projectile.GetComponent<Rigidbody2D>().AddForce((GameObject.FindGameObjectWithTag("Player").transform.position - projectile.transform.position).normalized * projectileSpeed);
        attacksLeft--;
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
        Instantiate(loot, new Vector3(transform.position.x, transform.position.y-2f, transform.position.z), Quaternion.identity); 
    }
}
