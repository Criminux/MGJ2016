using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    [SerializeField]
    private int moveSpeed;
    [SerializeField]
    private int rotationSpeed;
    private int maxdistance;

    private float health;

    private Transform myTransform;


    void Awake()
    {
        myTransform = transform;
        maxdistance = 2;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        health = 100f;
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            health -= 20f;
            
        }
    }


    void Update()
    {
        if(health <= 0f)
        {
            Destroy(gameObject);
        }

        if (Vector3.Distance(target.position, myTransform.position) > maxdistance)
        {
            //Move towards target
            transform.LookAt(new Vector3(target.position.x, target.position.y + 1, target.position.z));
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

        }

    }
}
