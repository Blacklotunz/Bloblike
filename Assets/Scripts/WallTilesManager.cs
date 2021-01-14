using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTilesManager : MonoBehaviour
{
    //corners
    //TL -1 1
    //TR 0 1
    //BR 1 1
    //BL 1 0
    //walls
    //to R -1 1
    //to L 1 1
    //to T -1 0
    //to B 0  0

    public Sprite[] walls, corners;
    private bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            this.GetComponent<SpriteRenderer>().sprite = this.transform.parent.name == "corner" ? corners[Random.Range(0, corners.Length)] : walls[Random.Range(0, walls.Length)];
            spawned = true;
        }
    }
}
