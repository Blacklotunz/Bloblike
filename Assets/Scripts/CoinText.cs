using System;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public static TMPro.TextMeshProUGUI coinCounter;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onCoinCollected += CoinCollected;
        GameEvents.current.onPlayerBuyItem += CoinsSpent;
        coinCounter = this.GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void CoinsSpent(int cost)
    {
        int count = (int.Parse(coinCounter.text)) - cost;
        coinCounter.text = count.ToString();
    }

    void CoinCollected()
    {
        int count = (int.Parse(coinCounter.text)) + 1;
        coinCounter.text = count.ToString();
    }
}
