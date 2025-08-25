using UnityEngine;

public class PauseManager : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        //Attach Game Manager to Pause Manager
        gameManager = GetComponent<GameManager>();
    }

    public void togglePause()
    {
        //Game will be paused using key
        gameManager.gamePaused = !gameManager.gamePaused;
    }

    private void Update()
    {
        //Pause Game
        if (gameManager.gamePaused)
        {
            Time.timeScale = 0f;
        }
        //Resume Game
        else
        {
            Time.timeScale = 1f;
        }
    }
}
