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
    
    

    //AudioStuff
    [SerializeField]
    private AudioClip steps;
    new AudioSource audio;
    
    public void executeDamage()
    {
        health -= 100f;
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        health = 100f;
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PowerUpTimer > 0)
                health -= 40f;
            else
                health -= 20f;
            Destroy(col.gameObject);
            
        }

        if(col.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.RemoveLife();
        }
    }


    void Update()
    {
        if (!GameManager.Instance.Running)
            return;

        if (health <= 0f)
        {
            gameManager.EnemyKilled();
            Destroy(gameObject);
        }

        
            //Move towards target
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

        audio.UnPause();
        

    }
  
}
