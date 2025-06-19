using System;
using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float deleteX = -10f;
    [SerializeField] private float spawnX = 0f;
    [SerializeField] private float newPosX = 10f;
    [SerializeField] private bool randomize = false;
    [SerializeField] private GameObject game;
    
    private float[] level1 = { -1f, 1f };
    private float[] level2 = { -1.5f, 1f };
    private float[] level3 = { -2f, 2f };
    private float[] level;
    int score;

    private bool hasSpawned = false;

    void levelDecider()
    {
        if (score <= 20)
        {
            level = level1;
        }
        else if (score <= 50)
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
        // game.score++;
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
            float randomY = (float)Math.Round(UnityEngine.Random.Range(level[0], level[1]), 1);
            float yPos = randomize ? randomY : transform.position.y;
            addScore();
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
