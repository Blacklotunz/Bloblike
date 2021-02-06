using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public RoomController roomReference;
    public LayerMask Damageble;
    public Collider2D[] enemiesToDmg;
    public Vector3 attackPosition; //change this with animation blend tree
    public int health, dmg;
    public float speed, attackOffset, atkRangeX, atkRangeY, attackCooldown, disaggro, aggroRange;
    public bool awake, dead;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 force, pushbackPosition;
    private Vector3 heading;
    private float distance, timeBetweenAtk, pushBackTimer;
    private int attackDirection;


    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.SetBool("moving", true);
        rb = GetComponent<Rigidbody2D>();
        timeBetweenAtk = attackCooldown;
        target = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
       
        if (health <= 0) return;

        heading = target.transform.position - transform.position;
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < aggroRange) awake = true;
        
        if (!awake) return;
        
        if (distance > disaggro) awake = false;

        if (animator.GetBool("moving"))
        {
            animator.SetFloat("horizontalMovement", heading.normalized.x);
            animator.SetFloat("verticalMovement", heading.normalized.y);
        }
        
        if (attackCooldown > 0) //wait attack cooldown
        {
            animator.SetInteger("attackDirection", -1);
            attackCooldown -= Time.deltaTime;
        }
        else //attack
        {
            // Gets a vector that points from the player's position to the target's.   
            Vector3 direction = heading / distance; // This is now the normalized direction.
            if (Vector2.Distance(transform.position, target.transform.position) <= atkRangeX)
            {
                attackCooldown = timeBetweenAtk;
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                    {
                        Attack(1);
                    }
                    if (direction.x < 0)
                    {
                        Attack(3);
                    }
                }
                else
                {
                    if (direction.y > 0)
                    {
                        Attack(4);
                    }
                    if (direction.y < 0)
                    {
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

    void FixedUpdate()
    {
        if (!awake || health <= 0) return;

        if (pushBackTimer > 0)
        {
            Vector3 currentPosition = transform.position;
            Vector3 newPosition = Vector3.Lerp(currentPosition, pushbackPosition, 0.2f);
            transform.position = newPosition;
            pushBackTimer -= Time.deltaTime;
        }


        distance = Vector2.Distance(transform.position, target.transform.position);
        if (animator.GetBool("moving") &&  distance >= Mathf.Min(atkRangeX,atkRangeY))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            force = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
            rb.MovePosition(force);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
            return;
        }
        pushBack();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && !dead)
        {
            this.TakeDamage(collision.GetComponent<Weapon>().dmg);
        }
    }

    void Die()
    {
        dead = true;
        //Freeze all positions
        animator.SetTrigger("dead");
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        this.GetComponent<SpriteRenderer>().sortingOrder = 1;
        this.GetComponent<EnemyController>().enabled = false;
        roomReference.EnemyKilled(); //toDO event
    }

   void pushBack()
    {
        pushbackPosition = transform.position - heading;
        pushBackTimer = timeBetweenAtk;
    }

    public void Attack(int attackDirection)
    {
        //reset atk cooldown
        attackCooldown = timeBetweenAtk;

        //enemiesToDmg = Physics2D.OverlapBoxAll(attackPosition, new Vector2(atkRangeX, atkRangeY), 0, Damageble);
        animator.SetInteger("attackDirection", attackDirection);

        this.attackDirection = attackDirection;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPosition, new Vector3(atkRangeX, atkRangeX, 1));
    }

    public Vector3 getAttackPosition()
    {
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

        return attackPosition;
    }

}
