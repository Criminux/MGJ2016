using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameManager.Instance.EnemyKilled();
            Destroy(other.gameObject);

        }
    }
}
