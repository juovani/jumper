using UnityEngine;
using TMPro;

public class ScoreManger : MonoBehaviour
{
    public static ScoreManger instance;
    public int score;
    public int coins;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textCoins;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void addScore(int amount)
    {
        score += amount;
        textScore.text = " " + score;
    }

    public void addCoins(int amount)
    {
        coins += amount;
        textCoins.text = coins.ToString();

    }
}
