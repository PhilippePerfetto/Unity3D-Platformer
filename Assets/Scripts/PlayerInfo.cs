using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo Instance;
    public int playerHealth = 3;
    public int nbCoins = 0;
    public Image[] hearts;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public CheckpointMgr checkpointMgr;

    private void Awake()
    {
        Instance = this;
    }

    public void SetHealth(int val)
    {
        print("START SetHealth : " + val + " on " + playerHealth);

        playerHealth += val;

        if (playerHealth > 3) playerHealth = 3;
        else if (playerHealth <= 0) { print("WTF !!!"); playerHealth = 0; checkpointMgr.Respawn(); }

        SetHealthBar();

        print("END SetHealth : " + val + " on " + playerHealth);
    }

    public void AddCoin()
    {
        nbCoins++;
        coinText.text = nbCoins.ToString();
    }

    public void SetHealthBar()
    {
        foreach (var hearth in hearts)
        {
            hearth.enabled = false;
        }

        for (int i = 0; i < playerHealth - 1; i++)
        {
            hearts[i].enabled = true;
        }
    }

    public int GetScore()
    {
        int score = nbCoins * 5 + playerHealth * 10;
        scoreText.text = "Score : " + score.ToString();
        return score;
    }
}
