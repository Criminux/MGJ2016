using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

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

    public void Awake()
    {

        instance = this;
        enemyTimer = enemyTimerValue;
        scoreTimer = scoreTimerValue;
        spawnTimerReduce = spawnTimerReduceValue;
        score = 0;
    }

    public void EnemyKilled()
    {
        score += 1000;
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
