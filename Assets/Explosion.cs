using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(other.gameObject);
    }
}
