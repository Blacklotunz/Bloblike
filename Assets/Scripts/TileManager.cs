using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Sprite[] sprites;
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
            this.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            spawned = true;
        }
    }
}
