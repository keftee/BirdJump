using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Numerics;

public class GameLogic : MonoBehaviour
{
    public int score;
    [SerializeField] TMP_Text scoreCard;
    [SerializeField] UnityEngine.UI.Image pausemenu;
    BackgroundMoveScript[] movnigObjects;
    [SerializeField] GameObject player;
    [SerializeField] private float jumpForce = 2.0f;
    private Dictionary<BackgroundMoveScript, float> savedSpeeds = new Dictionary<BackgroundMoveScript, float>();
    UnityEngine.Vector3 bottomLimit = new UnityEngine.Vector3(0,-4.5f,-2);

    Rigidbody2D playerRB;
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        score = 0;
        pausemenu.gameObject.SetActive(false); // Hide pause menu at start
    }
    void Update()
    {
        if (player.transform.position.y < bottomLimit.y)
        {
            //game over screen
            //just restart the level for now
            restartScene();
        }
        if (Input.GetMouseButtonDown(0))
        {
            playerRB.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
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
        pausemenu.gameObject.SetActive(true);

    }

    public void resumeGame()
    {
        foreach (var kvp in savedSpeeds)
            if (kvp.Key != null)
                kvp.Key.speed = kvp.Value;
        pausemenu.gameObject.SetActive(false);
    }

    public void restartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
