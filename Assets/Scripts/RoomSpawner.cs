using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingPoint;
    //1 -> need room right
    //2 -> need room bottom
    //3 -> need room left
    //4 -> need room top

    public RoomTemplates templates;
    private int rand;
    private bool spawned;
    public float spawnOffsetX, spawnOffsetY;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("RoomsTemplate").GetComponent<RoomTemplates>();
        spawned = false;
        Invoke("Spawn", .1f);
    }

    void Spawn()
    {
        if (!spawned && templates.roomLeft > 0)
        {
            templates.roomLeft--;
            spawned = true;
            GameObject nextRoom;
            switch (this.openingPoint)
            {
                case 1:
                    rand = Random.Range(0, templates.RRooms.Length);
                    nextRoom = Instantiate(templates.RRooms[rand], transform.position + new Vector3(spawnOffsetX, spawnOffsetY, 0f), Quaternion.identity);
                    LevelMap.AddRoom(nextRoom.name, nextRoom);
                    break;
                case 2:
                    rand = Random.Range(0, templates.BRooms.Length);
                    nextRoom = Instantiate(templates.BRooms[rand], transform.position + new Vector3(spawnOffsetX, spawnOffsetY, 0f), Quaternion.identity);
                    LevelMap.AddRoom(nextRoom.name, nextRoom);
                    break;
                case 3:
                    rand = Random.Range(0, templates.LRooms.Length);
                    nextRoom = Instantiate(templates.LRooms[rand], transform.position + new Vector3(spawnOffsetX, spawnOffsetY, 0f), Quaternion.identity);
                    LevelMap.AddRoom(nextRoom.name, nextRoom);
                    break;
                case 4:
                    rand = Random.Range(0, templates.TRooms.Length);
                    nextRoom = Instantiate(templates.TRooms[rand], transform.position + new Vector3(spawnOffsetX, spawnOffsetY, 0f), Quaternion.identity);
                    LevelMap.AddRoom(nextRoom.name, nextRoom);
                    break;
            }
        }

        if (templates.roomLeft <= 0 && !spawned) {
            //remove unused doors from this room
            Quaternion rotation = GetTileRotation();
            //spawn Wall tile to parent.transform.position
            LevelTemplate lt = FindObjectOfType<LevelTemplate>();
            GameObject newObject = Instantiate(lt.LevelWalls[Random.Range(0, lt.LevelWalls.Length)], transform.parent.transform.position, rotation);
            newObject.transform.parent = transform.parent.parent;
            CleanUp();
        }
    }

    Quaternion GetTileRotation()
    {
        //walls
        //to R -1 1
        //to L 1 1
        //to T -1 0
        //to B 0  0
        Quaternion rotation = Quaternion.identity;
        switch (this.openingPoint)
        {
            case 1:
                rotation = new Quaternion(-1f, 1f, 0f, 0f);
                break;
            case 2:
                rotation = new Quaternion(-1f, 0f, 0f, 0f);
                break;
            case 3:
                rotation = new Quaternion(1f, 1f, 0f, 0f);
                break;
            case 4:
                rotation = new Quaternion(0f, 0f, 0f, 0f);
                break;
        }
        return rotation;
    }


    void CleanUp()
    {
        if (!spawned)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Room" || collision.tag=="RoomSpawn") Destroy(gameObject);
    }
}
