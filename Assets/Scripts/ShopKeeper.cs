using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ShopKeeper : MonoBehaviour
{
    void Start()
    {
        GameEvents.current.onPlayerBuyItem += PlayBuyAnimation;
    }

    private void PlayBuyAnimation(int cost)
    {
        this.GetComponent<Animator>().SetTrigger("buy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ConversationManager.Instance.StartConversation(this.GetComponent<NPCConversation>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ConversationManager.Instance.EndConversation();
        }
    }
}
