using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    public Quaternion rotation;
    private bool tileSpawned, corner;
    public LevelTemplate levelTemplate;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        levelTemplate = GameObject.FindGameObjectWithTag("LevelTemplate").GetComponent<LevelTemplate>();
        tileSpawned = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!tileSpawned)
        {
            GameObject newObject;
            if (tag.Equals("Obstacle"))
            {
                newObject = Instantiate(levelTemplate.LevelObstacles[level], this.transform.position, Quaternion.identity);
                newObject.transform.parent = gameObject.transform;

            }
            if (tag.Equals("Floor") || tag.Equals("Obstacle"))
            {
                newObject = Instantiate(levelTemplate.LevelFloor[level], this.transform.position, Quaternion.identity);
                newObject.transform.parent = gameObject.transform;
            }
            if (tag.Equals("Wall"))
            {
                newObject = Instantiate(levelTemplate.LevelWalls[level], this.transform.position, rotation);
                newObject.transform.parent = gameObject.transform;
            }
            tileSpawned = true;
        }
        
    }
}
