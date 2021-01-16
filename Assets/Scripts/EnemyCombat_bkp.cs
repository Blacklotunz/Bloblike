using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat_bkp : MonoBehaviour
{
    public int health, dmg;
    public float attackOffset, atkRangeX, atkRangeY, attackCooldown;
    public GameObject target;
    public LayerMask Damageble;
    public bool dead;
    public Collider2D[] enemiesToDmg;
    public Vector3 attackPosition;

    private float timeBetweenAtk;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        timeBetweenAtk = attackCooldown;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            dead = true;
            Destroy(this.gameObject);
        }
        if (target.GetComponent<PlayerCombat>().dead)
        {
            animator.SetInteger("attackDirection", 0);
            return;
        }

        if (attackCooldown > 0)
        {
            animator.SetInteger("attackDirection", -1);
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            // Gets a vector that points from the player's position to the target's.
            Vector3 heading = target.transform.position - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance; // This is now the normalized direction.

            if (Vector2.Distance(transform.position, target.transform.position) <= atkRangeX)
            {
                attackCooldown = timeBetweenAtk;
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                    {
                        animator.SetInteger("attackDirection", 1);
                        Attack(1);
                    }
                    if (direction.x < 0)
                    {
                        animator.SetInteger("attackDirection", 3);
                        Attack(3);
                    }
                }
                else
                {
                    if (direction.y > 0)
                    {
                        animator.SetInteger("attackDirection", 4);
                        Attack(4);
                    }
                    if (direction.y < 0)
                    {
                        animator.SetInteger("attackDirection", 2);
                        Attack(2);
                    }
                }
            }
            else
            {
                animator.SetInteger("attackDirection", 0);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    public void Attack(int attackDirection)
    {
        //reset atk cooldown
        attackCooldown = timeBetweenAtk;

        switch (attackDirection)
        {
            case 1:
                attackPosition = new Vector3(transform.position.x + attackOffset, transform.position.y, 1);
                break;
            case 2:
                attackPosition = new Vector3(transform.position.x, transform.position.y - attackOffset, 1);
                break;
            case 3:
                attackPosition = new Vector3(transform.position.x - attackOffset, transform.position.y, 1);
                break;
            case 4:
                attackPosition = new Vector3(transform.position.x, transform.position.y + attackOffset, 1);
                break;
            default:
                attackPosition = new Vector3(transform.position.x, transform.position.y, 1);
                break;
        }
        //enemiesToDmg = Physics2D.OverlapBoxAll(attackPosition, new Vector2(atkRangeX, atkRangeY), 0, Damageble);
        animator.SetInteger("attackDirection", attackDirection);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPosition, new Vector3(atkRangeX, atkRangeX, 1));
    }
}
