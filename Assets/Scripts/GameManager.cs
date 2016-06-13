using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

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

    [SerializeField]
    private float scoreTimerValue;
    private float scoreTimer;

    [SerializeField]
    TextMesh scoreMesh;

    public void Awake()
    {
        instance = this;
        enemyTimer = enemyTimerValue;
        scoreTimer = scoreTimerValue;
        score = 0;
    }

    public void EnemyKilled()
    {
        score += 1000;
    }

    void spawnEnemy()
    {
        enemyTimer -= Time.fixedDeltaTime;
        if (enemyTimer <= 0)
        {
            Vector3 enemyPosition = GetRandomVector();
            Instantiate(Enemy, enemyPosition, Quaternion.identity);
            enemyTimer = enemyTimerValue;
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

        scoreMesh.text = score.ToString("00000000000");
    }

    void Update()
    {
        spawnEnemy();
        updateScore();

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
