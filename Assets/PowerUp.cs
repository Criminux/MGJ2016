using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
    public GameObject SpecialEffect;

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

            Destroy(Instantiate(SpecialEffect, transform.position, Quaternion.identity), 2.1f);
            //Suicide mission
            Destroy(gameObject);
        }
    }
}
