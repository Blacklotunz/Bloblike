using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onPlayerDie;
    public void PlayerDie()
    {
        if (onPlayerDie != null)
        {
            onPlayerDie();
        }
    }

    public event Action onCoinCollected;
    public void CoinCollected()
    {
        if(onCoinCollected != null)
        {
            onCoinCollected();
        }
    }

    public event Action<int> onPlayerHealthChange;
    public void PlayerHealthChange(int health)
    {
        if(onPlayerHealthChange != null)
        {
            onPlayerHealthChange(health);
        }
    }


    public event Action<int> onPlayerBuyItem;
    internal void PlayerBuyItem(int cost)
    {
        if (onPlayerBuyItem != null)
        {
            onPlayerBuyItem(cost);
        }
    }

}
