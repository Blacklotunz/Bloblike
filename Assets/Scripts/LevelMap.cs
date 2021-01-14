using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    public static List<DictionaryEntry> levelMap = new List<DictionaryEntry>();
    public static void AddRoom(string k, GameObject v)
    {
        DictionaryEntry newEntry = new DictionaryEntry(k, v);
        levelMap.Add(newEntry);
    }

    public static void SpawnBossRoom()
    {
        BossRoom bossRoom = FindObjectOfType<RoomTemplates>().bossRoom.GetComponent<BossRoom>();
        DictionaryEntry whereToSpawn = levelMap.FindLast(delegate (DictionaryEntry e)
        {
            return bossRoom.possibleSpawningPoint.Contains(e.Key) ;
        });
    }

    public static GameObject GetLastRoom()
    {
        return (GameObject)levelMap.Last<DictionaryEntry>().Value;
    }
}
