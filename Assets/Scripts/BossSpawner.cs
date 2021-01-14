using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject boss;
    public void Spawn()
    {
        Instantiate(boss, this.transform.position, Quaternion.identity, this.transform);
    }
}
