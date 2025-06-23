using UnityEngine;

public class NavScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void mainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
