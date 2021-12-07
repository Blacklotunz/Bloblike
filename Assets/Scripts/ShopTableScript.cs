using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ShopTableScript : MonoBehaviour
{
    public int cost;
    public GameObject sellingItem, cashy;
    private Transform playerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Open confirm dialog
            ConversationManager.Instance.StartConversation(this.GetComponent<NPCConversation>());
            playerPosition = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Close confirm dialog
            ConversationManager.Instance.EndConversation();
        }
    }

    public void Buy()
    {
        if (int.Parse(CoinText.coinCounter.text) >= cost)
        {
            cashy.SendMessage("getMoney", true);
            GameEvents.current.PlayerBuyItem(cost);
            Instantiate(sellingItem, playerPosition.position, Quaternion.identity);
        }
        else
        {
            cashy.SendMessage("getMoney", false);
        }
    }
}
