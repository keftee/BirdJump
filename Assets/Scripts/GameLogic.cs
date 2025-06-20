using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;

public class GameLogic : MonoBehaviour
{
    public int score;
    [SerializeField] TMP_Text scoreCard;
    [SerializeField] UnityEngine.UI.Image pausemenu;
    BackgroundMoveScript[] movnigObjects;
    private Dictionary<BackgroundMoveScript, float> savedSpeeds = new Dictionary<BackgroundMoveScript, float>();
    void Start()
    {
        Application.targetFrameRate = 60;
        score = 0;
    }

    public void changeScoreCard()
    {
        scoreCard.text = score.ToString();
    }
    public void pauseMenu()
    {
        movnigObjects = FindObjectsByType<BackgroundMoveScript>(sortMode: FindObjectsSortMode.None);
        savedSpeeds.Clear();
        foreach (BackgroundMoveScript instance in movnigObjects)
        {
            savedSpeeds[instance] = instance.speed;
            instance.speed = 0f;
        }
        
    }

    public void resumeGame()
    {
        foreach (var kvp in savedSpeeds)
            if (kvp.Key != null)
                kvp.Key.speed = kvp.Value;
    }
}
