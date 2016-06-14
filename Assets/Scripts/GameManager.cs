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

    [SerializeField]
    private float spawnTimerReduceValue;
    private float spawnTimerReduce;
    [SerializeField]
    private int score;

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
            Vector3 enemyPosition = GetRandomVector();
            Instantiate(Enemy, enemyPosition, Quaternion.identity);
            enemyTimer = enemyTimerValue;
        }
    }

    void spawnTimerReducer()
    {
        spawnTimerReduce -= Time.deltaTime;
        if(spawnTimerReduce <= 0)
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
    }

    void Update()
    {
        if (!running)
            return;

        spawnEnemy();
        updateScore();
        spawnTimerReducer();

        Debug.Log(score);
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
