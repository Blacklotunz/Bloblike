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
    public bool spawned;
    public float spawnOffsetX, spawnOffsetY;
    public GameObject replacementWall;

    // Start is called before the first frame update
    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("RoomsTemplate").GetComponent<RoomTemplates>();
        Invoke("Spawn", .1f);
    }

    void Spawn()
    {
        if (templates.roomLeft > 0)
        {
            spawned = true;
            templates.roomLeft--;
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
            
        }else
        {
            RemoveDoor();
        }
    }

    public void RemoveDoor()
    {
        //remove unused doors from this room
        Quaternion rotation = GetTileRotation();
        GameObject newObject;
        if (replacementWall != null)
        {
            newObject = Instantiate(replacementWall, transform.parent.transform.position, rotation);
        }
        else
        {
            //spawn Wall tile to parent.transform.position
            LevelTemplate lt = FindObjectOfType<LevelTemplate>();
            newObject = Instantiate(lt.LevelWalls[Random.Range(0, lt.LevelWalls.Length)], transform.parent.transform.position, rotation);
        }
        newObject.transform.parent = transform.parent.parent;
        Destroy(transform.parent.gameObject);
    }

    Quaternion GetTileRotation()
    {
        Quaternion rotation = Quaternion.identity;
        switch (this.openingPoint)
        {
            case 1:
                rotation = Quaternion.Euler(0f, 0f, - 90f);
                break;
            case 2:
                rotation = Quaternion.Euler(0f, 0f, 180f);
                break;
            case 3:
                rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
        }
        return rotation;
    }
}
