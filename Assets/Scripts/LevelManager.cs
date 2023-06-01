using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBase;
    public InGameUI ui;
    public int playerLives = 3;
    public bool isEnabled = false;

    public int spawnLimit;
    public EnemySpawn[] enemySpawn;
    private PlayerSpawn playerSpawn;

    void Start()
    {
        isEnabled = true;
        playerSpawn = FindObjectOfType<PlayerSpawn>();
        playerBase = FindObjectOfType<HealthBase>().gameObject;
        player = playerSpawn.player;
        enemySpawn = GetComponentsInChildren<EnemySpawn>();
        ui = GetComponentInChildren<InGameUI>();
        StartSpawn();
    }

    private void FixedUpdate()
    {
        CheckPlayerLives();
        CheckIfSpawnsAreFinished();
        CheckIfBaseIsAlive();
    }

    private void CheckIfBaseIsAlive()
    {
        if(playerBase == null)
        {
            GameOver();
        }
    }

    private void CheckPlayerLives()
    {
        if (playerSpawn.player == null)
        {
            if (playerLives > 0)
            {
                playerLives--;
                playerSpawn.SpawnPlayer();
            }
            else if (playerLives == 0)
            {
                playerLives--;
                playerSpawn.SpawnPlayer();
                playerSpawn.isEnabled = false;
            }
            else if (playerLives < 0)
            {
                GameOver();
            }
        }
    }

    private void CheckIfSpawnsAreFinished()
    {
        if (spawnLimit <= 0 && CheckSpawnsIfFinished())
        {
            ui.ShowContinueGameScreen();
        }
        else if (spawnLimit <= 0)
        {
            foreach (var item in enemySpawn)
            {
                item.StopSpawning();
            }
        }
    }

    public void StartSpawn()
    {
        foreach (var item in enemySpawn)
        {
            item.StartSpawning();
        }
    }

    private bool CheckSpawnsIfFinished()
    {
        bool value = true;
        for (int i = 0; i < enemySpawn.Length; i++)
        {
            if (!enemySpawn[i].isEmpty())
            {
                value = false;
            }
        }
        return value;
    }

    private void GameOver()
    {
        ui.ShowGameOverScreen();
    }
}
