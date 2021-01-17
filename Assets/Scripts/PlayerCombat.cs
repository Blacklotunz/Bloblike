using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayerCombat : MonoBehaviour
{
    public GameObject[] healthCounters;
    public TMPro.TextMeshProUGUI coinCounter;
    public Transform attackPos;
    public Animator animator;
    //public Vector3 attackPosition;
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
        coinCounter = GameObject.FindGameObjectWithTag("CoinsCounter").GetComponent<TMPro.TextMeshProUGUI>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Coin"))
        {
            int count = (int.Parse(coinCounter.text))+1;
            coinCounter.text = count.ToString();
            collision.gameObject.SendMessage("Collect");
        }
    }

    void Attack(int attackDirection)
    {
        //play animation
        atkCooldown = timeBetweenAtk;
        animator.SetInteger("attackDirection", attackDirection);
        
       /* switch (attackDirection)
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
        }*/
    }

    public void TakeDamage(int dmg)
    {
        if (!dead)
        {
            animator.SetTrigger("damage");
            cameraControl.CameraShake(0f);
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

    void Die()
    {
        dead = true;
        animator.SetTrigger("dead");
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
    }

/*    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPosition, new Vector3(xRange, yRange, 1));
    }*/
}
