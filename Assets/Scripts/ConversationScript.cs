using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationScript : MonoBehaviour
{
    public NPCConversation conversation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }


}
