using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class PotionScript : MonoBehaviour
{
    public int health, cost;
    public NPCConversation notEnoughCoinsConv;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Open confirm dialog
            ConversationManager.Instance.StartConversation(this.GetComponent<NPCConversation>());
        }
    }

    public void Use()
    {
        GameEvents.current.PlayerHealthChange(health);
    }


    public void Buy()
    {
        if(int.Parse(CoinText.coinCounter.text) >= cost)
        {
            GameEvents.current.PlayerBuyItem(cost);
            Use();
        }
        else
        {
            ConversationManager.Instance.StartConversation(notEnoughCoinsConv);
            //ConversationManager.Instance.EndConversation();
        }   
    }
}
