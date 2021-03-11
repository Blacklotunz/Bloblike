using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    public static List<DictionaryEntry> levelMap = new List<DictionaryEntry>();
    public static int numberOfShops=1;
    public static bool AddRoom(string k, GameObject v)
    {
        if (v.name.Contains("ShopRoom"))
        {
            if (numberOfShops > 0)
            {
                numberOfShops--;
            }
            else
            {
                Destroy(v);
                return false;
            }
        }

        //GameObject lastRoom = (GameObject) levelMap.Last<DictionaryEntry>().Value;
        foreach(DictionaryEntry de in levelMap)
        {
            if (((GameObject)de.Value).transform.position == v.transform.position)
            {
                Destroy(v);
                return true;
            }
        }
        DictionaryEntry newEntry = new DictionaryEntry(k, v);
        levelMap.Add(newEntry);
        return true;
    }

    public static void ResetMap()
    {
        levelMap = new List<DictionaryEntry>();
    }


    public static GameObject GetLastRoom()
    {
        return (GameObject)levelMap.Last<DictionaryEntry>().Value;
    }
}
