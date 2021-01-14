using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject target;
    public float speed, range;
    public bool awake;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 force;
    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (!awake) return;
        
        Vector3 heading = target.transform.position - transform.position;
        // Gets a vector that points from the player's position to the target's.
        distance = Vector2.Distance(transform.position, target.transform.position);
        if ( distance > range)
        {
            animator.SetFloat("horizontalMovement", heading.normalized.x);
            animator.SetFloat("verticalMovement", heading.normalized.y);
            animator.SetBool("moving", true);
        }
        else //range reached - attack (moved in EnemyCombatLogic)
        {
            animator.SetBool("moving", false);
        }
    }

    void FixedUpdate()
    {
        if (distance > range && animator.GetBool("moving"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);
            force = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
            rb.MovePosition(force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            awake = true;
            target = collision.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            awake = false;
        }
    }
}
