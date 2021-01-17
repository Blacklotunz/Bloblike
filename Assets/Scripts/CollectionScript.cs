using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : MonoBehaviour
{
    
    void Collect()
    {
        Destroy(this.gameObject);
    }
}
