using System;
using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float deleteX = -10f;
    [SerializeField] private float spawnX = 0f;
    [SerializeField] private float newPosX = 10f;
    [SerializeField] private bool randomize = false;
    
    private float[] level1 = { -1f, 1f };
    private float[] level2 = { -1.5f, 1f };
    private float[] level3 = { -2f, 2f };
    private float[] level;

    private bool hasSpawned = false;
    private GameLogic gameLogic;
    private float randomY;
    

    void Start()
    {
        if (randomize)
        {
            GameObject gameGameObj = GameObject.Find("GameGame");
            if (gameGameObj != null)
            {
                gameLogic = gameGameObj.GetComponent<GameLogic>();
            }
        }
    }

    void levelDecider()
    {
        if (gameLogic.score <= 20)
        {
            level = level1;
        }
        else if (gameLogic.score <= 50)
        {
            level = level2;
        }
        else
        {
            level = level3;
        }
    }

    void addScore()
    {
        gameLogic.score++;
        gameLogic.changeScoreCard();
        // Debug.Log(gameLogic.score);
    }
    // Update is called once per frame
    void Update()
    {
        // Move the background left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // If randomize is true, decide the difficulty
        if (randomize) levelDecider();

        // Spawn new background when passing spawnX
        if (!hasSpawned && transform.position.x <= spawnX)
        {
            if (randomize)
            {
                levelDecider(); addScore();
                randomY = (float)Math.Round(UnityEngine.Random.Range(level[0], level[1]), 1);
            }
            float yPos = randomize ? randomY : transform.position.y;
            Instantiate(gameObject, new Vector3(newPosX, yPos, transform.position.z), Quaternion.identity);
            hasSpawned = true;
        }

        // Destroy when off-screen
        if (transform.position.x < deleteX)
        {
            Destroy(gameObject);
        }
    } 
}
