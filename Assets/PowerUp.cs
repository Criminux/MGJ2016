using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{

    enum PowerUpType
    {
        Life,
        Damage
    }

    [SerializeField]
    PowerUpType type;

    [SerializeField]
    float PowerUpTimer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == PowerUpType.Damage)
                other.GetComponent<PlayerController>().PowerUpTimer = PowerUpTimer;
            else
            {
                GameManager.Instance.AddLife();
            }
            //Suicide mission
            Destroy(gameObject);
        }
    }
}
