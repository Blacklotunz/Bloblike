using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ShopTableScript : MonoBehaviour
{
    public int cost;
    public GameObject sellingItem;
    public CashyScript cashy;
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
    public void Buy()
    {
        if (int.Parse(CoinText.coinCounter.text) >= cost)
        {
            cashy.getMoney(true);
            GameEvents.current.PlayerBuyItem(cost);
            Instantiate(sellingItem, playerPosition.position, Quaternion.identity);
        }
        else
        {
            cashy.getMoney(false);
        }
    }
}
