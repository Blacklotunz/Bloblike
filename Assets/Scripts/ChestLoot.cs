using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    public GameObject[] loot;
    public float lootRange, spawnTime, spawnTimer;
    public bool spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn && spawnTimer <= 0) SpawnLoot();
        spawnTimer -= Time.deltaTime;
    }

    void SpawnLoot()
    {
        spawnTimer = spawnTime;
        GameObject lootItem = Instantiate(loot[Random.Range(0, loot.Length)], transform.position, Quaternion.identity);
        //lootItem.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * lootForce);
        lootItem.GetComponent<TargetJoint2D>().target = new Vector2(transform.position.x, transform.position.y) + (Random.insideUnitCircle * lootRange);
    }
}
