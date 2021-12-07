using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class LevelMap : MonoBehaviour
{
    public static List<GameObject> levelMap = new List<GameObject>();
    public static int numberOfShops=1;
    
    public static bool AddRoom(GameObject newRoom)
    {
        if (newRoom.name.Contains("ShopRoom"))
        {
            if (numberOfShops > 0)
            {
                numberOfShops--;
            }
            else
            {
                GameObject.Destroy(newRoom);
                return false;
            }
        }

        foreach(GameObject room in levelMap)
        {
            if (room.transform.position == newRoom.transform.position)
            {
                GameObject.Destroy(newRoom);
                return true;
            }
        }
        levelMap.Add(newRoom);

        return true;
    }

    public static void ResetMap()
    {
        levelMap = new List<GameObject>();
    }

    public static GameObject GetLastRoom()
    {
        return levelMap.Last<GameObject>();
    }

}
