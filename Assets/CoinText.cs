using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI coinCounter;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onCoinCollected += CoinCollected;
        coinCounter = this.GetComponent<TMPro.TextMeshProUGUI>();
    }

    void CoinCollected()
    {
        int count = (int.Parse(coinCounter.text)) + 1;
        coinCounter.text = count.ToString();
    }
}
