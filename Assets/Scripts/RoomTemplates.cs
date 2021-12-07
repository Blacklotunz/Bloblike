using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RoomTemplates : MonoBehaviour
{
    public GameObject[] TRooms, BRooms, LRooms, RRooms;
    public GameObject bossRoom, player;
    public static bool bossSpawned, playerSpawned;
    public int numOfRooms, roomLeft;
    AstarPath pathfinder;
    
    [SerializeField]
    private List<GameObject> finalLevelMap;

    public void Start()
    {
        roomLeft = numOfRooms;
        bossSpawned = false;
        playerSpawned = false;

        Invoke("RecreateLevel", 2f);
        pathfinder = GameObject.FindObjectOfType<AstarPath>();
        
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
            Invoke("UpdateGraphMap", 3f);

            finalLevelMap = LevelMap.levelMap;
        }
    }

    void UpdateGraphMap()
    {
        if (pathfinder)
        {
            pathfinder.Scan();
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