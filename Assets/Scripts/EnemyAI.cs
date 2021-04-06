using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public RoomController roomReference;
    public LayerMask Damageble;
    public Collider2D[] enemiesToDmg;
    public Vector3 attackPosition; //change this with animation blend tree
    public int health, dmg;
    public float speed, nextWayPointDistance, attackOffset, atkRangeX, atkRangeY, timeBetweenAtk, disaggro, aggroRange;
    public bool awake, dead;

    private Animator animator;
    private Vector2 pushbackPosition, direction;
    private Vector3 heading;
    private float distance, attackCooldown, pushBackTimer;
    private int attackDirection;

    Seeker seeker;
    Path path;
    Rigidbody2D rb;
    int currentWaypoint=0;
    bool reachedEndOfPath;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        attackCooldown = timeBetweenAtk;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void Update()
    {
        if (health <= 0) return;
      
        if (!target) return;

        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance < aggroRange)
        {
            awake = true;
        }

        if (!awake) return;

        if (distance > disaggro)
        {
            awake = false;
        }

        if (path == null)
        {
            return;
        }      
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && !dead)
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
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

            return;
        }

        heading = target.transform.position - transform.position;
        animator.SetFloat("horizontalMovement", heading.normalized.x);
        animator.SetFloat("verticalMovement", heading.normalized.y);


        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            if (attackCooldown > 0) //wait attack cooldown
            {
                animator.SetInteger("attackDirection", -1);
                attackCooldown -= Time.deltaTime;
            }
            else //attack
            {
                // Gets a vector that points from the player's position to the target's.   
                if (Vector2.Distance(transform.position, target.transform.position) <= atkRangeX)
                {
                    attackCooldown = timeBetweenAtk;
                    if (Mathf.Abs(heading.x) > Mathf.Abs(heading.y))
                    {
                        if (heading.normalized.x > 0)
                        {
                            Attack(1);
                        }
                        if (heading.normalized.x < 0)
                        {
                            Attack(3);
                        }
                    }
                    else
                    {
                        if (heading.normalized.y > 0)
                        {
                            Attack(4);
                        }
                        if (heading.normalized.y < 0)
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

            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
       
        Vector2 velocity = direction * speed;
        Vector2 force = velocity * Time.deltaTime;

        rb.AddForce(force);
        //transform.position += new Vector3(force.x, force.y, 0f);

        
        
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance <= nextWayPointDistance)
        {
            currentWaypoint++;
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
        attackDirection = 0;
        animator.SetTrigger("dead");
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        this.GetComponent<Collider2D>().enabled = false;
        //this.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        //this.GetComponent<SpriteRenderer>().sortingOrder = 1;
        this.GetComponent<EnemyAI>().enabled = false;
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
