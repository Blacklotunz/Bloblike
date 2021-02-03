using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatProjectileController : MonoBehaviour
{
    GameObject target;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //this.GetComponent<Rigidbody2D>()
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) return;

        this.GetComponent<Rigidbody2D>().isKinematic = true;
        this.GetComponent<Rigidbody2D>().simulated = false;
        this.GetComponent<Collider2D>().enabled = false;

        if (collision.gameObject.CompareTag("Player")) collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(1);

        animator.SetTrigger("explode");



        Destroy(gameObject, 2f);
    }
}
