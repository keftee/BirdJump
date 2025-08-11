using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Numerics;

public class GameLogic : MonoBehaviour
{
    public int score;
    private bool gameover;
    [SerializeField] TMP_Text scoreCard;
    [SerializeField] UnityEngine.UI.Image pausemenu;
    [SerializeField] UnityEngine.UI.Image gameovermenu;
    BackgroundMoveScript[] movnigObjects;
    [SerializeField] GameObject player;
    [SerializeField] private float jumpForce = 2.0f;
    // [SerializeField] private float torque = 0.0f;
    // [SerializeField] private float gravityTorque = 0f;
    private Dictionary<BackgroundMoveScript, float> savedSpeeds = new Dictionary<BackgroundMoveScript, float>();
    UnityEngine.Vector3 bottomLimit = new UnityEngine.Vector3(0, -4.5f, -2);
    UnityEngine.Vector3 topLimit = new UnityEngine.Vector3(0, 5.0f, -2);
    PolygonCollider2D playerCollider;

    Rigidbody2D playerRB;
    void Start()
    {
        gameover = false;
        playerCollider = player.GetComponent<PolygonCollider2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        score = 0;
        pausemenu.gameObject.SetActive(false); // Hide pause menu at start
        gameovermenu.gameObject.SetActive(false); // Hide pause menu at start
    }
    void Update()
    {
        if (player.transform.position.y < bottomLimit.y || player.transform.position.y > topLimit.y)
        {
            //game over screen
            gameOver();
        }

        // check for collisions
        int lavaLayer = LayerMask.NameToLayer("Lava");
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(1 << lavaLayer);
        contactFilter.useTriggers = true;

        Collider2D[] results = new Collider2D[5];
        int hitCount = playerCollider.Overlap(contactFilter, results);
        if (hitCount > 0)
        {
            gameOver();
        }

        if (Input.GetMouseButtonDown(0) && !gameover)
        {
            // playerRB.freezeRotation = false;
            playerRB.AddForceY(jumpForce, ForceMode2D.Impulse);
            // playerRB.AddTorque(torque, ForceMode2D.Impulse);
        }
    }

    // private void FixedUpdate() {
    // }

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

    public void gameOver()
    {
        gameover = true;
        movnigObjects = FindObjectsByType<BackgroundMoveScript>(sortMode: FindObjectsSortMode.None);
        foreach (BackgroundMoveScript instance in movnigObjects)
        {
            instance.speed = 0f;
        }
        gameovermenu.gameObject.SetActive(true);
    }

    public void restartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void mainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
