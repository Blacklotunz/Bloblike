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
        if (null != onPlayerDie)
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
}
