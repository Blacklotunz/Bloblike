using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{

    public static bool BossRoomSpawned;
    public bool bossSpawned;
    public BossSpawner bossSpawner;
    public Animator[] doorsAnimators;

    // Start is called before the first frame update
    void Start()
    {
        doorsAnimators = GetComponentsInChildren<Animator>();
        foreach(Animator animator in doorsAnimators)
        {
            animator.SetBool("open", true);
        }
        bossSpawned = false;
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !bossSpawned)
        {
            foreach (Animator animator in doorsAnimators)
            {
                animator.SetBool("open", false );
            }
            bossSpawned = true;

            collision.GetComponent<CameraControl>().CameraShake(3f);
            Invoke("SpawnBoss",2f);
        }
    }

    private void SpawnBoss()
    {
        bossSpawner.Spawn();
    }
}
