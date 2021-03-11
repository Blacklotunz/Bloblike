using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthCount : MonoBehaviour
{
    public int maxHealthLevel;
    private Animator hca;
    private int healthLevel;
    // Start is called before the first frame update
    void Start()
    {
        healthLevel = maxHealthLevel;
        hca = this.GetComponent<Animator>();
        hca.SetInteger("healthLevel", healthLevel);
    }

    public void increase()
    {
        healthLevel++;
        hca.SetInteger("healthLevel", healthLevel);
    }

    public void decrease()
    {
        healthLevel--;
        hca.SetInteger("healthLevel", healthLevel);
    }

    public int getHealthLevel()
    {
        return healthLevel;
    }

}
