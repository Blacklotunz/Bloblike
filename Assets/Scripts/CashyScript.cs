using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class CashyScript : MonoBehaviour
{
    NPCConversation conversation;
    public NPCConversation notEnoughMoneyConversation;
    public float yieldingSeconds;
    // Start is called before the first frame update
    void Start()
    {
        conversation = this.GetComponent<NPCConversation>();
        GameEvents.current.onPlayerBuyItem += PlayBuyAnimation;
    }

    private void PlayBuyAnimation(int cost)
    {
        this.GetComponent<Animator>()?.SetTrigger("buy");
    }

    public void getMoney(bool enoughMoney)
    {
        StartCoroutine(startCachyConversation(enoughMoney));     
    }

    private IEnumerator startCachyConversation(bool enoughMoney)
    {
        yield return new WaitForSeconds(yieldingSeconds);
        if (!enoughMoney)
        {
            ConversationManager.Instance.StartConversation(notEnoughMoneyConversation);
        }
        else
        {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }

}
