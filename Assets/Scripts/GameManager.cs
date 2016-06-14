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

    private float score;

    [SerializeField]
    private float scoreTimerValue;
    private float scoreTimer;

    [SerializeField]
    private float spawnTimerReduceValue;
    private float spawnTimerReduce;

    public void Awake()
    {

        instance = this;
        enemyTimer = enemyTimerValue;
        scoreTimer = scoreTimerValue;
        spawnTimerReduce = spawnTimerReduceValue;
        score = 0f;
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
        scoreTimer -= Time.fixedDeltaTime;
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

}
