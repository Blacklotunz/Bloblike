using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleAnimatorController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().enabled = false;
        StartCoroutine(LightAnimation());
    }

    IEnumerator LightAnimation()
    {
        float random = Random.Range(0, 4);
        yield return new WaitForSeconds(random);
        GetComponent<Animator>().enabled = true;
    }
}
