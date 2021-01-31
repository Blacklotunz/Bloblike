﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    public void FixedUpdate()
    {
        //spawn the player once the map is completed. Remove from here?
        if(!playerSpawned && roomLeft <= 0)
        {
            playerSpawned = true;
            GameObject lastRoom = LevelMap.GetLastRoom();
            Instantiate(player, lastRoom.transform.position + new Vector3(3f,2f,0f) , Quaternion.identity); 
        }
    }
}