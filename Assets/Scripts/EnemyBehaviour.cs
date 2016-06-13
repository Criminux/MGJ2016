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



    private Transform myTransform;


    void Awake()
    {
        myTransform = transform;
        maxdistance = 2;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {

        if (Vector3.Distance(target.position, myTransform.position) > maxdistance)
        {
            //Move towards target
            transform.LookAt(target.position);
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

        }

    }
}
