using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class PotionScript : MonoBehaviour
{
    public int health;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.Use();
        }
    }

    public void Use()
    {
        GameEvents.current.PlayerHealthChange(health);
        Destroy(gameObject);
    }

}
