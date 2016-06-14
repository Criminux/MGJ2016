using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private Highscoremanager highscoreManager;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private float enemyTimerValue;
    private float enemyTimer;


    [SerializeField]
    private float scoreTimerValue;
    private float scoreTimer;

    internal void AddLife()
    {
        if (lives < 5)
            lives++;
        UpdateLifeSprites();
    }

    [SerializeField]
    private float spawnTimerReduceValue;
    private float spawnTimerReduce;
    [SerializeField]
    private int score;

    [SerializeField]
    private float PowerUpDelay;

    [SerializeField]
    private GameObject PowerUpPrefab;

    public int Score { get { return score; } }

    [SerializeField]
    private int lives;

    public int Lives
    {
        get { return lives; }
    }

    [SerializeField]
    private GameObject[] liveSprites;


    //AudioStuff
    [SerializeField]
    private AudioClip die;
    new AudioSource audio;


    public void Awake()
    {
        instance = this;
        enemyTimer = enemyTimerValue;
        scoreTimer = scoreTimerValue;
        spawnTimerReduce = spawnTimerReduceValue;

        audio = GetComponent<AudioSource>();

        score = 0;
        highscoreManager = new Highscoremanager();
        poweruptimer = PowerUpDelay;
    }

    void Start()
    {
        MenuController.Instance.MainMenuHighscore.text = highscoreManager.Highscore.ToString("00000000000");
    }

    public void EnemyKilled()
    {
        score += 1000;
        audio.PlayOneShot(die, 1f);
    }

    void spawnEnemy()
    {
        enemyTimer -= Time.deltaTime;
        if (enemyTimer <= 0)
        {
            Vector3 enemyPosition = Vector3.zero;
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            do
            {
                enemyPosition = GetRandomVector();
            } while (Vector3.Distance(enemyPosition, playerPosition) < 3);

            Instantiate(Enemy, enemyPosition, Quaternion.identity);
            enemyTimer = enemyTimerValue;
        }
    }

    void EnemySpawnTimerReducer()
    {
        spawnTimerReduce -= Time.deltaTime;
        if (spawnTimerReduce <= 0)
        {
            enemyTimerValue = enemyTimerValue - 0.2f;
            if (enemyTimerValue <= 1)
            {
                enemyTimerValue = 1;
            }
            //enemyTimerValue = enemyTimer;
            spawnTimerReduce = spawnTimerReduceValue;
        }
    }

    void updateScore()
    {
        scoreTimer -= Time.deltaTime;
        if (scoreTimer <= 0)
        {
            score += 100;
            scoreTimer = scoreTimerValue;
        }

        MenuController.Instance.IngameScore.text = score.ToString("00000000000");
    }

    void Update()
    {
        if (!running)
            return;

        spawnEnemy();
        spawnPowerUp();
        updateScore();
        EnemySpawnTimerReducer();

        Debug.Log(score);
    }

    private void spawnPowerUp()
    {
        poweruptimer -= Time.deltaTime;

        if (poweruptimer < 0)
        {
            Instantiate(PowerUpPrefab, GetRandomVector(), Quaternion.identity);
            poweruptimer = PowerUpDelay;
        }
    }

    Vector3 GetRandomVector()
    {
        return new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5, 5f));

    }

    public void RemoveLife()
    {
        lives--;
        UpdateLifeSprites();
        if (lives <= 0)
            GameOver();
    }

    private bool running;
    private float poweruptimer;

    public bool Running
    {
        get { return running; }
        set { running = value; }
    }

    public void Restart()
    {
        running = true;
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.InitPosition();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        score = 0;
        lives = 5;
        enemyTimerValue = 5;
        MenuController.Instance.GameOverGroup.SetActive(false);
        UpdateLifeSprites();
    }

    private void GameOver()
    {
        running = false;
        MenuController.Instance.CurrentGameState = MenuController.States.GameOver;
        MenuController.Instance.GameOverGroup.SetActive(true);

        bool newHighscore = !highscoreManager.IsHighscoreGreater(score);
        int highscore = highscoreManager.Highscore;

        MenuController.Instance.GameOverHighscore.text = newHighscore ? "new Highscore\n" + highscore.ToString("00000000000") : "Highscore\n" + highscore.ToString("00000000000");
        MenuController.Instance.GameOverScore.text = score.ToString("00000000000");

        if (newHighscore)
        {
            highscoreManager.SetHigherScore(score);
            highscoreManager.SaveHighscore();
        }
    }

    private void UpdateLifeSprites()
    {
        switch (lives)
        {
            case 0:
                liveSprites[0].SetActive(false);
                liveSprites[1].SetActive(false);
                liveSprites[2].SetActive(false);
                liveSprites[3].SetActive(false);
                liveSprites[4].SetActive(false);
                break;
            case 1:
                liveSprites[0].SetActive(true);
                liveSprites[1].SetActive(false);
                liveSprites[2].SetActive(false);
                liveSprites[3].SetActive(false);
                liveSprites[4].SetActive(false);
                break;
            case 2:
                liveSprites[0].SetActive(true);
                liveSprites[1].SetActive(true);
                liveSprites[2].SetActive(false);
                liveSprites[3].SetActive(false);
                liveSprites[4].SetActive(false);
                break;
            case 3:
                liveSprites[0].SetActive(true);
                liveSprites[1].SetActive(true);
                liveSprites[2].SetActive(true);
                liveSprites[3].SetActive(false);
                liveSprites[4].SetActive(false);
                break;
            case 4:
                liveSprites[0].SetActive(true);
                liveSprites[1].SetActive(true);
                liveSprites[2].SetActive(true);
                liveSprites[3].SetActive(true);
                liveSprites[4].SetActive(false);
                break;
            case 5:
                liveSprites[0].SetActive(true);
                liveSprites[1].SetActive(true);
                liveSprites[2].SetActive(true);
                liveSprites[3].SetActive(true);
                liveSprites[4].SetActive(true);
                break;
        }
    }

}
