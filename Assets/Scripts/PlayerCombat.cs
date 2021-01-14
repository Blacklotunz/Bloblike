using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject[] healthCounters;
    public Transform attackPos;
    public Animator animator;
    public Vector3 attackPosition;
    public LayerMask Damageble;
    public int dmg, health;
    public float xRange, yRange, timeBetweenAtk, playerAtkOffset;
    public bool dead;
    
    private CameraControl cameraControl;
    private float atkCooldown;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        atkCooldown = 0f;
        cameraControl = this.GetComponent<CameraControl>();
        dead = false;

        healthCounters = GameObject.FindGameObjectsWithTag("Health Count");
        healthCounters = healthCounters.OrderByDescending( e => e.name ).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        
        if (health <= 0 && !dead)
        {
            dead = true;
            animator.SetTrigger("dead");
            return;
        }


        if (atkCooldown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Attack(1); //atk right
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Attack(2); //atk down
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                Attack(3); //atk left
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Attack(4); //atk up
            }
        }
        else
        {
            atkCooldown -= Time.deltaTime;
            animator.SetInteger("attackDirection", 0);
        }
        
        
    }


    void Attack(int attackDirection)
    {
        //play animation
        atkCooldown = timeBetweenAtk;
        animator.SetInteger("attackDirection", attackDirection);
        
        switch (attackDirection)
        {
            case 1:
                attackPosition = new Vector3(transform.position.x + playerAtkOffset, transform.position.y + 0.3f, 1);
                break;
            case 2:
                attackPosition = new Vector3(transform.position.x, transform.position.y - playerAtkOffset + .3f, 1);
                break;
            case 3:
                attackPosition = new Vector3(transform.position.x - playerAtkOffset, transform.position.y + 0.3f, 1);
                break;
            case 4:
                attackPosition = new Vector3(transform.position.x, transform.position.y + playerAtkOffset, 1);
                break;
            default:
                attackPosition = new Vector3(transform.position.x, transform.position.y, 1);
                break;
        }
    }

    public void TakeDamage(int dmg)
    {
        if (!dead)
        {
            animator.SetTrigger("damage");
            cameraControl.CameraShake();
            health -= dmg;

            for (int i = 0; i < healthCounters.Length; i++)
            {
                Animator hca = healthCounters[i].GetComponent<Animator>();
                if (!hca.GetCurrentAnimatorStateInfo(0).IsName("life_count_empty"))
                {
                    hca.SetTrigger("loseHealth");
                    break;
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPosition, new Vector3(xRange, yRange, 1));
    }
}
