using UnityEngine;
using TMPro;

public class GameLogic : MonoBehaviour
{
    public int score;
    [SerializeField] TMP_Text scoreCard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    public void changeScoreCard()
    {
        scoreCard.text = score.ToString();
    }
}
