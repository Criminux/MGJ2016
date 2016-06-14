using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public GameObject ExplosionEffect;

    void Start()
    {
        Destroy(Instantiate(ExplosionEffect, transform.position, Quaternion.identity), 5);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.EnemyKilled();
            Destroy(other.gameObject);

        }
    }
}
