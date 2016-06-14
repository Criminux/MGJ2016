using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    private GameManager gameManager = GameManager.Instance;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private int moveSpeed;
    [SerializeField]
    private int rotationSpeed;
    private int maxdistance;

    private float health;

    private Transform myTransform;


    //AudioStuff
    [SerializeField]
    private AudioClip steps;
    new AudioSource audio;

    void Awake()
    {
        myTransform = transform;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        health = 100f;

        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            
            health -= 20f;
            Destroy(col.gameObject);
            
        }
    }


    void Update()
    {
        if(health <= 0f)
        {
            gameManager.EnemyKilled();
           
            Destroy(gameObject);
            
        }

        
            //Move towards target
            transform.LookAt(new Vector3(target.position.x, target.position.y + 1, target.position.z));
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

        audio.UnPause();
        

    }
  
}
