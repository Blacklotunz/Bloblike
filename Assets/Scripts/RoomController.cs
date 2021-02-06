using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public int numOfEnemies;
    public LevelTemplate levelTemplate;
    private List<GameObject> roomTiles, roomDoors;
    public bool enemySpawned, roomCleared;
    
    void Start(){
        levelTemplate = FindObjectOfType<LevelTemplate>(); 
        roomTiles = new List<GameObject>();
        roomDoors = new List<GameObject>();
        Transform[] children = this.GetComponentsInChildren<Transform>(false);
        foreach(Transform tr in children)
        {
            if (tr.CompareTag("Floor"))
            {
                roomTiles.Add(tr.gameObject);
            }
            if (tr.CompareTag("Door"))
            {
                roomDoors.Add(tr.gameObject);
            }
        }
        roomCleared = false;
        OpenDoors(true);
    }

    void Update()
    {
        if(numOfEnemies == 0)
        {
            roomCleared = true;
            OpenDoors(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.roomCleared && collision.CompareTag("Player") && !enemySpawned)
        {
            Spawn();
        }

        //checks whether the adjacent room has compatible door otherwhise remove it also remove the spawn point to avoid room stacking on eachother
        if (collision.CompareTag("RoomSpawn") && !collision.gameObject.name.Contains("Fake"))
        {
            if (!IsNextRoomCompatible(collision.transform.parent.name))
            {
                collision.GetComponent<RoomSpawner>().spawned = true;
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

    public void EnemyKilled()
    {
        this.numOfEnemies--;
    }

    public void OpenDoors(bool open)
    {
       foreach (GameObject door in roomDoors)
       {
            if(door)
            {
                door.GetComponent<Animator>().SetBool("open", open);
                door.GetComponent<Collider2D>().enabled = !open;
            }
       }
    }


    public void Spawn()
    {
        if (!levelTemplate)
        {
            return;
        }
        for (int i = numOfEnemies; i > 0; i--)
        {   
            GameObject enemyToSpawn = levelTemplate.Enemies[Random.Range(0, levelTemplate.Enemies.Length - 1)];
            enemyToSpawn.GetComponent<EnemyController>().roomReference = this;
            Instantiate(enemyToSpawn, roomTiles[Random.Range(0, roomTiles.Count - 1)].transform.position, Quaternion.identity);
        }
        enemySpawned = true;
        OpenDoors(false);
    }


}
