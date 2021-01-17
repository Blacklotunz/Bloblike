using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    public GameObject[] loot;
    public float lootRange, lootSpawnTimer, openingTimer;
    private float lootSpawnTime;
    public bool spawn;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        lootSpawnTimer = lootSpawnTime;
        animator = this.GetComponent<Animator>();
        Invoke("Spawn", openingTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn && lootSpawnTimer <= 0) SpawnLoot();
        lootSpawnTimer -= Time.deltaTime;
    }
    void Spawn()
    {
        animator.SetTrigger("open");
        spawn = true;
    }

    void SpawnLoot()
    {
        lootSpawnTimer = lootSpawnTime;
        GameObject lootItem = Instantiate(loot[Random.Range(0, loot.Length)], transform.position, Quaternion.identity);
        //lootItem.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * lootForce);
        lootItem.GetComponent<TargetJoint2D>().target = new Vector2(transform.position.x, transform.position.y) + (Random.insideUnitCircle * lootRange);
        spawn = false;
        Destroy(this);
    }
}
