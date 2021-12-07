using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayerCombat : MonoBehaviour
{
    public GameObject[] healthCounters;
    public Weapon weapon;
    public Transform attackPos;
    public Animator animator;
    //public Vector3 attackPosition;
    public LayerMask Damageble;
    public int dmg, health;
    public float xRange, yRange, timeBetweenAtk, playerAtkOffset, atkSpeed;
    public bool dead;

    public float debugAnimatorSpeed;
    
    private CameraControl cameraControl;
    private float atkCooldown;
    
    void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        animator = GetComponent<Animator>();
        atkCooldown = 0;
        timeBetweenAtk = weapon.cooldown;
        atkSpeed = weapon.speed;
        cameraControl = this.GetComponent<CameraControl>();
        dead = false;

        healthCounters = GameObject.FindGameObjectsWithTag("Health Count");
        healthCounters = healthCounters.OrderByDescending( e => e.name ).ToArray();

        GameEvents.current.onPlayerHealthChange += PlayerHealthChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        
        if (health <= 0 && !dead)
        {
            Die();
            return;
        }


        if (atkCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Attack(1); //atk right
                weapon.Hit(transform.right);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Attack(2); //atk down
                weapon.Hit(transform.up * -1f);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Attack(3); //atk left
                weapon.Hit(transform.right * -1f);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Attack(4); //atk up
                weapon.Hit(transform.up);
            }
        }
        else
        {
            if(atkCooldown > 0)
            {
                atkCooldown -= Time.deltaTime;
            }
            animator.SetInteger("attackDirection", 0);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Coin"))
        {
            collision.gameObject.SendMessage("Collect");
            GameEvents.current.CoinCollected();
        }
    }

    void Attack(int attackDirection)
    {
        //play animation
        atkCooldown = timeBetweenAtk;
        animator.speed = atkSpeed;
        debugAnimatorSpeed = animator.speed;
        animator.SetInteger("attackDirection", attackDirection);
    }

    public void TakeDamage(int dmg)
    {
        if (!dead)
        {
            animator.SetTrigger("damage");
            cameraControl.CameraShake(0f);
            health -= dmg;
            UpdateHealthCounters(-dmg);
        }
    }

    public void PlayerHealthChange(int delta)
    {
        if (delta < 0)
        {
            TakeDamage(delta);
        }
        else
        {
            health += delta;
            UpdateHealthCounters(delta);
        }

    }

    void UpdateHealthCounters(int healthDelta)
    {
        int absHealthDelta = Mathf.Abs(healthDelta);
        for (int j = absHealthDelta; j > 0; j--)
        {
            if (healthDelta < 0)
            {
                for (int i = 0; i < healthCounters.Length; i++)
                {
                    HealthCount hc = healthCounters[i].GetComponent<HealthCount>();
                    if (hc.getHealthLevel() > 0)
                    {
                        hc.decrease();
                        break;
                    }
                }
            }
            else
            {
                for (int i = healthCounters.Length-1; i >= 0; i--)
                {
                    HealthCount hc = healthCounters[i].GetComponent<HealthCount>();
                    if (hc.getHealthLevel() < hc.maxHealthLevel )
                    {
                        hc.increase();
                        break;
                    }
                }
            }
        }
    }

    void Die()
    {
        dead = true;
        animator.SetInteger("attackDirection", 0);
        animator.SetTrigger("dead");
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().sortingLayerName = "Background";

        //trigger the event
        GameEvents.current.PlayerDie();
    }

    /*    private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPosition, new Vector3(xRange, yRange, 1));
        }*/
}
