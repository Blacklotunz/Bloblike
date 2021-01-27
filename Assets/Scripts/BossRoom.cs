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
        if (collision.CompareTag("RoomSpawn"))
        {
            if (!IsNextRoomCompatible(collision.gameObject.name))
            {
                collision.GetComponent<RoomSpawner>().RemoveDoor();
            }
            Destroy(collision.gameObject);
        }
    }

    bool IsNextRoomCompatible(string adiacentDoorName)
    {
        Dictionary<string, string> compatibility = new Dictionary<string, string>();
        compatibility.Add("R", "L");
        compatibility.Add("L", "R");
        compatibility.Add("T", "B");
        compatibility.Add("B", "T");

        string side = adiacentDoorName.Split(' ')[1];
        string compatibleSide;
        compatibility.TryGetValue(side, out compatibleSide);
        return gameObject.name.Contains(compatibleSide);
    }

    private void SpawnBoss()
    {
        bossSpawner.Spawn();
    }
}
