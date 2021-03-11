using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] TRooms, BRooms, LRooms, RRooms;
    public GameObject bossRoom, player;
    public static bool bossSpawned, playerSpawned;
    public int numOfRooms, roomLeft;

    public void Start()
    {
        roomLeft = numOfRooms;
        bossSpawned = false;
        playerSpawned = false;

        Invoke("RecreateLevel", 2f);
        
    }

    public void FixedUpdate()
    {
        //spawn the player once the map is completed. Remove from here?
        if(!playerSpawned && roomLeft <= 0)
        {
            playerSpawned = true;
            GameObject lastRoom = LevelMap.GetLastRoom();

            EnemyCombat[] enemies = lastRoom.GetComponentsInChildren<EnemyCombat>();
            foreach(EnemyCombat enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
            lastRoom.GetComponent<RoomController>().roomCleared = true;
            lastRoom.GetComponent<RoomController>().enemySpawned = true;
            Instantiate(player, lastRoom.transform.position + new Vector3(3f,2f,0f) , Quaternion.identity);
        }
    }

    public void RecreateLevel()
    {
        if(roomLeft > 0)
        {
            LevelMap.ResetMap();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}