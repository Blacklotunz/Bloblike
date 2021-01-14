using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{

    public static bool BossRoomSpawned;
    public string[] possibleSpawningPoint;
    bool bossSpawned;
    public BossSpawner bossSpawner;
    public Animator[] animators;

    // Start is called before the first frame update
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        foreach(Animator animator in animators)
        {
            animator.SetBool("open", true);
        }
        bossSpawned = false;
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !bossSpawned)
        {
            foreach (Animator animator in animators)
            {
                animator.SetBool("open", false );
            }
            bossSpawned = true;
            bossSpawner.Spawn();
        }
    }

    

}
