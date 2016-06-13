using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private float enemyTimer;

    public void Awake()
    {
        instance = this;
    }

    void Update()
    {
        enemyTimer -= Time.fixedDeltaTime;
        if(enemyTimer <= 0)
        {
            Vector3 enemyPosition = GetRandomVector();
            Instantiate(Enemy, enemyPosition, Quaternion.identity);
            enemyTimer = 5;
        }
    }

    Vector3 GetRandomVector()
    {
        return new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5, 5f));
        
    }

}
