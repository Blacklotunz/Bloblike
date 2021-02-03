using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    private Quaternion rotation;
    public float rotationX, rotationY, rotationZ;
    private bool tileSpawned;
    public LevelTemplate levelTemplate;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        levelTemplate = GameObject.FindGameObjectWithTag("LevelTemplate").GetComponent<LevelTemplate>();
        tileSpawned = false;
        level = levelTemplate.level;
        rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
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

                if (Random.Range(0, 100) > 80 && !this.name.Contains("corner") && !this.name.Contains("Hole"))
                {
                    newObject = Instantiate(levelTemplate.LevelWallsAppliances[Random.Range(0, levelTemplate.LevelWallsAppliances.Length - 1)], this.transform.position, rotation);
                    newObject.transform.parent = gameObject.transform;
                }
            }
            tileSpawned = true;
        }
        
    }
}
